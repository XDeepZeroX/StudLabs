using StudLab.Model;
using StudLab.Models.TablesEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudLab.Expansion
{
    public static class Expansion
    {
        public static bool IsEquals(this TableTransportTask This, TableTransportTask Other)
        {
            return This == Other;
        }
        public static bool IsEquals(this BaseTableEntity This, BaseTableEntity Other)
        {
            return This == Other;
        }
        public static bool IsEquals(this MatrixOperationTask This, MatrixOperationTask Other)
        {
            return This == Other;
        }
        public static bool IsEquals(this MultiCriteriaTask This, MultiCriteriaTask Other)
        {
            return This == Other;
        }
    }
}
