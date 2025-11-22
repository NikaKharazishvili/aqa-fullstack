using Newtonsoft.Json;

namespace ApiTests.Models;

/// <summary>DTO for user create/update. Fields are optional for PATCH.</summary>
public class UserRequest
{
    public string? Name { get; init; }
    public string? Job { get; init; }
}