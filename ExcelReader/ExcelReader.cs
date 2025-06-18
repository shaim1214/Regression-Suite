using System;
using System.IO;
using System.Data;
using ClosedXML.Excel;
using System.Collections.Generic;

namespace DataHub_Automation.ExcelReader
{
    public class ExcelReaderHelper
    {
        private static IDictionary<string, XLWorkbook> _cache;
        private static XLWorkbook workbook;
        static ExcelReaderHelper()
        {
            _cache = new Dictionary<string, XLWorkbook>();
        }
        public static object GetCellData(string xlPath, string sheetName, int row, int column)
        {
            if (_cache.ContainsKey(sheetName))
            {
                workbook = _cache[sheetName];
            }
            else
            {
                workbook = new XLWorkbook(xlPath);
                _cache.Add(sheetName, workbook);
            }
            var worksheet = workbook.Worksheet(sheetName);
            var cell = worksheet.Cell(row + 1, column + 1);
            return cell.Value;
        }
        public static bool FileEquals(string xlPath2, string xlPath3)
        {
            byte[] file1 = File.ReadAllBytes(xlPath2);
            byte[] file2 = File.ReadAllBytes(xlPath3);
            return ByteArrayCompare(file1, file2);
        }
        private static bool ByteArrayCompare(byte[] array1, byte[] array2)
        {
            if (array1.Length != array2.Length)
                return false;
            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                    return false;
            }
            return true;
        }
    }
}