using StudLab.Models.TablesEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudLab.Model
{
    public class User : BaseEntity
    {
        public string EmailUser { get; set; } //Email пользователя
        
        public List<TableTransportTask> TransportTables { get; set; }
        public List<MatrixOperationTask> MatrixOperationTables { get; set; }
        public List<MultiCriteriaTask> MultiCriteriaTables { get; set; }

        public User()
        {
            TransportTables = new List<TableTransportTask>();
            MatrixOperationTables = new List<MatrixOperationTask>();
            MultiCriteriaTables = new List<MultiCriteriaTask>();
        }
        public User(string email) : base()
        {
            this.EmailUser = email;
        }

    }
}
