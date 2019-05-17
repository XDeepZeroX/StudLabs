using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudLab.Model
{
    public class BaseTableEntity : BaseEntity
    {
        protected const char sep = ';',
                           endRow = '#';

        //Сама таблица
        public string Table { get; set; }
        public int NumRow { get; set; }
        public  int NumColumn { get; set; }

        public DateTime Date { get; set; }

        //Пользователь
        public virtual int? UserId { get; set; }
        public User User { get; set; }

        //Functions

        public List<List<double>> ToList()
        {
            return GetMatrix(Table);
            //List<List<double>> resultTable = new List<List<double>>();

            //var massRowTable = Table.Split(endRow);
            //resultTable = massRowTable.Select(row =>
            //    row.Split(sep)
            //    .Select(num => double.Parse(num))
            //    .ToList()
            //).ToList();
            //return resultTable;
        }
        public List<List<double>> GetMatrix(string strMatrix)
        {
            List<List<double>> resultTable = new List<List<double>>();

            var massRowTable = strMatrix.Split(endRow);
            resultTable = massRowTable.Select(row =>
                row.Split(sep)
                .Select(num => double.Parse(num))
                .ToList()
            ).ToList();
            return resultTable;
        }

        public string getTableStr(List<List<double>> table)
        {
            return String.Join(endRow, table.Select(row => String.Join(sep, row))); ;
        }
        public string getTableStr(Dictionary<string,string> data, ref int numRow, ref int numCol, string nameMatrix = "m")
        {
            List<List<double>> table = new List<List<double>>();
            int row = int.Parse(data["row"]),
                column = int.Parse(data["column"]);
            numRow = row;
            numCol = column;

            for (int i = 1; i <= row; i++)
            {
                List<double> rowTable = new List<double>();
                for (int j = 1; j <= column; j++)
                {
                    rowTable.Add(double.Parse(data[$"{nameMatrix}{i}{j}"]));
                }
                table.Add(rowTable);
            }

            return getTableStr(table);
        }
        public void setTable(List<List<double>> table)
        {
            var result = String.Join(endRow, table.Select(row => String.Join(sep, row)));
            Table = result;
        }
        public void setTable(Dictionary<string,string> data, string nameMatrix = "m")
        {
            List<List<double>> table = new List<List<double>>();
            int row = int.Parse(data["row"]),
                column = int.Parse(data["column"]);
            this.NumRow = row;
            this.NumColumn = column;

            for(int i = 1; i <= row; i++)
            {
                List<double> rowTable = new List<double>();
                for(int j = 1; j <= column; j++)
                {
                    rowTable.Add(double.Parse(data[$"{nameMatrix}{i}{j}"]));
                }
                table.Add(rowTable);
            }

            setTable(table);
        }


        public static bool operator ==(BaseTableEntity This, BaseTableEntity Other)
        {
            if (object.ReferenceEquals(This, null))
            {
                if (object.ReferenceEquals(Other, null))
                    return true;
                return false;
            }
            else if (object.ReferenceEquals(Other, null))
            {
                return false;
            }
            return This.Table == Other.Table &&
                This.NumRow == Other.NumRow &&
                This.NumColumn == Other.NumColumn;
        }
        public static bool operator !=(BaseTableEntity This, BaseTableEntity Other)
        {
            return !(This == Other);
        }
    }
}
