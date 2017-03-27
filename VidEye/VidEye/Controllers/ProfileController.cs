using DAL.Models;
using Microsoft.WindowsAzure.MediaServices.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VidEye.Models;

namespace VidEye.Controllers
{
    [Authorize]
    public class ProfileController : BaseController
    {
        private VideoUploadManager videoManager;

        public ProfileController()
        {
            videoManager = new VideoUploadManager();           
        }

     
        public ActionResult Index()
        {
            ProfileVM profile = new ProfileVM
            {
                VideoCards = _profile.Videos.Select(v => new VideoCard
                {
                    Thumbnail = v.Thumbnail,
                    Comments = v.VideoComments != null ? v.VideoComments.Count : 0,
                    Likes = v.Likes != null ? v.Likes.Count : 0,
                    Rating = v.Ratings != null && v.Ratings.Any() ? (int)v.Ratings.Average(r => r.Rate) : 0,
                    VideoURL = v.URL,
                    VideoID = v.ID,
                    Title = v.Title
                }).ToList()
            };          
            return View(profile);
        }

        [HttpPost]
        public JsonResult Comment(string comment, int videoId)
        {
            VideoCommentRep.Add(new VideoComment
            {
                VideoCommentDateCreated = DateTime.Now,
                VideoCommentDesc = comment,
                VideoID = videoId,
                PosterID = _profile.ID
            });
            var totalcomments = VideoCommentRep.FindAll(vc => vc.VideoID == videoId).Count;
            return Json(totalcomments, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Like(int videoId)
        {
            var existing = LikeTableRep.Find(l => l.VideoID == videoId && l.PosterID == _profile.ID);
            if(existing == null)
            {
                LikeTableRep.Add(new LikeTable
                {
                    LikeDateCreated = DateTime.Now,
                    VideoID = videoId,
                    PosterID = _profile.ID
                });
            }
            else
            {
                LikeTableRep.Delete(existing);
            }
           
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Rate(int videoId, int rate)
        {
            var existing = RateTableRep.Find(l => l.VideoID == videoId && l.PosterID == _profile.ID);
            if (existing == null)
            {
                RateTableRep.Add(new Rating
                {
                    RateDateCreated = DateTime.Now,
                    VideoID = videoId,
                    PosterID = _profile.ID,
                    Rate = rate
                });
            }
            else
            {
                RateTableRep.Delete(existing);
            }

            return Json("Success", JsonRequestBehavior.AllowGet);
        }


        // POST: PROFILE
        [HttpPost]
        public ActionResult Upload(VideoUploaderVM model)
        {
            if (ModelState.IsValid)
            {
                var filePath = SaveToDisk(model.UploadFile);
                var inputAsset = videoManager.GetAsset(LocalAssetType.File, filePath: filePath);
                //var thumbnailAsset = videoManager.GetAsset(LocalAssetType.Thumbnail, inputAsset);
                //videoManager.PublishAsset(thumbnailAsset, false, ".bmp");             
                //var audioOnlyAsset = videoManager.GetAsset(LocalAssetType.AudioOnly, inputAsset);
                //videoManager.PublishAsset(audioOnlyAsset);
                var mp4BitRate = videoManager.GetAsset(LocalAssetType.EncodedMp4, inputAsset);
                videoManager.PublishAsset(mp4BitRate);

                //save the video information to the database
                var vidTable = new VideoTable
                {
                    URL = videoManager.StreamingUri.ToString(),
                    Thumbnail = videoManager.ThumbnailUri.ToString(),
                    VideoDateCreated = DateTime.Now,
                    VideoDesc = model.VideoDescription,
                    Title = model.VideoTitle,
                    UserProfileID = _profile.ID
                };
                VideoRep.Add(vidTable);

                return RedirectToAction("Index");            
            }
            return View(model);
        }


        public ActionResult DisplayUpload(UploadedDisplayModel model)
        {
            return View(model);
        }

        private string SaveToDisk(HttpPostedFileBase file)
        {
            var uploadPath = ControllerContext.HttpContext.Server.MapPath(@"/Media/Uploads");
            var fullpath = Path.Combine(uploadPath, file.FileName);
            int counter = 2;

            while (System.IO.File.Exists(fullpath))
            {
                fullpath = Path.Combine(uploadPath, string.Format("{1}{0}", file.FileName, counter));
                counter++;
            }
            file.SaveAs(fullpath);
            return fullpath;
        }
    }
}