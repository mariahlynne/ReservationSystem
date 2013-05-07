using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace ReservationSystem.Utility
{
    public class DatabaseInterface
    {
        MongoDatabase _database;
        const string _usersCollectionName = "Users";
        const string _reservations = "Reservations";

        public DatabaseInterface()
        {
            string connectionString = "mongodb://localhost/ReservationSystem";
            _database = MongoDatabase.Create(connectionString);
        }

        public void SaveUser(User user)
        {
            var coll = _database.GetCollection(_usersCollectionName);
            coll.Save(user);
        }

        public User GetUser(string username, string password)
        {
            var coll = _database.GetCollection(_usersCollectionName);
            var query = Query.And(Query.EQ("Username", username), Query.EQ("Password", password));
            return coll.FindOneAs<User>(query);
        }

        public User GetUserById(ObjectId id)
        {
            var coll = _database.GetCollection(_usersCollectionName);
            var query = Query.EQ("_id", id);
            return coll.FindOneAs<User>(query);
        }

        public List<Reservation> GetReservations()
        {
            var coll = _database.GetCollection(_reservations);
            DateTime currentDateTime = DateTime.Now;
            var query = Query.And(Query.EQ("hasBeenDeleted", false), Query.GTE("dateTime", currentDateTime));
            return coll.FindAs<Reservation>(query).SetSortOrder(SortBy.Ascending("dateTime")).ToList();
        }

        public int GetNextReservationId()
        {
            var coll = _database.GetCollection(_reservations);
            List<Reservation> reservations = coll.FindAllAs<Reservation>().ToList();
            if (reservations.OrderBy(r => r.reservationId).LastOrDefault() == null)
                return 1;
            return reservations.OrderBy(r => r.reservationId).LastOrDefault().reservationId + 1;
        }

        public void AddReservation(Reservation reservation)
        {
            var coll = _database.GetCollection(_reservations);
            coll.Save(reservation);
        }

        public Reservation GetReservationById(int id)
        {
            var coll = _database.GetCollection(_reservations);
            var query = Query.And(Query.EQ("reservationId", id), Query.EQ("hasBeenDeleted", false));
            return coll.FindOneAs<Reservation>(query);
        }

        public void UpdateReservation(Reservation r)
        {
            var coll = _database.GetCollection(_reservations);
            coll.Remove(Query.EQ("reservationId", r.reservationId));
            coll.Save(r);
        }
    }
}
