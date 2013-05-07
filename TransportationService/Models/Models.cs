using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Bson;
using ReservationSystem.Utility;

namespace ReservationSystem.Models
{

    public class ReservationModel
    {
        public string customerName { get; set; }
        public int partySize { get; set; }
        public string phoneNumber { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public string ampm { get; set; }
        public bool updatingReservation { get; set; }
        public int reservationId { get; set; }
    }

    public class CustomRow
    {
        public List<string> Columns { get; set; }
        public string ModifyCall { get; set; }
        public string DeleteCall { get; set; }
        public int Id { get; set; }
    }

    public class CustomTable
    {
        public List<string> Headers { get; set; }
        public List<CustomRow> Rows { get; set; }
    }
}
