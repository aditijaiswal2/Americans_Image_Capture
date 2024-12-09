using Americans_Image_Capture.Shared.Models.Maag_Americans_image;

namespace Americans_Image_Capture.Server.Contracts
{
    public interface ILogindetailrepository
    {
        Task AddUserAsync(Logindetail user); // Add a new user
        Task<Logindetail> GetUserByIdAsync(int userId); // Retrieve user by UserId
        Task<IEnumerable<Logindetail>> GetAllUsersAsync(); // Retrieve all users (optional)
    }
}
