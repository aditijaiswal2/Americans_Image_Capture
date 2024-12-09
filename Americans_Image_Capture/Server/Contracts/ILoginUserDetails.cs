using Americans_Image_Capture.Shared.DTOs;
using Americans_Image_Capture.Shared.Models;

namespace Americans_Image_Capture.Server.Contracts
{
    public interface ILoginUserDetails
    {
        Task<LoginUserDetailsDTO> CreateLoginUserDetailsAsync(LoginUserDetailsDTO loginUserDetailsDto);
        Task <IEnumerable<LoginUserDetailsDTO>> GetAllLoginUserDetailsAsync();
    }
}
