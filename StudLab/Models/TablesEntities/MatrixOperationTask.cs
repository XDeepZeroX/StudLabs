using StudLab.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudLab.Models.TablesEntities
{
    public class MatrixOperationTask : BaseTableEntity
    {
        public MatrixOperationTask()
        {
        }
        public MatrixOperationTask(Dictionary<string, string> data, string matrixOne = "matrixOne")
        {
            setTable(data, matrixOne);
        }
        public MatrixOperationTask(Dictionary<string,string> data, ref int numRow, ref int numCol, string nameMatrix = "matrixTwo") : this(data)
        {
            TableTwo = getTableStr(data, ref numRow, ref numCol, nameMatrix);
        }

        [ForeignKey("MatrixOperationTask")]
        public override int? UserId { get; set; }

        public string TableTwo { get; set; }
        public int NumRowTwo { get; set; }
        public int NumColumnTwo { get; set; }
    }


}
