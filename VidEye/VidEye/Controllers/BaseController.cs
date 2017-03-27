using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using DAL.Repository;
using DAL.Models;

namespace VidEye.Controllers
{   
    public class BaseController : Controller
    {
        protected UserProfileRepository UserRep;
        protected VideoTableRepository VideoRep;
        protected VideoCommentRepository VideoCommentRep;
        protected LikeTableRepository LikeTableRep;
        protected UserProfile _profile;
        protected RatingRepository RateTableRep;
        public BaseController()
        {
            UserRep = new UserProfileRepository();
            VideoRep = new VideoTableRepository();
            VideoCommentRep = new VideoCommentRepository();
            LikeTableRep = new LikeTableRepository();
            RateTableRep = new RatingRepository();
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (!string.IsNullOrEmpty(User.Identity.Name))
            {
                var userID = User.Identity.GetUserId();
                _profile = UserRep.Find(u => u.MemebershipID == userID);
                ViewBag.User = _profile;

            }
            base.OnActionExecuting(filterContext);
        }
        
    }
}