using System;

namespace DefenseByNight.API.Dtos
{
    public class UserRegisterDto
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastActive { get; set; }

        public UserRegisterDto()
        {
            this.CreatedDate = DateTime.Now;
            this.LastActive = null;
        }
    }
}