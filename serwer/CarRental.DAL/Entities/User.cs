using System;
using System.Collections.Generic;
using System.Security.Cryptography;
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
        public string StatusOfVerification { get; set; }
        public RoleOfWorker RoleOfUser { get; set; }
        public string CodeOfVerification { get; set; }
        public User(string firstName, string lastName, string numberIdentificate, string email,
            string mobileNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            NumberIdentificate = numberIdentificate;
            Email = email;
            MobileNumber = mobileNumber;
            DateCreated = DateTime.Now;
            RoleOfUser = RoleOfWorker.Worker;
            StatusOfVerification = "Processing...";
            
            using (RandomNumberGenerator rng = new RNGCryptoServiceProvider())
            {
                byte[] tokenData = new byte[32];
                rng.GetBytes(tokenData);

                CodeOfVerification = Convert.ToBase64String(tokenData);
            }
        }
        public User()
        {

        }
        public void Update(string firstName, string lastName, string numberIdentificate, string email,
            string mobileNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            NumberIdentificate = numberIdentificate;
            Email = email;
            MobileNumber = mobileNumber;
            DateModified = DateTime.Now;
        }
        public void SetPassword(string encodePassword)
        {
            EncodePassword = encodePassword;
            StatusOfVerification = "Account has been registered.";

        }
    }
}
