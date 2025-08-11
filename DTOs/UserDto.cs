public class UserCreateDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; } 
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public class UserUpdateDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; } 
    public string? Email { get; set; }
    public string? Password { get; set; }
}

public class UserReadDto
{
    public int Id { get; set; } 
    public required string FirstName { get; set; }
    public required string LastName { get; set; } 
    public required string Email { get; set; }
}
