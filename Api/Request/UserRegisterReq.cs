namespace Api.Request
{
    public record UserRegisterReq
    {
        public string? Fullname { get; set; }
        public string? Email { get; set; }

    }
}