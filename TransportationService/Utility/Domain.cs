using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

// These are the classes we will store in the Database.

namespace ReservationSystem.Utility
{
    public class Reservation
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string customerName { get; set; }
        public string phoneNumber { get; set; }
        public int partySize { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime dateTime { get; set; }
        public int reservationId { get; set; }
        public bool hasBeenDeleted { get; set; }
    }

    public class User
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
