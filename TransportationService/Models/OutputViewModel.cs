using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ReservationSystem.Utility;

namespace ReservationSystem.Models
{
   public class OutputViewModel
   {
      public string Username { get; set; }
      public IEnumerable<Reservation> Reservations { get; set; }
      public List<Reservation> reservationList { get; set; }
   }
}
