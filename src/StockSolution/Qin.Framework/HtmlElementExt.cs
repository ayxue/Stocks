using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Qin.Html
{
    public static class HtmlElementExt
    {
        public static string Value(this HtmlNode node)
        {
            return node.GetAttributeValue("value", string.Empty);
        }

        public static string Class(this HtmlNode node)
        {
            return node.GetAttributeValue("class", string.Empty);
        }

        public static IEnumerable<string> Value(this IEnumerable<HtmlNode> nodes)
        {
            return nodes.Select( n => n.Value());
        }

        public static IEnumerable<string> Text(this IEnumerable<HtmlNode> nodes)
        {
            return nodes.Select(n => n.InnerText);
        }

        public static IEnumerable<HtmlNode> Y(this HtmlDocument doc, string query)
        {
            return doc.DocumentNode.Y(query);
        }

        public static IEnumerable<HtmlNode> Y(this HtmlNode root, string query)
        {
            var xpath = root.ParentNode == null? QueryToXPath(query) : root.XPath + QueryToXPath(query);
            var ret = root.SelectNodes(xpath);

            if (ret == null)
                return Enumerable.Empty<HtmlNode>();

            return ret;
        }


        public static IEnumerable<HtmlNode> Y(this IEnumerable<HtmlNode> nodes, string query)
        {
            var ret = new List<HtmlNode>();
            foreach(var node in nodes)
            {
                var result = node.Y(query);
                ret.AddRange(result);
            }

            return ret;
        }

        public static DataTable ToDataTableByColumn(this IEnumerable<HtmlNode> rows, bool hasColumn)
        {
            var table = new DataTable();
            int defaultColumnName = 0;
            var rowArray = rows.ToArray();
            while (table.Columns.Count <= rowArray.Count())
            {
                table.Columns.Add(defaultColumnName.ToString());
                defaultColumnName++;
            }

            for(int i = 0; i < rowArray.Length; i ++)
            {
                var row = rowArray[i];
                var cells = row.Y("td").ToArray();
                for(int j = 0; j < cells.Length; j ++)
                {
                    var val = cells[j].Text();
                    if(table.Rows.Count - 1 < j)
                        table.Rows.Add(table.NewRow());

                    table.Rows[j][i] = val;                    
                }            
            }

            if(hasColumn && table.Rows.Count > 1)
            {
                var row = table.Rows[0];
                for(int i = table.Columns.Count - 1; i >= 0 ; i --)
                {
                    var name = row[i].ToString();
                    if (string.IsNullOrEmpty(name))
                        table.Columns.RemoveAt(i);
                    else
                        table.Columns[i].ColumnName = name;
                }

                table.Rows.RemoveAt(0);
            }

            return table;
        }

        public static string Text(this HtmlNode node)
        {
            return node.InnerText.Replace("&nbsp;", "").Replace("·", "").Trim();
        }


        public static string QueryToXPath(string query)
        {
            var parts = query.Trim().Split(' ');
            var xpathes = new List<string>();


            for(int i = 0; i < parts.Length; i ++)
            {
                var path = ConvertQuery(parts[i]);
                if (!string.IsNullOrEmpty(path))
                    xpathes.Add(path);
            }

            return @"//" + string.Join("//", xpathes);
        }

        public static string ConvertQuery(string query)
        {
            var subQuery = query.Trim();
            if (string.IsNullOrEmpty(subQuery))
                return string.Empty;

            // class
            if(subQuery.IndexOf(".") >= 0)
            {
                var index = subQuery.IndexOf(".");
                var classPart = subQuery.Substring(index + 1, subQuery.Length - index - 1);
                var mainPart = subQuery.Substring(0, index);
                return ConvertQuery(mainPart) + string.Format("[@class='{0}']", classPart);
            }

            // id
            if(subQuery.IndexOf("#") >= 0)
            {
                var index = subQuery.IndexOf("#");
                var idPart = subQuery.Substring(index + 1, subQuery.Length - index - 1);
                var mainPart = subQuery.Substring(0, index);
                return ConvertQuery(mainPart) + string.Format("[@id='{0}']", idPart);
            }

            // attr
            if (subQuery.IndexOf("[") >= 0)
            {
                var beginIndex = subQuery.IndexOf("[");
                var endIndex = subQuery.IndexOf("]", beginIndex);
                var attrPart = subQuery.Substring(beginIndex + 1, endIndex - beginIndex - 1);
                var mainPart = subQuery.Substring(0, beginIndex);

                if (!char.IsDigit(attrPart[0]) && !attrPart.EndsWith("()"))
                    attrPart = "@" + attrPart;

                return ConvertQuery(mainPart) + string.Format("[{0}]", attrPart);
            }

            return query;
        }
    }
}
