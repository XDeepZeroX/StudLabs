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

        public User()
        {
            TransportTables = new List<TableTransportTask>();
        }
        public User(string email) : base()
        {
            this.EmailUser = email;
        }

    }
}
