namespace ApiTests.Models;

public class AuthRequest
{
    public required string email { get; set; }
    public string? password { get; set; }  // ← Optional
}