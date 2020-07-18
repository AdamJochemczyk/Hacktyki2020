using CarRental.Services.Interfaces;
using CarRental.Services.Models.User;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace CarRental.Services.Models.Email_Templates
{
    public class EmailService : IEmailServices
    {
        public void EmailAfterRegistration(CreateUserDto createUserDto)
        {
            string subject = "Rent Car Service";
            string data = createUserDto.FirstName;
            var id = createUserDto.UserId;
            string htmlBody = @"
                        <html lang=""en"">    
                         <body style='width:720px'>  
                           <h2>Dear " + createUserDto.FirstName + @",</h2> <p style='font-family: Arial,sans-serif'>You have been registered in the service where you can rent a car.
                          Please see the information below about your login and password<br></p>
                            <h2>Login: " + createUserDto.Email + @"<br>
                             </h2>
                             <p>That's your temporary password, you can change your password followed this link.</p>
                              <a href='https://localhost:44390/api/users'style='text-align:center' data-method='post'>Change Password</a>
                              < p>We appreciate that you are with us and using service<br>Have a nice day,<br>Car Rental Service</p>
                            <img src=""cid:WinLogo"" />
                                    </body>
                                         </html>";
            string messageBody = string.Format(htmlBody, data);
            AlternateView alternateViewHtml = AlternateView.CreateAlternateViewFromString(htmlBody, Encoding.UTF8, MediaTypeNames.Text.Html);
            LinkedResource windowsLogo = new LinkedResource(@"C:\Users\kuche\zespol3-laczone-samochody\serwer\CarRental\assets\Image\rsz_logo.png", MediaTypeNames.Image.Jpeg);
            windowsLogo.ContentId = "WinLogo";
            alternateViewHtml.LinkedResources.Add(windowsLogo);
            MailMessage mailMessage = new MailMessage("kucherbogdan2000@gmail.com", "kucherbogdan2000@gmail.com", subject, messageBody);
            mailMessage.AlternateViews.Add(alternateViewHtml);
            using (SmtpClient smpt = new SmtpClient("smtp.gmail.com", 587))
            {
                smpt.EnableSsl = true;
                smpt.DeliveryMethod = SmtpDeliveryMethod.Network;
                smpt.UseDefaultCredentials = false;
                smpt.Credentials = new NetworkCredential("kucherbogdan2000@gmail.com", "basket2009");
                MailMessage message = new MailMessage();
                message.To.Add(createUserDto.Email);
                message.From = new MailAddress("kucherbogdan2000@gmail.com");
                message.Subject = "Car Renting";
                message.Body = "Something";
                smpt.Send(mailMessage);

            }
        }
    }
}
