using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogNET.Attributes
{
    public class PostPhotoAttribute : ValidationAttribute
    {
        public int MaxSizeMB { get; set; } = 5;
        public override bool IsValid(object value)
        {
            if (value == null) return true;

            IFormFile file = (IFormFile)value;
            if (!file.ContentType.StartsWith("image/"))
            {
                ErrorMessage = "Please select an image file.";
                return false;
            }
            if (file.Length > MaxSizeMB * 1000 * 1000)
            {
                ErrorMessage = $"Image cannot be larger than {MaxSizeMB}MB";
                return false;
            }

            return true;
        }
    }
}
