using StudLab.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudLab.Models.TablesEntities
{
    public class MultiCriteriaTask : BaseTableEntity
    {

        public MultiCriteriaTask()
        {
        }
        public MultiCriteriaTask(Dictionary<string, string> data)
        {
            setTable(data);
        }

        [ForeignKey("MultiCriteriaTask")]
        public override int? UserId { get; set; }
    }
}