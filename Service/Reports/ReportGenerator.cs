﻿using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;
using Shared.DataTransferObjects.Documents;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Drawing.Diagrams;

namespace Service.Reports
{
    public class ReportGenerator
    {
        public string baseFolder { get; set; }
        public ReportGenerator(string folder) { baseFolder = folder; }

        public void CreateGeneralReport
            (IEnumerable<RecipientsForReportDto> recipients, 
            string documentName, long versionNumber, string pathFromEntity, string templateFilePath)
        {

            var now = DateTime.Now.ToLocalTime();
            string newName = pathFromEntity.Substring(0, pathFromEntity.LastIndexOf(".")) +".docx";

            string path = GetFilePath(newName);

            File.Copy(templateFilePath, path);

            using (WordprocessingDocument wordDocument 
                = WordprocessingDocument.Open
                (path, true))
            {

                if (wordDocument.MainDocumentPart is null || wordDocument.MainDocumentPart.Document.Body is null)
                {
                    throw new ArgumentNullException("MainDocumentPart and/or Body is null.");
                }
                wordDocument.MainDocumentPart.Document.Body.AppendChild(CreateParagraph(
                    $"Лист ознакомления на " +
                    $"{now.Day}.{now.Month}.{now.Year}"));



                wordDocument.MainDocumentPart.Document.Body
                    .AppendChild(CreateParagraph($"Документ: {documentName}. Версия: {versionNumber}"));
  
                Table table = DesignTable();

                int count = 1;

                //head row
                table.AppendChild(CreateRow([
                       "№",
                        "ФИО",
                        "Должность",
                        "Дата ознакомления"                      
                ]));
                foreach (var el in recipients)
                {
                    var isChecked = el.DateChecked.HasValue ? true : false;
                    var cellValue = "Не ознакомлен";
                    if(isChecked)
                    {
                        var date = el.DateChecked?.ToLocalTime();
                        cellValue = $"{date.Value.Day}.{date.Value.Month}.{date.Value.Year}";
                    }

                    table.AppendChild(CreateRow([
                        count++.ToString(),
                        el.User.FullName ?? "",
                        el.User.PositionName ?? "",
                        cellValue
                        ]));
                }

              

                wordDocument.MainDocumentPart.Document.Body.AppendChild(table);
            
                wordDocument.Save();
            }
        }

        private string GetFilePath(string path)
        {
            bool exists = System.IO.Directory.Exists(baseFolder);
            if (!exists)
                System.IO.Directory.CreateDirectory(baseFolder);
            string docName = path;
            string fullPath = Path.Combine(baseFolder, path);
            return fullPath;
        }

        //create row
        private TableRow CreateRow(string[] values)
        {
            List<TableCell> cells = new List<TableCell>();
            foreach(var it in values)
            {
                cells.Add(CreateCell(it));
            }

            TableRow row = new TableRow();
            foreach (var cell in cells) {
                row.Append(cell);
            }
            return row;
        }

        //create cell
        private TableCell CreateCell(string text)
        {
            TableCell cell = new TableCell();
            cell.Append(new Paragraph(new Run(new Text(text))));
            return cell;
        }
        
        //design table
        private Table DesignTable()
        {
            Table table = new Table();

            TableProperties tblProp = new TableProperties(
                new TableBorders(
                    new TopBorder()
                    {
                        Val =
                        new EnumValue<BorderValues>(BorderValues.Single),
                        Size = 1
                    },
                    new BottomBorder()
                    {
                        Val =
                        new EnumValue<BorderValues>(BorderValues.Single),
                        Size = 1
                    },
                    new LeftBorder()
                    {
                        Val =
                        new EnumValue<BorderValues>(BorderValues.Single),
                        Size = 1
                    },
                    new RightBorder()
                    {
                        Val =
                        new EnumValue<BorderValues>(BorderValues.Single),
                        Size = 1
                    },
                    new InsideHorizontalBorder()
                    {
                        Val =
                        new EnumValue<BorderValues>(BorderValues.Single),
                        Size = 1
                    },
                    new InsideVerticalBorder()
                    {
                        Val =
                        new EnumValue<BorderValues>(BorderValues.Single),
                        Size = 1
                    }
                )
            );

            table.AppendChild<TableProperties>(tblProp);
           
            TableWidth tableWidth = new TableWidth();
            tableWidth.Width = "100%";
            tableWidth.Type = TableWidthUnitValues.Pct;
            table.AppendChild<TableWidth>(tableWidth);

            return table;
        }

        //create paragraph
        private Paragraph CreateParagraph(string text)
        {
            Run run = new Run(new Text(text));

            Paragraph paragraph = new Paragraph();
            ParagraphProperties paragraphProperties = new ParagraphProperties();

            paragraph.ParagraphProperties = new ParagraphProperties();
            paragraph.ParagraphProperties.Justification = new Justification
            {
                Val = JustificationValues.Center
            };
            paragraph.AppendChild( run );
            return paragraph;
        }

    }
}
