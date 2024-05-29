namespace GlobalUtility.Interface
{
    public interface IJwtTokenService
    {
        string GenerateJwtToken(string userID, string userFullname);
        int ValidateJwtToken(string token);
    }
}
