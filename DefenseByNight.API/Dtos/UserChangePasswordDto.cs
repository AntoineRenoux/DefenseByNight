namespace DefenseByNight.API.Dtos
{
    public class UserChangePasswordDto
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfimPassword { get; set; }
    }
}