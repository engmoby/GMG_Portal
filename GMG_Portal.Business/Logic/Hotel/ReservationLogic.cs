using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMG_Portal.Data;
using Heloper;

namespace GMG_Portal.Business.Logic.SystemParameters
{
    public class ReservationLogic
    {
        private readonly GMG_Portal_DBEntities1 _db;

        public ReservationLogic()
        {
            _db = new GMG_Portal_DBEntities1();
        }
        public List<Hotels_Reservation> GetAllWithSeen()
        {
            return _db.Hotels_Reservation.OrderBy(p => p.SeenBy).ToList();
        }
        public List<Hotels_Reservation> GetAll()
        {
            return _db.Hotels_Reservation.Where(p => p.SeenDate != null).ToList();
        }
        public Hotels_Reservation Get(int id)
        {
            return _db.Hotels_Reservation.Find(id);
        }
        private Hotels_Reservation Save(Hotels_Reservation reservation)
        {
            try
            {
                _db.SaveChanges();
                reservation.OperationStatus = "Succeded";
                return reservation;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    if (e.InnerException.ToString().Contains("IX_Countries_Ar"))
                    {
                        reservation.OperationStatus = "NameArMustBeUnique";
                        return reservation;
                    }
                    else if (e.InnerException.ToString().Contains("IX_Countries_En"))
                    {
                        reservation.OperationStatus = "NameEnMustBeUnique";
                        return reservation;
                    }
                }
                throw;
            }
        }
        public Hotels_Reservation Insert(Hotels_Reservation postedReservation)
        {
             Random _r = new Random();
            int n = _r.Next();

            var reservation = new Hotels_Reservation()
            {
                FirstName = postedReservation.FirstName,
                LastName = postedReservation.LastName,
                Email = postedReservation.Email,
                Phone = postedReservation.Phone,
                Adult = postedReservation.Adult,
                Child = postedReservation.Child,
                CheckIn = postedReservation.CheckIn,
                CheckOut = postedReservation.CheckOut,
                Notes = postedReservation.Notes,
                HotelId = postedReservation.HotelId,
                CountryId = postedReservation.CountryId,
                OperationId = n,
                CreationTime = Parameters.CurrentDateTime
            };
            _db.Hotels_Reservation.Add(reservation);
            return Save(reservation);
        }
        public Hotels_Reservation Edit(Hotels_Reservation postedReservation)
        {
            Hotels_Reservation reservation = Get(postedReservation.Id);
            reservation.Seen = postedReservation.Seen;
            reservation.SeenDate = Parameters.CurrentDateTime;
            reservation.SeenBy = Parameters.UserId;
            return Save(reservation);
        }


    }
}
