using Newtonsoft.Json;

namespace ApiTests.Models;

/// <summary>DTO for user create/update. Fields are optional for PATCH.</summary>
public class UserRequest
{
    public string? name { get; init; }
    public string? job { get; init; }
}