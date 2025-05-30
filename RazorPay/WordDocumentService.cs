using NLog;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Words.NET;

namespace RazorPay
{
    public class WordDocumentService
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public byte[] CreateDocumentWithStickerDetails(List<(string qrType, string merchantName, string merchantAddress, byte[] barcodeBytes, string pin, string mobile, string mid, string state, string tidnumber, string CONTACTPERSONNAME, string AWBNO)> stickerDetails, out byte[] pdfBytes)
        {
           
            try
            {
                pdfBytes = null;
                using (var doc = DocX.Create("Stickers.docx"))
                {
                    foreach (var entry in stickerDetails)
                    {
                        var qrType = "NTB";
                        var merchantName = entry.merchantName;
                        var merchantAddress = entry.merchantAddress;
                        var barcodeBytes = entry.barcodeBytes;
                        var pin = entry.pin;
                        var mobile = entry.mobile;
                        var mid = entry.mid;
                        var state = entry.state;
                        var tidnumber = entry.tidnumber;
                        var CONTACTPERSONNAME = entry.CONTACTPERSONNAME;
                        var AWBNO = entry.AWBNO;

                        // Insert data into the document
                        doc.InsertParagraph($"QR Type- {qrType}");
                        doc.InsertParagraph($"Merchant Name:- {merchantName}");
                        doc.InsertParagraph($"ContactPerson Name:- {CONTACTPERSONNAME}");
                        doc.InsertParagraph($"Merchant Address: - {merchantAddress}");
                        doc.InsertParagraph($"AWD:-{AWBNO}");

                        // Insert barcode image
                        using (var ms = new MemoryStream(barcodeBytes))
                        {
                            var image = doc.AddImage(ms);
                            var picture = image.CreatePicture();
                            doc.InsertParagraph().InsertPicture(picture);
                        }
                        doc.InsertParagraph(tidnumber);
                        doc.InsertParagraph($"Pin: {pin}");
                        doc.InsertParagraph($"Mobile: {mobile}");
                        doc.InsertParagraph($"TID: {tidnumber}");
                        doc.InsertParagraph($"MID: {mid}");
                        doc.InsertParagraph($"State: {state}");
                        doc.InsertParagraph(); // Add empty line between entries
                    }

                    using (var ms = new MemoryStream())
                    {
                        doc.SaveAs(ms);
                        var docBytes = ms.ToArray();

                        // Generate PDF using PdfSharp
                        pdfBytes = GeneratePdfFromDoc(stickerDetails);

                        return docBytes;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Info($"{ex.ToString()}");
                pdfBytes = null; // Ensure pdfBytes is assigned even in the catch block
                return null;
            }
        }
        private byte[] GeneratePdfFromDoc(List<(string qrType, string merchantName, string merchantAddress, byte[] barcodeBytes, string pin, string mobile, string mid, string state, string tidnumber, string CONTACTPERSONNAME, string AWBNO)> stickerDetails)
        {
            try
            {
                using (var document = new PdfDocument())
                {
                    var setsPerPage = 8;
                    var page = document.AddPage();
                    var gfx = XGraphics.FromPdfPage(page);
                    var font = new XFont("calibri", 10, XFontStyle.Bold);

                    var margin = 20.0;
                    var lineHeight = 20;
                    var pageWidth = page.Width;
                    var pageHeight = page.Height;
                    var columnWidth = (pageWidth - 3 * margin) / 2; // Width of each column
                    var yPoint = margin;
                    var xPointLeft = margin;
                    var xPointRight = columnWidth + 2 * margin;

                    int index = 0;

                    while (index < stickerDetails.Count)
                    {
                        // Check if we need a new page
                        if (index > 0 && index % setsPerPage == 0)
                        {
                            page = document.AddPage();
                            gfx = XGraphics.FromPdfPage(page);
                            yPoint = margin; // Reset yPoint for the new page
                        }

                        // Draw content for the current row
                        var initialYPoint = yPoint; // Store the initial yPoint to use for both columns
                        double yPointLeft = initialYPoint;
                        double yPointRight = initialYPoint;

                        double leftColumnHeight = 0;
                        double rightColumnHeight = 0;
                        int sp = 0;

                        if (index < stickerDetails.Count)
                        {
                            yPointLeft = DrawContentInColumn(gfx, stickerDetails[index], font, XBrushes.Black, xPointLeft, initialYPoint, columnWidth, out leftColumnHeight, sp);
                            index++;
                        }

                        if (index < stickerDetails.Count)
                        {
                            yPointRight = DrawContentInColumn(gfx, stickerDetails[index], font, XBrushes.Black, xPointRight, initialYPoint, columnWidth, out rightColumnHeight, sp);
                            index++;
                        }

                        // Update yPoint to the maximum height reached by either column
                        yPoint = initialYPoint + Math.Max(leftColumnHeight, rightColumnHeight) + lineHeight; // Move down to start a new row
                    }

                    using (var ms = new MemoryStream())
                    {
                        document.Save(ms);
                        return ms.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Info($"{ex.ToString()}");

                return null;
            }
        }

        private double DrawContentInColumn(XGraphics gfx, (string qrType, string merchantName, string merchantAddress, byte[] barcodeBytes, string pin, string mobile, string mid, string state, string tidnumber, string CONTACTPERSONNAME, string AWBNO) entry, XFont font, XBrush brush, double x, double y, double maxWidth, out double contentHeight, int sp)
        {
            int additionalOffset = 70;
            var lineHeight = font.GetHeight();
            var initialY = y;
            var spacing = 0.5 * lineHeight; // Space equivalent to three lines
            var firstspacing = sp;
            y += sp;
            y = DrawString(gfx, $"QR Type- {entry.qrType}", font, brush, x, y, maxWidth);
            y = y - sp;
            y = DrawString(gfx, $"Merchant Name:- {entry.merchantName}", font, brush, x, y, maxWidth);
            if (!string.IsNullOrEmpty(entry.CONTACTPERSONNAME))
            {
                y = DrawString(gfx, $"ContactPersonName:- {entry.CONTACTPERSONNAME}", font, brush, x, y, maxWidth);
            }
            y += spacing; // Add spacing before Merchant Address
            y = DrawString(gfx, $"Merchant Address: - {entry.merchantAddress}", font, brush, x, y, maxWidth);
            y += spacing; // Add spacing after Merchant Address

            // Center TID and tidnumber
            if (!string.IsNullOrEmpty(entry.AWBNO))
            {
                var tidText = $"AWB: {entry.AWBNO}";
                var tidTextSize = gfx.MeasureString(tidText, font);
                var centeredX = x + (maxWidth - tidTextSize.Width) / 2;
                y = DrawString(gfx, tidText, font, brush, centeredX, y, tidTextSize.Width);
            }
            else
            {
                var tidText = $"TID: {entry.tidnumber}";
                var tidTextSize = gfx.MeasureString(tidText, font);
                var centeredX = x + (maxWidth - tidTextSize.Width) / 2;
                y = DrawString(gfx, tidText, font, brush, centeredX, y, tidTextSize.Width);
            }

            // Center barcode image
            using (var ms = new MemoryStream(entry.barcodeBytes))
            {
                var barcodeImage = XImage.FromStream(() => ms);
                var centeredImageX = x + (maxWidth - barcodeImage.PixelWidth) / 2;
                gfx.DrawImage(barcodeImage, centeredImageX, y + lineHeight, barcodeImage.PixelWidth, barcodeImage.PixelHeight);
                y += barcodeImage.PixelHeight + lineHeight * 2;
            }

            y = DrawString(gfx, $"Pin: {entry.pin}", font, brush, x, y, maxWidth);

            if (entry.mid.Length > 6)
            {
                y = DrawString(gfx, $"Mobile: {entry.mobile}", font, brush, x, y, maxWidth);
                if (!string.IsNullOrEmpty(entry.AWBNO))
                {
                    y = DrawString(gfx, $"TID: {entry.tidnumber}", font, brush, x, y, maxWidth);
                }
                y = DrawString(gfx, $"MID: {entry.mid}", font, brush, x, y, maxWidth);

            }
            else
            {
                spacing = spacing + 2;
                var textWidth = maxWidth / 2; // Half the width for each column
                y = DrawString(gfx, $"Mobile: {entry.mobile}", font, brush, x, y, textWidth);
                if (!string.IsNullOrEmpty(entry.AWBNO))
                {
                    y = DrawString(gfx, $"TID: {entry.tidnumber}", font, brush, x + textWidth, y - lineHeight, textWidth);
                }

                y = y + spacing;
                y = DrawString(gfx, $"MID: {entry.mid}", font, brush, x + textWidth + additionalOffset, y - lineHeight, textWidth);
                y = y - spacing;

            }
            if (entry.state.Length <= 30)
            {

                y = DrawString(gfx, $"STATE: {entry.state}", font, brush, x, y, maxWidth);
            }
            else
            {
                string[] parts = SplitStringAtLength(entry.state, 10);
                y = DrawString(gfx, $"STATE: {parts[0]}", font, brush, x, y, maxWidth);
                y = DrawString(gfx, parts[1], font, brush, x, y, maxWidth);
            }

            contentHeight = y - initialY;

            return y;
        }

        private double DrawString(XGraphics gfx, string text, XFont font, XBrush brush, double x, double y, double maxWidth)
        {
            var words = text.Split(' ');
            var currentLine = string.Empty;
            var lineHeight = font.GetHeight();

            foreach (var word in words)
            {
                var testLine = string.IsNullOrEmpty(currentLine) ? word : currentLine + " " + word;
                var size = gfx.MeasureString(testLine, font);

                if (size.Width > maxWidth)
                {
                    gfx.DrawString(currentLine, font, brush, new XRect(x, y, maxWidth, lineHeight), XStringFormats.TopLeft);
                    y += lineHeight;
                    currentLine = word;
                }
                else
                {
                    currentLine = testLine;
                }
            }

            if (!string.IsNullOrEmpty(currentLine))
            {
                gfx.DrawString(currentLine, font, brush, new XRect(x, y, maxWidth, lineHeight), XStringFormats.TopLeft);
                y += lineHeight;
            }

            return y;
        }


        private string[] SplitStringAtLength(string input, int length)
        {
            if (input.Length <= length)
                return new string[] { input };

            int breakIndex = input.LastIndexOf(' ', length);
            if (breakIndex == -1 || breakIndex == 0)
                return new string[] { input, string.Empty }; // No space found or space at the beginning

            return new string[]
            {
        input.Substring(0, breakIndex),
        input.Substring(breakIndex + 1)
            };
        }
    }
}
