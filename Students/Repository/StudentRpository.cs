using Microsoft.EntityFrameworkCore;
using Students.IRepository;
using Students.Models;

namespace Students.Repository
{
    public  class StudentRpository : IStudentRpository
    {

        private readonly ApplicationDbContext _context; 

        public StudentRpository(ApplicationDbContext context)
        {
            _context = context;
        }
        public  async Task<Student> AddAsync(Student student)
        {
            await _context.Students.AddAsync(student);

            await _context.SaveChangesAsync();

            return student;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var student = await _context.Students
                                        .Include(s => s.StudentDetails)
                                        .FirstOrDefaultAsync(s => s.Id == id);

            if (student != null)
            {
                if (student.StudentDetails != null)
                {
                    _context.StudentDetails.Remove(student.StudentDetails);
                }
                
                _context.Students.Remove(student);

                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _context.Students.Include(s => s.StudentDetails).ToListAsync();
        }

        public async Task<Student?> GetByIdAsync(int id)
        {
            return await _context.Students.Include(s => s.StudentDetails).FirstOrDefaultAsync(s => s.Id == id);

        }

        public async Task<List<Cities>> GetCitiesAsyncByStateID(int stateId)
        {
            return await _context.Cities.Where(c => c.State_Id == stateId).ToListAsync();    
        }

        public  async Task<List<Countries>> GetCountriesAsyncBy()
        {
            try
            {
            return  await _context.Countries.ToListAsync();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<States>> GetStatesAsyncByCountryID(int countryId)
        {
            return await _context.States.Where(s => s.Country_Id == countryId).ToListAsync();
        }

        public async Task<bool> UpdateAsync(Student student)
        {
            try
            {
                _context.Students.Update(student);

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
           
            return false;
        }
    }
}
