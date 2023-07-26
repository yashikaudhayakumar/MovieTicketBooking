namespace MovieTicketBooking.Data.Models.Dto
{
    public class UserPasswordUpdate
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
