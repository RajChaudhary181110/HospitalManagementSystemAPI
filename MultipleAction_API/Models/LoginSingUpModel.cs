using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MultipleAction_API.Models
{
    public class Patient
    {
        public int PatientID { get; set; }
        public string PatientNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }  // "Male", "Female", or "Other"
        public DateTime? DateOfBirth { get; set; }
        public string Age { get; set; }
        public string ContactNumber { get; set; }
        public string EmergencyContactName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Pincode { get; set; }
        public string MaritalStatus { get; set; }
        public string Status { get; set; } //= "Active";  // Default value
        public DateTime CreatedDate { get; set; } //= DateTime.Now;
        public string ModifiedBY { get; set; }
        public DateTime? Modifieddate { get; set; }
        public string FilePath { get; set; }
        public bool Deleted { get; set; } //= false; // Default 0 in SQL
    }
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserRegisterModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
    }
    public class StatusModell
    {
        public string Msg { get; set; }
        public string Status { get; set; }
    }
}
