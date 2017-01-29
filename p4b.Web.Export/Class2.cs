
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Xml;
//using System.Xml.Linq;
//using DocumentFormat.OpenXml.Packaging;
//using OpenXmlPowerTools;


//namespace OpenXmlPowerTools.SpreadsheetWriter
//{
//	public class Class2
//	{


//		private static string _EmptyXlsx = "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";
//		public static void PortfolioReport(Worksheet workSheet, string fileName)
//		{


//			if (fileName == null) throw new ArgumentNullException("fileName");
		

//			FileInfo fi = new FileInfo(fileName);
//			if (fi.Exists)
//				fi.Delete();



//			File.WriteAllBytes(fi.FullName, Convert.FromBase64String(_EmptyXlsx));



//			var sDoc = SpreadsheetDocument.Open(fileName, true);
			

//			WorkbookPart workbookPart = sDoc.WorkbookPart;
//			XDocument wXDoc = workbookPart.GetXDocument();
//			XElement sheetElement = wXDoc
//				.Root
//				.Elements(S.sheets)
//				.Elements(S.sheet)
//				.Where(s => (string)s.Attribute(SSNoNamespace.name) == "Sheet1")
//				.FirstOrDefault();
//			string id = (string)sheetElement.Attribute(R.id);
//			sheetElement.Remove();
//			workbookPart.PutXDocument();

//			WorksheetPart sPart = (WorksheetPart)workbookPart.GetPartById(id);
//			write(sPart);
//		}

//		private static void write(WorksheetPart worksheetPart)
//		{
//			using (Stream partStream = worksheetPart.GetStream(FileMode.Create, FileAccess.Write))
//			{
//				using (XmlWriter partXmlWriter = XmlWriter.Create(partStream))
//				{
//					partXmlWriter.WriteStartDocument();
//					partXmlWriter.WriteStartElement("worksheet", ws);
//					partXmlWriter.WriteStartElement("sheetData", ws);

//					int numColumnHeadingRows = 0;
//					int numColumns = 0;
//					int numColumnsInRows = 0;
//					int numRows;
//					if (worksheetData.ColumnHeadings != null)
//					{
//						Row row = new Row
//						{
//							Cells = worksheetData.ColumnHeadings
//						};
//						SerializeRows(sDoc, partXmlWriter, new[] { row }, 1, out numColumns, out numColumnHeadingRows);
//					}
//					SerializeRows(sDoc, partXmlWriter, worksheetData.Rows, numColumnHeadingRows + 1, out numColumnsInRows,
//						out numRows);
//					int totalRows = numColumnHeadingRows + numRows;
//					int totalColumns = Math.Max(numColumns, numColumnsInRows);
//					if (worksheetData.ColumnHeadings != null && worksheetData.TableName != null)
//					{
//						partXmlWriter.WriteEndElement();
//						string rId2 = "R" + Guid.NewGuid().ToString().Replace("-", "");
//						partXmlWriter.WriteStartElement("tableParts", ws);
//						partXmlWriter.WriteStartAttribute("count");
//						partXmlWriter.WriteValue(1);
//						partXmlWriter.WriteEndAttribute();
//						partXmlWriter.WriteStartElement("tablePart", ws);
//						partXmlWriter.WriteStartAttribute("id", relns);
//						partXmlWriter.WriteValue(rId2);
//						TableDefinitionPart tdp = worksheetPart.AddNewPart<TableDefinitionPart>(rId2);
//						XDocument tXDoc = tdp.GetXDocument();
//						XElement table = new XElement(S.table,
//							new XAttribute(SSNoNamespace.id, 1),
//							new XAttribute(SSNoNamespace.name, worksheetData.TableName),
//							new XAttribute(SSNoNamespace.displayName, worksheetData.TableName),
//							new XAttribute(SSNoNamespace._ref, "A1:" + SpreadsheetMLUtil.IntToColumnId(totalColumns - 1) + totalRows.ToString()),
//							new XAttribute(SSNoNamespace.totalsRowShown, 0),
//							new XElement(S.autoFilter,
//								new XAttribute(SSNoNamespace._ref, "A1:" + SpreadsheetMLUtil.IntToColumnId(totalColumns - 1) + totalRows.ToString())),
//							new XElement(S.tableColumns,
//								new XAttribute(SSNoNamespace.count, totalColumns),
//								worksheetData.ColumnHeadings.Select((ch, i) =>
//									new XElement(S.tableColumn,
//										new XAttribute(SSNoNamespace.id, i + 1),
//										new XAttribute(SSNoNamespace.name, ch.Value)))),
//							new XElement(S.tableStyleInfo,
//								new XAttribute(SSNoNamespace.name, "TableStyleMedium2"),
//								new XAttribute(SSNoNamespace.showFirstColumn, 0),
//								new XAttribute(SSNoNamespace.showLastColumn, 0),
//								new XAttribute(SSNoNamespace.showRowStripes, 1),
//								new XAttribute(SSNoNamespace.showColumnStripes, 0)));
//						tXDoc.Add(table);
//						tdp.PutXDocument();
//					}
//				}
//			}
//		}
//		private static void SerializeRow(XmlWriter xw, int rowCount, Row row, out int numColumns)
//		{
//			string ns = S.s.NamespaceName;

//			xw.WriteStartElement("row", ns);
//			xw.WriteStartAttribute("r");
//			xw.WriteValue(rowCount);
//			xw.WriteEndAttribute();
//			xw.WriteStartAttribute("spans");
//			xw.WriteValue("1:" + row.Cells.Count().ToString());
//			xw.WriteEndAttribute();
//			int cellCount = 0;
//			int cellSpan = 0;
//			foreach (var cell in row.Cells)
//			{
//				if (cell != null)
//				{
//					xw.WriteStartElement("c", ns);
//					xw.WriteStartAttribute("r");
//					xw.WriteValue(SpreadsheetMLUtil.IntToColumnId(cellCount) + rowCount.ToString());
//					xw.WriteEndAttribute();
//					//if (cell.Bold != null ||
//					//	cell.Italic != null ||
//					//	cell.FormatCode != null ||
//					//	cell.HorizontalCellAlignment != null)
//					//{
//					//	xw.WriteStartAttribute("s");
//					//	xw.WriteValue(GetCellStyle(sDoc, cell));
//					//	xw.WriteEndAttribute();
//					//}
//					xw.WriteStartAttribute("s");
//					xw.WriteValue(0);
//					xw.WriteEndAttribute();

//					switch (cell.CellDataType)
//					{
//						case CellDataType.Boolean:
//							xw.WriteStartAttribute("t");
//							xw.WriteValue("b");
//							xw.WriteEndAttribute();
//							break;
//						case CellDataType.Date:
//							xw.WriteStartAttribute("t");
//							xw.WriteValue("d");
//							xw.WriteEndAttribute();
//							break;
//						case CellDataType.Number:
//							xw.WriteStartAttribute("t");
//							xw.WriteValue("n");
//							xw.WriteEndAttribute();
//							break;
//						case CellDataType.String:
//							xw.WriteStartAttribute("t");
//							xw.WriteValue("str");
//							xw.WriteEndAttribute();
//							break;
//						default:
//							xw.WriteStartAttribute("t");
//							xw.WriteValue("str");
//							xw.WriteEndAttribute();
//							break;
//					}
//					if (cell.Value != null)
//					{
//						xw.WriteStartElement("v", ns);
//						xw.WriteValue(cell.Value);
//						xw.WriteEndElement();
//					}
//					xw.WriteEndElement();
//				}
//				cellCount++;
//			}
//			xw.WriteEndElement();
//			numColumns = cellCount;
//		}

//	}
//}
