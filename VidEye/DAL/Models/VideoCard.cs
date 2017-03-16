using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class VideoCard
    {
        public VideoCard()
        {
            
            
        }

        public string Thumbnail { get; set; }
        public string VideoURL { get; set; }
        public int VideoID { get; set; }
        public int Likes { get; set; }
        public int Comments { get; set; }
        public int Rating { get; set; }
        public string Title { get; set; }

    }
}
