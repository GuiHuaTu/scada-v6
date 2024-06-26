﻿// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Xml;

namespace Scada.Report.Xml2003.Excel
{
    /// <summary>
    /// Represents an Excel workbook.
    /// <para>Представляет книгу Excel.</para>
    /// </summary>
    public class Workbook
    {
        /// <summary>
        /// Ссылка на XML-узел, соответствующий книге Excel.
        /// </summary>
        protected XmlNode node;
        /// <summary>
        /// Ссылка на XML-узел, содержащий стили книги Excel.
        /// </summary>
        protected XmlNode stylesNode;
        /// <summary>
        /// Список стилей книги Excel с возможностью доступа по ID стиля.
        /// </summary>
        protected SortedList<string, Style> styles;
        /// <summary>
        /// Список листов книги Excel.
        /// </summary>
        protected List<Worksheet> worksheets;


        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="xmlNode">Ссылка на XML-узел, соответствующий книге Excel.</param>
        public Workbook(XmlNode xmlNode)
        {
            node = xmlNode;
            styles = new SortedList<string, Style>();
            worksheets = new List<Worksheet>();
        }


        /// <summary>
        /// Получить ссылку на XML-узел, соответствующий книге Excel.
        /// </summary>
        public XmlNode Node
        {
            get
            {
                return node;
            }
        }

        /// <summary>
        /// Получить или установить ссылку на XML-узел, содержащий стили книги Excel.
        /// </summary>
        public XmlNode StylesNode
        {
            get
            {
                return stylesNode;
            }
            set
            {
                stylesNode = value;
            }
        }

        /// <summary>
        /// Получить список стилей книги Excel с возможностью доступа по ID стиля.
        /// </summary>
        public SortedList<string, Style> Styles
        {
            get
            {
                return styles;
            }
        }

        /// <summary>
        /// Получить список листов книги Excel.
        /// </summary>
        public List<Worksheet> Worksheets
        {
            get
            {
                return worksheets;
            }
        }


        /// <summary>
        /// Добавить стиль в конец списка стилей книги Excel и модифицировать дерево XML-документа.
        /// </summary>
        /// <param name="style">Добавляемый стиль.</param>
        public void AppendStyle(Style style)
        {
            styles.Add(style.ID, style);
            stylesNode.AppendChild(style.Node);
        }

        /// <summary>
        /// Удалить стиль из списка стилей книги Excel и модифицировать дерево XML-документа.
        /// </summary>
        /// <param name="listIndex">Индекс удаляемого стиля в списке.</param>
        public void RemoveStyle(int listIndex)
        {
            Style style = styles.Values[listIndex];
            stylesNode.RemoveChild(style.Node);
            styles.RemoveAt(listIndex);
        }

        /// <summary>
        /// Найти лист в списке листов Excel по имени без учёта регистра.
        /// </summary>
        /// <param name="worksheetName">Имя листа Excel.</param>
        /// <returns>Лист Excel или null, если он не найден.</returns>
        public Worksheet FindWorksheet(string worksheetName)
        {
            foreach (Worksheet worksheet in worksheets)
                if (worksheet.Name.Equals(worksheetName, StringComparison.OrdinalIgnoreCase))
                    return worksheet;

            return null;
        }

        /// <summary>
        /// Добавить лист в конец списка листов Excel и модифицировать дерево XML-документа.
        /// </summary>
        /// <param name="worksheet">Добавляемый лист.</param>
        public void AppendWorksheet(Worksheet worksheet)
        {
            worksheets.Add(worksheet);
            node.AppendChild(worksheet.Node);
        }

        /// <summary>
        /// Вставить лист в список листов Excel и модифицировать дерево XML-документа.
        /// </summary>
        /// <param name="listIndex">Индекс вставляемого листа в списке.</param>
        /// <param name="worksheet">Вставляемый лист.</param>
        public void InsertWorksheet(int listIndex, Worksheet worksheet)
        {
            worksheets.Insert(listIndex, worksheet);

            if (worksheets.Count == 1)
                node.AppendChild(worksheet.Node);
            else if (listIndex == 0)
                node.PrependChild(worksheet.Node);
            else
                node.InsertAfter(worksheet.Node, worksheets[listIndex - 1].Node);
        }

        /// <summary>
        /// Удалить лист из списка листов Excel и модифицировать дерево XML-документа.
        /// </summary>
        /// <param name="listIndex">Индекс удаляемого листа в списке.</param>
        public void RemoveWorksheet(int listIndex)
        {
            Worksheet worksheet = worksheets[listIndex];
            node.RemoveChild(worksheet.Node);
            worksheets.RemoveAt(listIndex);
        }

        /// <summary>
        /// Sets the object color, creating a new style if necessary.
        /// </summary>
        /// <param name="targetNode">Reference to the XML node of the object which color is set.</param>
        /// <param name="backColor">Background color to set.</param>
        /// <param name="fontColor">Font color to set.</param>
        public void SetColor(XmlNode targetNode, string backColor, string fontColor)
        {
            XmlDocument xmlDoc = targetNode.OwnerDocument;
            XmlNamespaceManager nsmgr = new(new NameTable());
            nsmgr.AddNamespace("report", XmlNamespaces.Default);

            XmlAttribute styleAttr = targetNode.Attributes["ss:StyleID"];
            string oldStyleID;

            if (styleAttr == null)
            {
                styleAttr = xmlDoc.CreateAttribute("ss", "StyleID", targetNode.NamespaceURI);
                targetNode.Attributes.Append(styleAttr);
                oldStyleID = "";
            }
            else
            {
                oldStyleID = styleAttr.Value;
            }

            string newStyleID = (oldStyleID == "" ? "CustomStyle" : oldStyleID) + "_" +
                (string.IsNullOrEmpty(backColor) ? "none" : backColor) + "_" +
                (string.IsNullOrEmpty(fontColor) ? "none" : fontColor);

            if (styles.ContainsKey(newStyleID))
            {
                // set the previously created style having the specified color
                styleAttr.Value = newStyleID;
            }
            else
            {
                // create a new style
                Style newStyle;

                if (styles.TryGetValue(oldStyleID, out Style oldStyle))
                {
                    newStyle = oldStyle.Clone();
                }
                else
                {
                    XmlNode newStyleNode = xmlDoc.CreateElement("Style", XmlNamespaces.Default);
                    newStyleNode.Attributes.Append(xmlDoc.CreateAttribute("ss", "ID", XmlNamespaces.Default));
                    newStyle = new Style(newStyleNode);
                }

                newStyle.ID = newStyleID;

                // set background color of the style
                if (!string.IsNullOrEmpty(backColor))
                {
                    XmlNode interiorNode = newStyle.Node.SelectSingleNode("report:Interior", nsmgr);

                    if (interiorNode == null)
                    {
                        interiorNode = xmlDoc.CreateElement("Interior", XmlNamespaces.Default);
                        newStyle.Node.AppendChild(interiorNode);
                    }

                    XmlAttribute colorAttr = xmlDoc.CreateAttribute("ss", "Color", XmlNamespaces.Default);
                    colorAttr.Value = backColor;
                    interiorNode.Attributes.Append(colorAttr);

                    XmlAttribute patternAttr = xmlDoc.CreateAttribute("ss", "Pattern", XmlNamespaces.Default);
                    patternAttr.Value = "Solid";
                    interiorNode.Attributes.Append(patternAttr);
                }

                // set font color of the style
                if (!string.IsNullOrEmpty(fontColor))
                {
                    XmlNode fontNode = newStyle.Node.SelectSingleNode("report:Font", nsmgr);

                    if (fontNode == null)
                    {
                        fontNode = xmlDoc.CreateElement("Font", XmlNamespaces.Default);
                        newStyle.Node.AppendChild(fontNode);
                    }

                    XmlAttribute colorAttr = xmlDoc.CreateAttribute("ss", "Color", XmlNamespaces.Default);
                    colorAttr.Value = fontColor;
                    fontNode.Attributes.Append(colorAttr);
                }

                // set the new style to the node and add the style to the workbook
                styleAttr.Value = newStyleID;
                styles.Add(newStyleID, newStyle);
                stylesNode.AppendChild(newStyle.Node);
            }
        }
    }
}
