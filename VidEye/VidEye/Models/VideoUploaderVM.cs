using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace VidEye.Models
{
    public class VideoUploaderVM : IValidatableObject
    {
        [Required]
        public HttpPostedFileBase UploadFile { get; set; }
        [Required]
        public string VideoTitle { get; set; }
        [Required]
        public string VideoDescription { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            /*Validate the file extensions*/
            if (!UploadFile.FileName.Contains("mp4"))
                yield return new ValidationResult("Please upload the supported files", new[] { "UploadFile" });
        }
    }
}
