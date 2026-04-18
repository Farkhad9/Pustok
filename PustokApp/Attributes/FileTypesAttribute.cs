using System.ComponentModel.DataAnnotations;

namespace PustokApp.Attributes
{
    public class FileTypesAttribute: ValidationAttribute
    {
        public string[] Types { get; set; }
        public FileTypesAttribute(params string[] types)
        {
            Types = types;
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
                if (!Types.Contains(file.ContentType))
                {
                    return new ValidationResult($"File type must be one of the following: {string.Join(", ", Types)}.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
