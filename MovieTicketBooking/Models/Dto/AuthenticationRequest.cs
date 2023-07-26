namespace MovieTicketBooking.Data.Models.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthenticationRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public string Username { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class AuthenticationResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public string AccessToken { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsAdmin { get; set; }
    }
}
