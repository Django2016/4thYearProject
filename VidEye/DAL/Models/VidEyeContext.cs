using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class VidEyeContext : DbContext
    {
        public VidEyeContext() : base("VidEyeConnection")
        {

        }
       public DbSet<UserProfile> UserProfiles{get;set;}
       public DbSet<VideoTable> VideoTables{get;set;}
       public DbSet<VideoComment> VideoComments{get;set;}
       public DbSet<LikeTable> LikeTables{get;set;}
       public DbSet<ShareTable> ShareTables{get;set;}
       public DbSet<Rating> Ratings{get;set;}
       public DbSet<Subscription> Subscriptions{get;set;}
       public DbSet<VideoSubscription> VideoSubscriptions{get;set;}
       public DbSet<UserStatus> UserStatuses{get;set;}
    }
}
