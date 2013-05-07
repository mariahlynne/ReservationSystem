using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReservationSystem.Utility;
using MongoDB.Bson;
using ReservationSystem.Models;

namespace ReservationSystem.Controllers
{

    public class AdminController : TransportationBaseController
    {
            
        [HttpGet]
        public ActionResult LoadView()
        {
            var user = sessionManager.User;
            if (user == null)
            {
                return PartialView("Login");
            }
            DatabaseInterface db = new DatabaseInterface();
            var model = new OutputViewModel()
            {
                Username = user.Username,
                Reservations = db.GetReservations(),
            };
            return PartialView("AdminView", model);
        }

        public ActionResult RefreshAdmin()
        {
            User user = sessionManager.User;
            if (user != null)
            {
                return Json(new
                {
                    user = JsonUtility.ToUserJson(user),
                    headerText = "Welcome, " + user.Username
                });
            }
            return Json(new { error = true });
        }

        public ActionResult AddReservation()
        {
            DatabaseInterface db = new DatabaseInterface();
            ReservationModel model = new ReservationModel()
            {
                customerName = "",
                date = "",
                time = "",
                ampm = "PM",
                partySize = -1,
                phoneNumber = "",
                updatingReservation = false,
            };
            return PartialView("Reservation", model);
        }

        public ActionResult ModifyReservation(int reservationId)
        {
            DatabaseInterface db = new DatabaseInterface();
            Reservation r = db.GetReservationById(reservationId);
            ReservationModel model = new ReservationModel()
            {
                customerName = r.customerName,
                partySize = r.partySize,
                phoneNumber = r.phoneNumber,
                date = r.dateTime.ToString("MM/dd/yyyy"),
                time = r.dateTime.ToString("hh:mm"),
                ampm = r.dateTime.ToString("tt"),
                reservationId = r.reservationId,
                updatingReservation = true
            };
            return PartialView("Reservation", model);
        }

        public ActionResult AddNewReservation(string customerName, string phoneNumber, int partySize, string time, string date)
        {
            DatabaseInterface db = new DatabaseInterface();
            Reservation reservation = new Reservation()
            {
                phoneNumber = phoneNumber,
                partySize = partySize,
                customerName = customerName,
                reservationId = db.GetNextReservationId(),
                dateTime = DateTime.Parse(date + " " + time),
                Id = ObjectId.GenerateNewId(),
                hasBeenDeleted = false
            };
            db.AddReservation(reservation);
            return Json(new
            {
                success = "true"
            });
        }

        public ActionResult UpdateReservation(int reservationId, string customerName, string phoneNumber, int partySize, string time, string date)
        {
            DatabaseInterface db = new DatabaseInterface();
            Reservation r = db.GetReservationById(reservationId);
            r.customerName = customerName;
            r.phoneNumber = phoneNumber;
            r.partySize = partySize;
            r.dateTime = DateTime.Parse(date + " " + time);
            db.UpdateReservation(r);
            return Json(new { success = "true", id = r.reservationId, dateTime = r.dateTime.ToString("MM/dd/yyyy hh:mm tt") });
        }

        public ActionResult ViewReservations()
        {
            DatabaseInterface db = new DatabaseInterface();
            var reservations = db.GetReservations();
            var model = new CustomTable()
            {
                Headers = new List<string>(){
                   "Name",
                   "Date/Time",
                   "Party Size",
                   "Phone"
                   },
                Rows = reservations.Select(r => new CustomRow()
                {
                    Id = r.reservationId,
                    ModifyCall = "modifyReservation(event," + r.reservationId + ")",
                    DeleteCall = "deleteItemClick('" + r.reservationId + "', event)",
                    Columns = new List<string>(){
                        r.customerName,
                        r.dateTime.ToString("MM/dd/yyyy hh:mm tt"),
                        r.partySize.ToString(),
                        r.phoneNumber
                   }
                }).ToList()
            };
            return PartialView("ViewReservations", model);
        }

        public ActionResult CancelReservation(int id)
        {
            DatabaseInterface db = new DatabaseInterface();
            Reservation r = db.GetReservationById(id);
            r.hasBeenDeleted = true;
            db.UpdateReservation(r);
            return Json(new { success = "true", msg = "" });
        }
    }
}
