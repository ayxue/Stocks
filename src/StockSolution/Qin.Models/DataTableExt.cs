using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qin.Html
{
    public static class DataTableExt
    {
        public static DataColumn GetColumn(this DataTable table, string columnName)
        {
            foreach (DataColumn column in table.Columns)
                if (column.ColumnName.StartsWith(columnName))
                    return column;

            return null;
        }

    }
}
