using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Subscription
    {
        public Subscription()
        {

        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        //public int TypeID { get; set; }
        public double Price { get; set; }
        public string SubscriptionDesc { get; set; }
        public DateTime SubscriptionDateCreated { get; set; }

        //public ICollection<VideoSubscription> VideoSubscription { get; set; }
    }
}
