using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudLab.Model.Abstract
{
    public class BaseViewEntity : IEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}
