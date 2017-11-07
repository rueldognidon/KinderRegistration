using System;

namespace KinderRegistartion.Services
{
    public class KinderRegistration
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanySchool { get; set; }
        public string Position { get; set; }
        public string EmailAddress { get; set; }
        public DateTime TimeIn { get; set; }
        public string MobileNumber { get; set; }
        public string YearsOfExperience { get; set; }
        public bool WillingToBeContacted { get; set; }
    }
}