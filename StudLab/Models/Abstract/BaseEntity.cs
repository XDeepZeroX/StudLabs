using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudLab.Model
{
    public class BaseEntity : IEntity
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        [Display(AutoGenerateField =false)]
        public int Id { get; set; }

        [HiddenInput(DisplayValue = false)]
        [Display(AutoGenerateField = false)]
        public bool IsDeleted { get; set; }

        //public string EmailUser { get; set; }



    }
}
