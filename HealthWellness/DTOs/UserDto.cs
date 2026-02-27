public class UserDto
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; } // later replace with plain password + hashing
    public string Role { get; set; }
}
