using CarRental.DAL.Entities;
using CarRental.Services.Interfaces;
using CarRental.Services.Models.User;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Security.Claims;
using System.Text;

namespace CarRental.Services.Models.Email_Templates
{
    public class EmailService : IEmailServices
    {
           public void EmailAfterRegistration(CreateUserDto createUserDto)
        {
            var claims = new List<Claim> {
                     new Claim(createUserDto.UserId.ToString(), createUserDto.FirstName,createUserDto.LastName,createUserDto.NumberIdentificate
                                         ,createUserDto.RoleOfWorker.ToString())
            };

            var jwt = new JwtSecurityToken(
                 issuer: TokenOptions.ISSUER,
                 audience: TokenOptions.AUDIENCE,
                 claims: claims,
                 expires: DateTime.Now.AddMinutes(TokenOptions.LIFETIME),
                 signingCredentials: new SigningCredentials(TokenOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
           
            string subject = "Rent Car Service";
            string data = createUserDto.FirstName;
            string htmlBody = @"
                        <html lang=""en"">    
                         <body style='width:720px'>  
                           <h2>Dear " + createUserDto.FirstName + @",</h2> <p style='font-family: Arial,sans-serif'>You have been registered in the service where you can rent a car.
                             <br>
                             Please see the information below about your login</p>
                            <h2>Login: " + createUserDto.Email + @"
                             </h2>
                             <p>That's your temporary password, you can change your password followed this link.</p>
                              <div style='text-align:center'><a href='https://localhost:44390/api/users"+encodedJwt+@"' style='font-size:30px'>Change Password</a></div>
                              <p style='font-family: Arial,sans-serif'>We appreciate that you are with us and using service<br>Have a nice day,<br>Car Rental Service</p>
                            <img src=""cid:WinLogo"" />
                                    </body>
                                         </html>";
            string messageBody = string.Format(htmlBody, data);
            AlternateView alternateViewHtml = AlternateView.CreateAlternateViewFromString(htmlBody, Encoding.UTF8, MediaTypeNames.Text.Html);
            LinkedResource windowsLogo = new LinkedResource(@"C:\Users\kuche\zespol3-laczone-samochody\serwer\CarRental\assets\Image\rsz_logo.png", MediaTypeNames.Image.Jpeg);
            windowsLogo.ContentId = "WinLogo";
            alternateViewHtml.LinkedResources.Add(windowsLogo);
            MailMessage mailMessage = new MailMessage("kucherbogdan2000@gmail.com", createUserDto.Email, subject, messageBody);
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
