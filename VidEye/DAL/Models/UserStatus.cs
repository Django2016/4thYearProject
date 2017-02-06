using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class UserStatus
    {
        public UserStatus()
        {

        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string UserStatusDesc { get; set; }
        public int UserProfileID { get; set; }
        public DateTime UserStatusDateCreated { get; set; }

        [ForeignKey("UserProfileID")]
        public UserProfile UserProfile { get; set; }
    }
}
