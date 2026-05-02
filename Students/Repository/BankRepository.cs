using Microsoft.EntityFrameworkCore;
using Students.IRepository;
using Students.Models;

namespace Students.Repository
{
    public class BankRepository : IBankRepository
    {
        private readonly ApplicationDbContext _context;

        public BankRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Bank> AddAsync(Bank bank)
        {
            await _context.Banks.AddAsync(bank);
            await _context.SaveChangesAsync();
            return bank;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var bank = await _context.Banks.Include(b => b.BankDetails).FirstOrDefaultAsync(b => b.Id == id);
            if (bank != null)
            {
                if (bank.BankDetails != null && bank.BankDetails.Any())
                {
                    _context.BankDetails.RemoveRange(bank.BankDetails);
                }
                
                _context.Banks.Remove(bank);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Bank>> GetAllAsync()
        {
            return await _context.Banks.Include(b => b.BankDetails).ToListAsync();
        }

        public async Task<Bank?> GetByIdAsync(int id)
        {
            return await _context.Banks.Include(b => b.BankDetails).FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<bool> UpdateAsync(Bank bank)
        {
            var existingBank = await _context.Banks.Include(b => b.BankDetails).FirstOrDefaultAsync(b => b.Id == bank.Id);
            if (existingBank != null)
            {
                // Find details to remove
                var existingDetailIds = bank.BankDetails.Where(d => d.Id != 0).Select(d => d.Id).ToList();
                var detailsToRemove = existingBank.BankDetails.Where(d => !existingDetailIds.Contains(d.Id)).ToList();
                _context.BankDetails.RemoveRange(detailsToRemove);

                existingBank.Name = bank.Name;
                existingBank.IFSCCode = bank.IFSCCode;
                existingBank.IsActive = bank.IsActive;

                // Update or Add new details
                foreach (var detail in bank.BankDetails)
                {
                    var existingDetail = existingBank.BankDetails.FirstOrDefault(d => d.Id == detail.Id && detail.Id != 0);
                    if (existingDetail != null)
                    {
                        existingDetail.AccountName = detail.AccountName;
                        existingDetail.AccountNumber = detail.AccountNumber;
                        existingDetail.BranchName = detail.BranchName;
                    }
                    else
                    {
                        existingBank.BankDetails.Add(detail);
                    }
                }

                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
