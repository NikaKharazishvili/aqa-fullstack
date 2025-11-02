namespace ApiTests.Models;

/// <summary>Request model for login/register with email and optional password.</summary>
public class AuthRequest
{
    public required string email { get; set; }
    public string? password { get; set; }  // ← Optional
}