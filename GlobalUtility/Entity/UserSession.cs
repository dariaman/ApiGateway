namespace GlobalUtility.Entity
{
    public record UserSession
    {
        public Int64 UserID { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
    }
}
