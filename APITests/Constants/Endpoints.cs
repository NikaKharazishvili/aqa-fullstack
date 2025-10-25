namespace ApiTests.Constants;

public static class Endpoints
{
    // Base URL
    public const string BaseUrl = "https://reqres.in/api";

    // Headers & Keys
    public const string ApiKeyHeader = "x-api-key";
    public const string ApiKeyValue = "reqres-free-v1";

    // Users
    public const string Users = "users";
    public const string SingleUser = "users/{id}";
    public const string CreateUser = "users";
    public const string UpdateUser = "users/{id}";
    public const string DeleteUser = "users/{id}";

    // Auth
    public const string Login = "login";
    public const string Register = "register";

    // Resources
    public const string Resources = "unknown";
    public const string SingleResource = "unknown/{id}";
}