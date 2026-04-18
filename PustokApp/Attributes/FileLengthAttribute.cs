using System.ComponentModel.DataAnnotations;

namespace PustokApp.Attributes
{
    public class FileLengthAttribute : ValidationAttribute
    {
        public int Length { get; set; }
        public FileLengthAttribute(int length)
        {
            Length = length;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            List<IFormFile> infos = new List<IFormFile>(); 

            if (value is List<IFormFile> files)
                infos = files;
            else if (value is IFormFile file)
                infos.Add(file); 

            foreach (var file in infos)
            {
                if (file.Length > Length * 1024 * 1024)
                {
                    return new ValidationResult($"File size must be less than {Length}MB.");
                }
            }

            return ValidationResult.Success; 
        }
    }
}
