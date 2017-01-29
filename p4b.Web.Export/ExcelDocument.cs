//------------------------------------------------------------------------------
// <copyright file="ExcelDocument.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// <summary>The Export to Excel Document class.</summary>
//  ----------------------------------------------------------------------------
namespace p4b.Web.Export
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Collections.Generic;
    using DocumentFormat.OpenXml;
    using DocumentFormat.OpenXml.Extensions;
    using DocumentFormat.OpenXml.Packaging;
    using DocumentFormat.OpenXml.Spreadsheet;

    /// <summary>
    /// Excel document.
    /// </summary>
    public static class ExcelDocument<T>
    {
        /// <summary>
        /// Default spread sheet name. 
        /// </summary>
        private const string DefaultSheetName = "Sheet1";

        /// <summary>
        /// Create the exel document for streaming.
        /// </summary>
        /// <param name="documentName">Excel file name.</param>
        /// <param name="excelWorkSheetName">Excel worksheet name: default: sheet1.</param>
        /// <param name="rowData">Row data to write.</param>
        /// <param name="headerData">Header data.</param>
        /// <param name="rowPointers">Row pointers.</param>
        /// <returns>Memory stream.</returns>
        public static MemoryStream Create(string documentName, string excelWorkSheetName, IEnumerable<T> rowData, string[] headerData, string[] rowPointers)
        {
            return CreateSpreadSheet(documentName, excelWorkSheetName, rowData, headerData, rowPointers, null);
        }

        /// <summary>
        /// Create the spreadsheet.
        /// </summary>
        /// <param name="documentName">Excel file name.</param>
        /// <param name="excelWorkSheetName">Excel worksheet name: default: sheet1.</param>
        /// <param name="rowData">Row data to write.</param>
        /// <param name="headerData">Header data.</param>
        /// <param name="rowPointers">Row pointers.</param>
        /// <param name="styleSheet">Style sheet.</param>
        /// <returns>Memory stream.</returns>
        private static MemoryStream CreateSpreadSheet(string documentName, string excelWorkSheetName, IEnumerable<T> rowData, string[] headerData, string[] rowPointers, Stylesheet styleSheet)
        {
            int rowNum = 0;
            int colNum = 0;
            int maxWidth = 0;
            int minCol = 1;
            int maxCol = rowPointers == null ? minCol : rowPointers.Length;
            maxCol = maxCol == 1 && headerData == null ? 1 : headerData.Length;

            MemoryStream xmlStream = SpreadsheetReader.Create();
            SpreadsheetDocument spreadSheet = SpreadsheetDocument.Open(xmlStream, true);

            SetSheetName(excelWorkSheetName, spreadSheet);

            if (styleSheet == null)
            {
                SetStyleSheet(spreadSheet);
            }
            else
            {
                spreadSheet.WorkbookPart.WorkbookStylesPart.Stylesheet = styleSheet;
                spreadSheet.WorkbookPart.WorkbookStylesPart.Stylesheet.Save();
            }

            WorksheetPart worksheetPart = SpreadsheetReader.GetWorksheetPartByName(spreadSheet, excelWorkSheetName);

            WriteHeaders(headerData, out rowNum, out colNum, out maxWidth, spreadSheet, worksheetPart);
            AddCellWidthStyles(Convert.ToUInt32(minCol), Convert.ToUInt32(maxCol), maxWidth, spreadSheet, worksheetPart);

            if (rowPointers == null || rowPointers.Length == 0)
            {
                WriteRowsFromHeaders(rowData, headerData, rowNum, out maxWidth, spreadSheet, worksheetPart);
            }
            else
            {
                WriteRowsFromKeys(rowData, rowPointers, rowNum, out maxWidth, spreadSheet, worksheetPart);
            }

            // Save to the memory stream
            SpreadsheetWriter.Save(spreadSheet);
            spreadSheet.Close();
            spreadSheet.Dispose();
            return xmlStream;
        }

        /// <summary>
        /// Set the name of the spreadsheet. 
        /// </summary>
        /// <param name="excelSpreadSheetName">Spread sheet name.</param>
        /// <param name="spreadSheet">Spread sheet.</param>
        private static void SetSheetName(string excelSpreadSheetName, SpreadsheetDocument spreadSheet)
        {
            excelSpreadSheetName = excelSpreadSheetName ?? DefaultSheetName;

            Sheet ss = spreadSheet.WorkbookPart.Workbook.Descendants<Sheet>().Where(s => s.Name == DefaultSheetName).SingleOrDefault<Sheet>();
            ss.Name = excelSpreadSheetName;
        }

        /// <summary>
        /// Add cell width styles. 
        /// </summary>
        /// <param name="minCol">Minimum column index.</param>
        /// <param name="maxCol">Maximum column index.</param>
        /// <param name="maxWidth">Maximum column width.</param>
        /// <param name="spreadSheet">Spread sheet.</param>
        /// <param name="workSheetPart">Work sheet.</param>
        private static void AddCellWidthStyles(uint minCol, uint maxCol, int maxWidth, SpreadsheetDocument spreadSheet, WorksheetPart workSheetPart)
        {
            Columns cols = new Columns(new Column() { CustomWidth = true, Min = minCol, Max = maxCol, Width = maxWidth, BestFit = false });

            workSheetPart.Worksheet.InsertBefore<Columns>(cols, workSheetPart.Worksheet.GetFirstChild<SheetData>());
        }

        /// <summary>
        /// Set the style sheet.
        // Note: Setting the style here rather than passing it in ensures that all worksheets will have a common user interface design.
        /// </summary>
        /// <param name="spreadSheet">Spread sheet to change.</param>
        private static void SetStyleSheet(SpreadsheetDocument spreadSheet)
        {
            // Note: Setting the style here rather than passing it in ensures that all worksheets will have a common user interface design.
            Stylesheet styleSheet = spreadSheet.WorkbookPart.WorkbookStylesPart.Stylesheet;

            styleSheet.Fonts.AppendChild(
                new Font(new FontSize() { Val = 11 }, new Color() { Rgb = "FFFFFF" }, new FontName() { Val = "Arial" }));

            styleSheet.Fills.AppendChild(new Fill()
            {
                PatternFill = new PatternFill()
                {
                    PatternType = PatternValues.Solid,
                    BackgroundColor = new BackgroundColor() { Rgb = "D8D8D8" }
                }
            });

            spreadSheet.WorkbookPart.WorkbookStylesPart.Stylesheet.Save();
        }

        /// <summary>
        /// Save the styl for worksheet headers. 
        /// </summary>
        /// <param name="cellLocation">Cell location.</param>
        /// <param name="spreadSheet">Spreadsheet to change.</param>
        /// <param name="workSheetPart">Worksheet to change.</param>
        private static void SeatHeaderStyle(string cellLocation, SpreadsheetDocument spreadSheet, WorksheetPart workSheetPart)
        {
            Stylesheet styleSheet = spreadSheet.WorkbookPart.WorkbookStylesPart.Stylesheet;
            Cell cell = workSheetPart.Worksheet.Descendants<Cell>().Where(c => c.CellReference == cellLocation).FirstOrDefault();

            if (cell == null)
            {
                throw new ArgumentNullException("Cell not found");
            }

            cell.SetAttribute(new OpenXmlAttribute("", "s", "", "1"));
            OpenXmlAttribute cellStyleAttribute = cell.GetAttribute("s", "");
            CellFormats cellFormats = spreadSheet.WorkbookPart.WorkbookStylesPart.Stylesheet.CellFormats;

            // pick tthe first cell format.
            CellFormat cellFormat = (CellFormat)cellFormats.ElementAt(0);

            CellFormat cf = new CellFormat(cellFormat.OuterXml);
            cf.FontId = styleSheet.Fonts.Count;
            cf.FillId = styleSheet.Fills.Count;

            cellFormats.AppendChild(cf);

            int a = (int)styleSheet.CellFormats.Count.Value;

            cell.SetAttribute(cellStyleAttribute);

            cell.StyleIndex = styleSheet.CellFormats.Count;

            workSheetPart.Worksheet.Save();
        }

        /// <summary>
        /// Replace special characters. 
        /// </summary>
        /// <param name="value">Value to input.</param>
        /// <returns>Value with special characters replaced.</returns>
        private static string ReplaceSpecialCharacters(string value)
        {
            value = value.Replace("’", "'");
            value = value.Replace("“", "\"");
            value = value.Replace("”", "\"");
            value = value.Replace("–", "-");
            value = value.Replace("…", "...");
            return value;
        }

        /// <summary>
        /// Write values to the spreadsheet.
        /// </summary>
        /// <param name="cellLocation">Row Column Value.</param>
        /// <param name="strValue">Value to write.</param>
        /// <param name="spreadSheet">Spreadsheet to write to. </param>
        /// <param name="workSheet">Worksheet to write to. </param>
        private static void WriteValues(string cellLocation, string strValue, SpreadsheetDocument spreadSheet, WorksheetPart workSheet)
        {
            WorksheetWriter workSheetWriter = new WorksheetWriter(spreadSheet, workSheet);

            int intValue = 0;
            if (strValue.Contains("$"))
            {
                strValue = strValue.Replace("$", "");
                strValue = strValue.Replace(",", "");

                workSheetWriter.PasteValue(cellLocation, strValue, CellValues.Number);
            }
            else if (int.TryParse(strValue, out intValue))
            {
                workSheetWriter.PasteValue(cellLocation, strValue, CellValues.Number);
            }
            else if (string.IsNullOrEmpty(strValue))
            {
                workSheetWriter.PasteText(cellLocation, strValue);
            }
            else
            {
                workSheetWriter.PasteText(cellLocation, strValue);
            }
        }

        /// <summary>
        /// Write the excel rows for the spreadsheet.
        /// </summary>
        /// <param name="rowData">Excel row values.</param>
        /// <param name="rowDataKeys">Excel row-key values.</param>
        /// <param name="rowNum">Row number.</param>
        /// <param name="maxWidth">Max width.</param>
        /// <param name="spreadSheet">Spreadsheet to write to. </param>
        /// <param name="workSheet">Worksheet to write to. </param>
        private static void WriteRowsFromKeys(IEnumerable<T> rowData, string[] rowDataKeys, int rowNum, out int maxWidth, SpreadsheetDocument spreadSheet, WorksheetPart workSheet)
        {
            DateTime t = DateTime.Now;
            System.Diagnostics.Debug.Write(string.Format("ff {0}", (DateTime.Now - t).Ticks)); t = DateTime.Now;
            maxWidth = 0;
            SheetDataSet dsfa = new SheetDataSet();
            SheetData data = new SheetData();
            

            foreach (object row in rowData)
            {
                int colNum = 0;
                t = DateTime.Now; 
                foreach (string rowKey in rowDataKeys)
                {
                    
                    object value = row.GetType().GetProperty(rowKey).GetValue(row, null);

                    string strValue = value == null ? string.Empty : value.ToString();
                    strValue = ReplaceSpecialCharacters(strValue);
                    maxWidth = strValue.Length > maxWidth ? strValue.Length : maxWidth;

                    string cellLocation = string.Format("{0}{1}", GetColumnLetter(colNum.ToString()), rowNum);

                    ExcelDocument<T>.WriteValues(cellLocation, strValue, spreadSheet, workSheet);
                    

                    colNum++;
                }
                System.Diagnostics.Debug.WriteLine(string.Format("1ff {0}", (DateTime.Now - t).Ticks)); t = DateTime.Now;

                rowNum++;
            }
        }
        public static string ToCoNum(int i)
        {
            var ab=Enumerable.Range(65,26).Select(f=>(char)f);
            return IntToStringFast(i, ab.ToArray());
        }
        /// <summary>
        /// An optimized method using an array as buffer instead of 
        /// string concatenation. This is faster for return values having 
        /// a length > 1.
        /// </summary>
        public static string IntToStringFast(int value, char[] baseChars)
        {
            // 32 is the worst cast buffer size for base 2 and int.MaxValue
            int i = 32;
            char[] buffer = new char[i];
            int targetBase = baseChars.Length;

            do
            {
                buffer[--i] = baseChars[value % targetBase];
                value = value / targetBase;
            }
            while (value > 0);

            char[] result = new char[32 - i];
            Array.Copy(buffer, i, result, 0, 32 - i);

            return new string(result);
        }


        /// <summary>
        /// Convert column number to alpha numeric value.
        /// </summary>
        /// <param name="colNumber">Column number.</param>
        /// <returns>ASCII value for number.</returns>
        private static string GetColumnLetter(string colNumber)
        {
            if (string.IsNullOrEmpty(colNumber))
            {
                throw new ArgumentNullException(colNumber);
            }

            string colName = null;

            try
            {
                for (int i = 0; i < colNumber.Length; i++)
                {
                    string colValue = colNumber.Substring(i, 1);

                    int asc = Convert.ToInt16(colValue) + 65;

                    colName += Convert.ToChar(asc);
                }
            }
            finally
            {
                colName = colName ?? "A";
            }

            return colName;
        }

        /// <summary>
        /// Write the values for the rows from headers.
        /// </summary>
        /// <param name="rowData">Excel row values.</param>
        /// <param name="headerData">Excel header values.</param>
        /// <param name="rowNum">Row number.</param>
        /// <param name="maxWidth">Max width.</param>
        /// <param name="spreadSheet">Spreadsheet to write to. </param>
        /// <param name="workSheet">Worksheet to write to. </param>
        private static void WriteRowsFromHeaders(IEnumerable<T> rowData, string[] headerData, int rowNum, out int maxWidth, SpreadsheetDocument spreadSheet, WorksheetPart workSheet)
        {
            WorksheetWriter workSheetWriter = new WorksheetWriter(spreadSheet, workSheet);
            maxWidth = 0;

            foreach (object row in rowData)
            {
                int colNum = 0;
                foreach (string header in headerData)
                {
                    string strValue = row.GetType().GetProperty(header).GetValue(row, null).ToString();
                    strValue = ReplaceSpecialCharacters(strValue);
                    maxWidth = strValue.Length > maxWidth ? strValue.Length : maxWidth;

                    string cellLocation = string.Format("{0}{1}", GetColumnLetter(colNum.ToString()), rowNum);

                    ExcelDocument<T>.WriteValues(cellLocation, strValue, spreadSheet, workSheet);
                    colNum++;
                }

                rowNum++;
            }
        }

        /// <summary>
        /// Write the excel headers for the spreadsheet.
        /// </summary>
        /// <param name="headerData">Excel header values.</param>
        /// <param name="rowNum">Row number.</param>
        /// <param name="colNum">Column Number.</param>
        /// <param name="maxWidth">Max column width</param>
        /// <param name="spreadSheet">Maximum Column Width to write to. </param>
        /// <param name="workSheet">Worksheet to write to. </param>
        private static void WriteHeaders(string[] headerData, out int rowNum, out int colNum, out int maxWidth, SpreadsheetDocument spreadSheet, WorksheetPart workSheet)
        {
            rowNum = 1;
            colNum = 0;
            maxWidth = 0;

            foreach (string header in headerData)
            {
                string strValue = ReplaceSpecialCharacters(header);

                string cellLocation = string.Format("{0}{1}", GetColumnLetter(colNum.ToString()), rowNum);
                maxWidth = strValue.Length > maxWidth ? strValue.Length : maxWidth;
                ExcelDocument<T>.WriteValues(cellLocation, strValue, spreadSheet, workSheet);
                SeatHeaderStyle(cellLocation, spreadSheet, workSheet);
                colNum++;
            }

            rowNum++;
        }
    }
}