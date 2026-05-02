using Students.Models;

namespace Students.IRepository
{
    public interface IBankRepository
    {
        Task<IEnumerable<Bank>> GetAllAsync();
        Task<Bank?> GetByIdAsync(int id);
        Task<Bank> AddAsync(Bank bank);
        Task<bool> UpdateAsync(Bank bank);
        Task<bool> DeleteAsync(int id);
    }
}
