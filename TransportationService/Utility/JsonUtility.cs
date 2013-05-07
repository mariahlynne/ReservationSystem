﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace ReservationSystem.Utility
{

   [Serializable]
   public class UserJson
   {
      [JsonPropertyAttribute("userName")]
      public string Username { get; set; }
      [JsonPropertyAttribute("email")]
      public string Email { get; set; }
   }
   public static class JsonUtility
   {

      public static UserJson ToUserJson(User user)
      {
         return new UserJson()
         {
            Username = user.Username,
            Email = user.Email
         };
      }
   }
}