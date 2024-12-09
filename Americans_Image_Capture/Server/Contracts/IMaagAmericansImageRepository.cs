

namespace Americans_Image_Capture.Server.Contracts
{
    public interface IMaagAmericansImageRepository
    {
        Task<IEnumerable<MaagAmericansImage>> GetAllAsync();
        Task<MaagAmericansImage> GetByIdAsync(int id);
        Task AddAsync(MaagAmericansImage image);
        Task UpdateAsync(MaagAmericansImage image);
        Task DeleteAsync(int id);
    }
}
