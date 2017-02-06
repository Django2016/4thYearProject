using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
   public  class LikeTable
    {
        public LikeTable()
        {

        }
        [Key]
        public int ID { get; set; }
        public int VideoID { get; set; }
       // public int UserProfileID { get; set; }
        public int LikeDesc { get; set; }
        public DateTime LikeDateCreated { get; set; }

       // [ForeignKey("UserProfileID")]
        public UserProfile UserProfile { get; set; }

        [ForeignKey("VideoID")]
        public VideoTable VideoTable { get; set; }


    }
}
