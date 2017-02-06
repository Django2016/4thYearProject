using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
   public class ShareTable
    {
        public ShareTable()
        {

        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set;}
        public int VideoID { get; set; }
        //public int UserProfileID { get; set; }

        [ForeignKey("VideoID")]
        public VideoTable VideoTable { get; set; }

       // [ForeignKey("UserProfileID")]
        public UserProfile UserProfile { get; set; }
    }
}
