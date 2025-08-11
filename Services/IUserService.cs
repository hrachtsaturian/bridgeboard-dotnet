namespace Api.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserReadDto>> GetAllUsersAsync();
        Task<UserReadDto?> GetUserByIdAsync(int id);
        Task<UserReadDto> CreateUserAsync(UserCreateDto userDto);
        Task<UserReadDto?> UpdateUserAsync(int id, UserUpdateDto userDto);
        Task<bool> DeleteUserAsync(int id);
    }
}