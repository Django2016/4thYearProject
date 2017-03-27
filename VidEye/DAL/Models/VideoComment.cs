using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class VideoComment
    {
        public VideoComment()
        {

        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        
        //public int UserProfileID { get; set; }
        public string VideoCommentDesc { get; set; }
        public DateTime VideoCommentDateCreated { get; set; }

        public int PosterID { get; set; }

       // [ForeignKey("PosterID")]
        public UserProfile UserProfile { get; set; }

        public int VideoID { get; set; }
        [ForeignKey("VideoID")]
        public VideoTable VideoTable{ get; set; }
    }
}
