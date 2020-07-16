using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.DAL.Entities
{
    public class User : BaseEntity
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NumberIdentificate { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string EncodePassword { get; set; }

        public User(string firstName, string lastName, string numberIdentificate, string email,
            string mobileNumber, string encodePassword)
        {
            FirstName = firstName;
            LastName = lastName;
            NumberIdentificate = numberIdentificate;
            Email = email;
            MobileNumber = mobileNumber;
            EncodePassword = encodePassword;
            DateCreated = DateTime.Now;
        }
        public User()
        {

        }
        public void Update(User user)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            NumberIdentificate = user.NumberIdentificate;
            Email = user.Email;
            MobileNumber = user.MobileNumber;
            DateModified = DateTime.Now;
        }
    }
}
