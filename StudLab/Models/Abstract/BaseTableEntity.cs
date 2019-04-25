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
        [ForeignKey("TransportTask")]
        public int? UserId { get; set; }
        public User User { get; set; }

        //Functions

        public List<List<double>> toList()
        {
            List<List<double>> resultTable = new List<List<double>>();

            var massRowTable = Table.Split(endRow);
            resultTable = massRowTable.Select(row =>
                row.Split(sep)
                .Select(num => double.Parse(num))
                .ToList()
            ).ToList();
            return resultTable;
        }
        public void setTable(List<List<double>> table)
        {
            Table = String.Join(endRow, table.Select(row => String.Join(sep, row)));
        }
        public void setTable(Dictionary<string,string> data)
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
                    rowTable.Add(double.Parse(data[$"m{i}{j}"]));
                }
                table.Add(rowTable);
            }

            setTable(table);
        }
    }
}
