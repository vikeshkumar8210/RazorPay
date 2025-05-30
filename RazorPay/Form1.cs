using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using Xceed.Words.NET;
using ZXing.Common;
using ZXing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using NLog;

namespace RazorPay
{
    public partial class Form1 : Form
    {
        private readonly BarcodeService _barcodeService;
        private readonly ExcelService _excelService;
        private readonly WordDocumentService _wordDocumentService;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public Form1(BarcodeService barcodeService, ExcelService excelService, WordDocumentService wordDocumentService)
        {
            _barcodeService = barcodeService ?? throw new ArgumentNullException(nameof(barcodeService));
            _excelService = excelService ?? throw new ArgumentNullException(nameof(excelService));
            _wordDocumentService = wordDocumentService ?? throw new ArgumentNullException(nameof(wordDocumentService));
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Logger.Info("Form1 loaded.");
            lbl_selectfile.Parent = pic_bg;
            lbl_name.Parent = pic_bg;
            pic_logo.Parent = pic_bg;
            lbl_selectfile.BackColor = Color.Transparent;
            lbl_name.BackColor = Color.Transparent;
            pic_logo.BackColor = Color.Transparent;
        }
        private void btn_Browse_Click_1(object sender, EventArgs e)
        {
            try
            {

                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "Excel Files|*.xls;*.xlsx";
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        txt_browse.Text = openFileDialog.FileName;
                        Logger.Info($"File selected: {openFileDialog.FileName}");
                    }
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
        //public List<(string qrType, string merchantName, string merchantAddress, string barcodeContent, string pin, string mobile, string mid, string state, string tidnumber)> GetStickerDetails(string filePath)
        //{
        //    var stickerDetails = new List<(string, string, string, string, string, string, string, string, string)>();

        //    using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        //    {
        //        IWorkbook workbook;

        //        // Check the file extension to determine which NPOI class to use
        //        if (Path.GetExtension(filePath).ToLower() == ".xls")
        //        {
        //            workbook = new HSSFWorkbook(stream); // for .xls
        //        }
        //        else if (Path.GetExtension(filePath).ToLower() == ".xlsx")
        //        {
        //            workbook = new XSSFWorkbook(stream); // for .xlsx
        //        }
        //        else
        //        {
        //            throw new Exception("Invalid file format. Please upload a valid Excel file.");
        //        }

        //        var sheet = workbook.GetSheetAt(0); // Get the first sheet

        //        for (int row = 1; row <= sheet.LastRowNum; row++)
        //        {
        //            var currentRow = sheet.GetRow(row);
        //            if (currentRow != null)
        //            {
        //                var qrType = "NTB";
        //                var merchantName = currentRow.GetCell(1)?.ToString();
        //                var mid = currentRow.GetCell(2)?.ToString();
        //                var barcodeContent = currentRow.GetCell(3)?.ToString();
        //                var merchantAddress = currentRow.GetCell(4)?.ToString();
        //                var pin = currentRow.GetCell(5)?.ToString();
        //                var state = currentRow.GetCell(6)?.ToString();
        //                var mobile = currentRow.GetCell(8)?.ToString();
        //                var tidnumber = barcodeContent;

        //                if (!string.IsNullOrEmpty(qrType) && !string.IsNullOrEmpty(merchantName) &&
        //                    !string.IsNullOrEmpty(merchantAddress) && !string.IsNullOrEmpty(barcodeContent) &&
        //                    !string.IsNullOrEmpty(pin) && !string.IsNullOrEmpty(mobile) &&
        //                    !string.IsNullOrEmpty(mid) && !string.IsNullOrEmpty(state) && !string.IsNullOrEmpty(tidnumber))
        //                {
        //                    stickerDetails.Add((qrType, merchantName, merchantAddress, barcodeContent, pin, mobile, mid, state, tidnumber));
        //                }
        //            }
        //        }
        //    }

        //    return stickerDetails;
        //}

        //public byte[] CreateDocumentWithStickerDetails(List<(string qrType, string merchantName, string merchantAddress, byte[] barcodeBytes, string pin, string mobile, string mid, string state, string tidnumber)> stickerDetails, out byte[] pdfBytes)
        //{
        //    pdfBytes = null;
        //    using (var doc = DocX.Create("Stickers.docx"))
        //    {
        //        foreach (var entry in stickerDetails)
        //        {
        //            var qrType = "NTB";
        //            var merchantName = entry.merchantName;
        //            var merchantAddress = entry.merchantAddress;
        //            var barcodeBytes = entry.barcodeBytes;
        //            var pin = entry.pin;
        //            var mobile = entry.mobile;
        //            var mid = entry.mid;
        //            var state = entry.state;
        //            var tidnumber = entry.tidnumber;

        //            // Insert data into the document
        //            doc.InsertParagraph($"QR Type- {qrType}");
        //            doc.InsertParagraph($"Merchant Name:- {merchantName}");
        //            doc.InsertParagraph($"Merchant Address: - {merchantAddress}");
        //            doc.InsertParagraph($"TID:");

        //            // Insert barcode image
        //            using (var ms = new MemoryStream(barcodeBytes))
        //            {
        //                var image = doc.AddImage(ms);
        //                var picture = image.CreatePicture();
        //                doc.InsertParagraph().InsertPicture(picture);
        //            }
        //            doc.InsertParagraph(tidnumber);
        //            doc.InsertParagraph($"Pin: {pin}");
        //            doc.InsertParagraph($"Mobile: {mobile}");
        //            doc.InsertParagraph($"MID: {mid}");
        //            doc.InsertParagraph($"State: {state}");
        //            doc.InsertParagraph(); // Add empty line between entries
        //        }

        //        using (var ms = new MemoryStream())
        //        {
        //            doc.SaveAs(ms);
        //            var docBytes = ms.ToArray();

        //            // Generate PDF using PdfSharp
        //            pdfBytes = GeneratePdfFromDoc(stickerDetails);

        //            return docBytes;
        //        }
        //    }
        //}
        //private byte[] GeneratePdfFromDoc(List<(string qrType, string merchantName, string merchantAddress, byte[] barcodeBytes, string pin, string mobile, string mid, string state, string tidnumber)> stickerDetails)
        //{
        //    using (var document = new PdfDocument())
        //    {
        //        var setsPerPage = 8;
        //        var page = document.AddPage();
        //        var gfx = XGraphics.FromPdfPage(page);
        //        var font = new XFont("calibri", 10, XFontStyle.Bold);

        //        var margin = 20.0;
        //        var lineHeight = 20;
        //        var pageWidth = page.Width;
        //        var pageHeight = page.Height;
        //        var columnWidth = (pageWidth - 3 * margin) / 2; // Width of each column
        //        var yPoint = margin;
        //        var xPointLeft = margin;
        //        var xPointRight = columnWidth + 2 * margin;

        //        int index = 0;

        //        while (index < stickerDetails.Count)
        //        {
        //            // Check if we need a new page
        //            if (index > 0 && index % setsPerPage == 0)
        //            {
        //                page = document.AddPage();
        //                gfx = XGraphics.FromPdfPage(page);
        //                yPoint = margin; // Reset yPoint for the new page
        //            }

        //            // Draw content for the current row
        //            var initialYPoint = yPoint; // Store the initial yPoint to use for both columns
        //            double yPointLeft = initialYPoint;
        //            double yPointRight = initialYPoint;

        //            double leftColumnHeight = 0;
        //            double rightColumnHeight = 0;
        //            int sp = 0;

        //            if (index < stickerDetails.Count)
        //            {
        //                yPointLeft = DrawContentInColumn(gfx, stickerDetails[index], font, XBrushes.Black, xPointLeft, initialYPoint, columnWidth, out leftColumnHeight, sp);
        //                index++;
        //            }

        //            if (index < stickerDetails.Count)
        //            {
        //                yPointRight = DrawContentInColumn(gfx, stickerDetails[index], font, XBrushes.Black, xPointRight, initialYPoint, columnWidth, out rightColumnHeight, sp);
        //                index++;
        //            }

        //            // Update yPoint to the maximum height reached by either column
        //            yPoint = initialYPoint + Math.Max(leftColumnHeight, rightColumnHeight) + lineHeight; // Move down to start a new row
        //        }

        //        using (var ms = new MemoryStream())
        //        {
        //            document.Save(ms);
        //            return ms.ToArray();
        //        }
        //    }
        //}

        //private double DrawContentInColumn(XGraphics gfx, (string qrType, string merchantName, string merchantAddress, byte[] barcodeBytes, string pin, string mobile, string mid, string state, string tidnumber) entry, XFont font, XBrush brush, double x, double y, double maxWidth, out double contentHeight, int sp)
        //{
        //    var lineHeight = font.GetHeight();
        //    var initialY = y;
        //    var spacing = 0.5 * lineHeight; // Space equivalent to three lines
        //    var firstspacing = sp;
        //    y += sp;
        //    y = DrawString(gfx, $"QR Type- {entry.qrType}", font, brush, x, y, maxWidth);
        //    y = y - sp;
        //    y = DrawString(gfx, $"Merchant Name:- {entry.merchantName}", font, brush, x, y, maxWidth);
        //    y += spacing; // Add spacing before Merchant Address
        //    y = DrawString(gfx, $"Merchant Address: - {entry.merchantAddress}", font, brush, x, y, maxWidth);
        //    y += spacing; // Add spacing after Merchant Address

        //    // Center TID and tidnumber
        //    var tidText = $"TID: {entry.tidnumber}";
        //    var tidTextSize = gfx.MeasureString(tidText, font);
        //    var centeredX = x + (maxWidth - tidTextSize.Width) / 2;
        //    y = DrawString(gfx, tidText, font, brush, centeredX, y, tidTextSize.Width);

        //    // Center barcode image
        //    using (var ms = new MemoryStream(entry.barcodeBytes))
        //    {
        //        var barcodeImage = XImage.FromStream(() => ms);
        //        var centeredImageX = x + (maxWidth - barcodeImage.PixelWidth) / 2;
        //        gfx.DrawImage(barcodeImage, centeredImageX, y + lineHeight, barcodeImage.PixelWidth, barcodeImage.PixelHeight);
        //        y += barcodeImage.PixelHeight + lineHeight * 2;
        //    }

        //    y = DrawString(gfx, $"Pin: {entry.pin}", font, brush, x, y, maxWidth);

        //    if (entry.mid.Length > 6)
        //    {
        //        y = DrawString(gfx, $"Mobile: {entry.mobile}", font, brush, x, y, maxWidth);
        //        y = DrawString(gfx, $"MID: {entry.mid}", font, brush, x, y, maxWidth);
        //    }
        //    else
        //    {
        //        var textWidth = maxWidth / 2; // Half the width for each column
        //        y = DrawString(gfx, $"Mobile: {entry.mobile}", font, brush, x, y, textWidth);
        //        y = DrawString(gfx, $"MID: {entry.mid}", font, brush, x + textWidth, y - lineHeight, textWidth);
        //    }

        //    y = DrawString(gfx, $"State: {entry.state}", font, brush, x, y, maxWidth);

        //    contentHeight = y - initialY;

        //    return y;
        //}

        //private double DrawString(XGraphics gfx, string text, XFont font, XBrush brush, double x, double y, double maxWidth)
        //{
        //    var words = text.Split(' ');
        //    var currentLine = string.Empty;
        //    var lineHeight = font.GetHeight();

        //    foreach (var word in words)
        //    {
        //        var testLine = string.IsNullOrEmpty(currentLine) ? word : currentLine + " " + word;
        //        var size = gfx.MeasureString(testLine, font);

        //        if (size.Width > maxWidth)
        //        {
        //            gfx.DrawString(currentLine, font, brush, new XRect(x, y, maxWidth, lineHeight), XStringFormats.TopLeft);
        //            y += lineHeight;
        //            currentLine = word;
        //        }
        //        else
        //        {
        //            currentLine = testLine;
        //        }
        //    }

        //    if (!string.IsNullOrEmpty(currentLine))
        //    {
        //        gfx.DrawString(currentLine, font, brush, new XRect(x, y, maxWidth, lineHeight), XStringFormats.TopLeft);
        //        y += lineHeight;
        //    }

        //    return y;
        //}
        //public byte[] GenerateBarcode(string content)
        //{
        //    var writer = new BarcodeWriter<Bitmap>
        //    {
        //        Format = BarcodeFormat.CODE_128,
        //        Options = new EncodingOptions
        //        {
        //            Height = 25,
        //            Width = 120
        //        },
        //        Renderer = new BitmapRenderer() // Set the renderer instance here
        //    };

        //    using (var bitmap = writer.Write(content))
        //    {
        //        using (var stream = new MemoryStream())
        //        {
        //            bitmap.Save(stream, ImageFormat.Png);
        //            return stream.ToArray();
        //        }
        //    }
        //}

        private void btn_save_Click(object sender, EventArgs e)
        {
           // using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
           // {
             //   string path = "";
             //   folderDialog.RootFolder = Environment.SpecialFolder.MyComputer;
             //   folderDialog.SelectedPath = "D:\\"; // Restrict to D drive
              //  DialogResult result = folderDialog.ShowDialog();
              //  if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderDialog.SelectedPath))
              //  {
                    // Display selected folder path in textBox2
              //      path = folderDialog.SelectedPath;
              //  }

                string filepath = txt_browse.Text;

                var tidDetails = _excelService.GetStickerDetails(filepath);
                var stickerDetails = new List<(string qrType, string merchantName, string merchantAddress, byte[], string pin, string mobile, string mid, string state, string tid, string CONTACTPERSONNAME, string AWBNO)>();
                foreach (var detail in tidDetails)
                {
                    var barcodeBytes = _barcodeService.GenerateBarcode(detail.barcodeContent);
                    stickerDetails.Add((detail.qrType, detail.merchantName, detail.merchantAddress, barcodeBytes, detail.pin, detail.mobile, detail.mid, detail.state, detail.tidnumber, detail.CONTACTPERSONNAME, detail.AWBNO));
                }
                var docBytes = _wordDocumentService.CreateDocumentWithStickerDetails(stickerDetails, out var pdfBytes);

                var zipStream = new MemoryStream();
                using (var archive = new System.IO.Compression.ZipArchive(zipStream, System.IO.Compression.ZipArchiveMode.Create, true))
                {
                    var docEntry = archive.CreateEntry("Stickers.docx");
                    using (var entryStream = docEntry.Open())
                    {
                        entryStream.Write(docBytes, 0, docBytes.Length);
                    }

                    var pdfEntry = archive.CreateEntry("Stickers.pdf");
                    using (var entryStream = pdfEntry.Open())
                    {
                        entryStream.Write(pdfBytes, 0, pdfBytes.Length);
                    }
                }

                zipStream.Seek(0, SeekOrigin.Begin);
                using (var saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "ZIP Files|*.zip";
                    saveFileDialog.FileName = "Stickers.zip";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        var savePath = saveFileDialog.FileName;
                        using (var fileStream = new FileStream(savePath, FileMode.Create))
                        {
                            zipStream.CopyTo(fileStream);
                        }

                        MessageBox.Show($"ZIP file saved successfully at {savePath}");
                    }

                }
            //}
        }

        //public class BitmapRenderer : ZXing.Rendering.IBarcodeRenderer<Bitmap>
        //{
        //    public Bitmap Render(BitMatrix matrix, BarcodeFormat format, string content, EncodingOptions options)
        //    {
        //        var width = matrix.Width;
        //        var height = matrix.Height;
        //        var bitmap = new Bitmap(width, height);
        //        for (var y = 0; y < height; y++)
        //        {
        //            for (var x = 0; x < width; x++)
        //            {
        //                var pixelColor = matrix[x, y] ? Color.Black : Color.White;
        //                bitmap.SetPixel(x, y, pixelColor);
        //            }
        //        }
        //        return bitmap;
        //    }

        //    public Bitmap Render(BitMatrix matrix, BarcodeFormat format, string content)
        //    {
        //        return Render(matrix, format, content, null);
        //    }
        //}
    }
}
