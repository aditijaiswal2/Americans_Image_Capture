using Americans_Image_Capture.Shared.Entities;

namespace Americans_Image_Capture.Server.Contracts
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
    }
}
