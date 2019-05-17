using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AuthServiceServer.Models.Data
{
    public class DataTypeUserNameValidationAttribute : ValidationAttribute
    {
        private Regex _regex = new Regex(@"^[а-яА-ЯA-Za-zёЁ]+$");
        private const int minLenName = 2;
        private const int maxLenName = 20;


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var val = value.ToString();
            if (_regex.IsMatch(val) &&
                val.Length >= minLenName &&
                val.Length <= maxLenName)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Имя должно содержать только буквенные символы и быть от 2 до 20 символов");
        }
    }
}
