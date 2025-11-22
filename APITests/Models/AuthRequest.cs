namespace ApiTests.Models;

/// <summary>Request model for login/register with email and optional password.</summary>
public class AuthRequest
{
    public required string Email { get; set; }
    public string? Password { get; set; }  // ← Optional
}