using System;
using System.Collections.Generic;




namespace HorseAuction
{
    public class User
    {
        public Guid UserID { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string CellPhone { get; set; }
        public string UserEmail { get; set; }


    }
}
