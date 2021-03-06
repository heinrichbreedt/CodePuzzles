﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Raven.Client;

namespace Games.Controllers
{
   public class HomeController : Controller
   {
      public IDocumentSession RavenSession { get; protected set; }

      protected override void OnActionExecuting(ActionExecutingContext filterContext)
      {
         RavenSession = MvcApplication.Store.OpenSession();
      }

      protected override void OnActionExecuted(ActionExecutedContext filterContext)
      {
         if (filterContext.IsChildAction)
            return;

         using (RavenSession)
         {
            if (filterContext.Exception != null)
               return;

            if (RavenSession != null)
               RavenSession.SaveChanges();
         }
      }
      
      public ActionResult Index()
      {
         ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

         
         return View();
      }

      public JsonResult GetGames()
      {
         var games = RavenSession.Query<Game>().ToList();
         return Json(games, JsonRequestBehavior.AllowGet);
      }

      [HttpPost]
      public JsonResult SaveGames([FromJson] IEnumerable<Game> games)
      {
         foreach (var game in games)
         {
            var dbGame = RavenSession.Load<Game>(game.Id);
            if(dbGame == null)
            {
               dbGame = new Game();
               RavenSession.Store(dbGame);
            }
            dbGame.Title = game.Title;
            dbGame.Description = game.Description;
            dbGame.TotalStars = game.TotalStars;
            dbGame.TotalVotes = game.TotalVotes;
            RavenSession.SaveChanges();
         }
         return Json("Ok", JsonRequestBehavior.AllowGet);
      }

      public ActionResult About()
      {
         RavenSession.Store(new Game
                               {
                                  Title = "Gears of War 3",
                                  Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
                                  TotalStars = 50, 
                                  TotalVotes = 10
                               });
         RavenSession.Store(new Game
                               {
                                  Title = "Step Up for Kinnect",
                                  Description = "Integer magna magna, iaculis euismod tincidunt a, cursus ac dolor. Aenean quis egestas diam. Pellentesque purus ipsum, elementum sit amet malesuada eget, aliquet eu magna. Nullam magna massa, sodales nec imperdiet quis, consectetur eget nisl. Aenean eget velit in eros porta dictum. Sed eu dui in augu",
                                  TotalStars = 11, 
                                  TotalVotes = 10
                               });
         RavenSession.Store(new Game
                               {
                                  Title = "Dead Island",
                                  Description = "Vivamus purus eros, aliquet malesuada gravida at, fringilla vel elit. Mauris vestibulum, erat at volutpat semper, metus enim faucibus nunc, in ultrices magna enim in justo",
                                  TotalStars = 34, 
                                  TotalVotes = 10
                               });
         ViewBag.Message = "Your quintessential app description page.";

         return View();
      }

      public ActionResult Contact()
      {
         ViewBag.Message = "Your quintessential contact page.";

         return View();
      }
   }

   public class FromJsonAttribute : CustomModelBinderAttribute
   {
      private readonly static JavaScriptSerializer Serializer = new JavaScriptSerializer();

      public override IModelBinder GetBinder()
      {
         return new JsonModelBinder();
      }

      private class JsonModelBinder : IModelBinder
      {
         public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
         {
            var stringified = controllerContext.HttpContext.Request[bindingContext.ModelName];
            if (string.IsNullOrEmpty(stringified))
               return null;
            return Serializer.Deserialize(stringified, bindingContext.ModelType);
         }
      }
   }

   public class Game
   {
      public int Id { get; set; }
      public string Title { get; set; }
      public string Description{ get; set; }
      public int TotalVotes { get; set; }
      public int TotalStars { get; set; }
   }
}
