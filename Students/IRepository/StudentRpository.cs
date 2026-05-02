using Students.Models;

namespace Students.IRepository
{
    public interface IStudentRpository
    {
        Task<IEnumerable<Student>> GetAllAsync();
        
        Task<Student?> GetByIdAsync(int id);
        
        Task<Student> AddAsync(Student student);
        
        Task<bool> UpdateAsync(Student student);
        
        Task<bool> DeleteAsync(int id);

        Task<List<Countries>> GetCountriesAsyncBy(); 
        
        Task<List<States>> GetStatesAsyncByCountryID(int countryId);  

        Task<List<Cities>> GetCitiesAsyncByStateID(int stateId);
    }
}
