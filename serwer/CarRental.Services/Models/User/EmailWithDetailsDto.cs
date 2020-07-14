using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Services.Models.User
{
   public class EmailWithDetailsDto
    {
        public int RecipientId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Footer { get; set; }
        

    }
}
