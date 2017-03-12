using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VidEye.Models;

namespace VidEye.Controllers
{
    public class ProfileController : BaseController
    {

        // GET: Profile
        public ActionResult Index()
        {
            ProfileVM profile = new ProfileVM
            {
                VideoCards = _profile.Videos != null ? _profile.Videos.Select(v => new VideoCard
                {
                    Thumbnail = v.Thumbnail,
                    Comments = v.VideoComments != null ? v.VideoComments.Count : 0,
                    Likes = v.Likes != null ? v.Likes.Count : 0,
                    Rating = v.Ratings != null ? (int)v.Ratings.Average(r => r.rate): 0,
                    VideoURL = v.URL,
                    VideoID = v.ID
                }).ToList() : new List<VideoCard>()
            };
            return View(profile);
        }


        // POST: PROFILE
        [HttpPost]
        public ActionResult Upload(VideoUploaderVM model)
        {
            if (ModelState.IsValid)
            {

                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}