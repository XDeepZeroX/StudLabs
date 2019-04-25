using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudLab.Model
{
    public interface IEntity
    {
        [Key]
        int Id { get; set; } //Primary key

        bool IsDeleted { get; set; }
        //string EmailUser { get; set; }
    }
}
