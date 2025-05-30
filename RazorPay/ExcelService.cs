using NLog;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorPay
{
    public class ExcelService
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public List<(string qrType, string merchantName, string merchantAddress, string barcodeContent, string pin, string mobile, string mid, string state, string tidnumber, string CONTACTPERSONNAME, string AWBNO)> GetStickerDetails(string filePath)
        {
            
                var stickerDetails = new List<(string, string, string, string, string, string, string, string, string, string, string)>();
            try
            {

                using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    IWorkbook workbook;

                    // Check the file extension to determine which NPOI class to use
                    if (Path.GetExtension(filePath).ToLower() == ".xls" || Path.GetExtension(filePath).ToLower() == ".xlsx")
                    {
                        workbook = new HSSFWorkbook(stream); // for .xls
                    }
                    else
                    {
                        workbook = new XSSFWorkbook(stream); // for .xlsx
                    }

                    var sheet = workbook.GetSheetAt(0); // Get the first sheet
                    var headerIndices = new Dictionary<string, int>();
                    var headerRow = sheet.GetRow(0);
                    List<string> header = new List<string>();
                    for (int i = 0; i < headerRow.LastCellNum; i++)
                    {
                        var cellValue = headerRow.GetCell(i)?.ToString();
                        if (!string.IsNullOrEmpty(cellValue))
                        {
                            headerIndices[cellValue] = i;
                        }
                    }

                    for (int rowIndex = 1; rowIndex <= sheet.LastRowNum; rowIndex++)
                    {
                        var currentRow = sheet.GetRow(rowIndex);
                        if (currentRow == null) continue; // skip empty rows

                        string qrType = "NTB";
                        string merchantName = GetCellValue(currentRow, headerIndices, "Merchantname");
                        Logger.Info($"{merchantName}");
                        string mid = GetCellValue(currentRow, headerIndices, "MasterID");
                        Logger.Info($"{mid}");
                        string Awbnumber = GetCellValue(currentRow, headerIndices, "AWBNO");
                        Logger.Info($"{Awbnumber}");
                        string barcodeContent = !string.IsNullOrEmpty(Awbnumber) ? Awbnumber : GetCellValue(currentRow, headerIndices, "TID");
                        string merchantAddress = GetCellValue(currentRow, headerIndices, "Address");
                        Logger.Info($"{merchantAddress}");
                        string pin = GetCellValue(currentRow, headerIndices, "PINCODE");
                        Logger.Info($"{pin}");
                        string state = GetCellValue(currentRow, headerIndices, "STATE");
                        Logger.Info($"{state}");
                        string mobile = GetCellValue(currentRow, headerIndices, "MOBILE");
                        Logger.Info($"{mobile}");
                        string tidnumber = GetCellValue(currentRow, headerIndices, "TID");
                        Logger.Info($"{tidnumber}");
                        string contactPersonName = GetCellValue(currentRow, headerIndices, "CONTACT PERSON NAME");
                        Logger.Info($"{contactPersonName}");

                        // Do something with the data...
                        if (!string.IsNullOrEmpty(qrType) && !string.IsNullOrEmpty(merchantName) &&
                                  !string.IsNullOrEmpty(merchantAddress) && !string.IsNullOrEmpty(barcodeContent) &&
                                  !string.IsNullOrEmpty(pin) && !string.IsNullOrEmpty(mobile) &&
                                  !string.IsNullOrEmpty(mid) && !string.IsNullOrEmpty(state) && !string.IsNullOrEmpty(tidnumber) || !string.IsNullOrEmpty(contactPersonName) || !string.IsNullOrEmpty(Awbnumber))
                        {
                            stickerDetails.Add((qrType, merchantName, merchantAddress, barcodeContent, pin, mobile, mid, state, tidnumber, contactPersonName, Awbnumber));
                        }
                    }
                }
                return stickerDetails;
            }
            catch (Exception ex)
            {
                Logger.Info($"{ex.ToString()}");
                return stickerDetails;

            }
           
        }

        private string GetCellValue(IRow row, Dictionary<string, int> headerIndices, string headerName)
        {
            if (headerIndices.TryGetValue(headerName, out int columnIndex))
            {
                return row.GetCell(columnIndex)?.ToString();
            }
            return null;
        }


    }
}
