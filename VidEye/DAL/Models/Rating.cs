using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Rating
    {
        public Rating()
        {

        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
       // public int UserProfileID { get; set; }
        public int VideoID { get; set; }
        public int Rate { get; set; }
        public DateTime RateDateCreated {get; set;}

        //[ForeignKey("UserProfileID")]
        public int PosterID { get; set; }

        [ForeignKey("VideoID")]
        public VideoTable VideoTables { get; set; }
    }
}
