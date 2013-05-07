using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReservationSystem.Utility;
using MongoDB.Bson;

namespace ReservationSystem.Controllers
{
   public class UtilityController : TransportationBaseController
   {
      //
      // GET: /Home/

      public ActionResult Index()
      {
      return View();
      }

      public ActionResult GetRegisterModal()
      {
         return View();
      }

      public ActionResult IsValidCancelReservation(string id)
      {
          return Json(new { isValid = "true" });
      }

      public ActionResult GetConfirmationHTML()
      {
          return PartialView("Confirmation");
      }
   }
}
