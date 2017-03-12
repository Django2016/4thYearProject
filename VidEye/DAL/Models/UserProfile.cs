using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class UserProfile
    {
        public UserProfile()
        {

        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set;}
        [MaxLength(50)]
        public string Fname { get; set; }
        [MaxLength(50)]
        public string Lname { get; set; }
        public DateTime? Dob { get; set; }
        [MaxLength(50)]
        public string PhoneNumber { get; set; }
        [MaxLength(10)]
        public string Gender { get; set; }
        [MaxLength(150)]
        public string Address { get; set; }
        [MaxLength(128)]
        public string MemebershipID { get; set; }

        public ICollection<VideoTable> Videos { get; set; }

        public ICollection<VideoComment> VideoComments { get; set; }

        //public ICollection<LikeTable> Likes { get; set; }

        //public ICollection<ShareTable> Shares { get; set; }
        
        //public ICollection<UserStatus> Statuses { get; set; }
        
        //public ICollection<Rating> Ratings { get; set; } 

    }
}
