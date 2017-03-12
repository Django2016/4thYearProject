using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class VideoTable
    {
        public VideoTable()
        {

        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(200)]
        public string VideoDesc { get; set; }

        [MaxLength(150)]
        public string Thumbnail { get; set; }

        public int UserProfileID { get; set; }

        public string URL { get; set; }
        public DateTime VideoDateCreated { get; set; }

        [ForeignKey("UserProfileID")]
        public UserProfile UserProfile { get; set; }

        public ICollection<VideoComment> VideoComments { get; set; }

        public ICollection<LikeTable> Likes { get; set; }

        public ICollection<ShareTable> Shares { get; set; }

        //public ICollection<VideoSubscription> VideoSubscriptions { get; set; }

        public ICollection<Rating> Ratings { get; set; }

    }
}
