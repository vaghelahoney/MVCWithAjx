using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Students.IRepository;
using Students.Models;
using System.Diagnostics;

namespace Students.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStudentRpository _studentRepository;

        public HomeController(IStudentRpository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var student = await _studentRepository.GetByIdAsync(id);
                return Json(student);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (student == null)
                    {
                        return NotFound();
                    }

                    if (student.Id > 0)
                    {
                        await _studentRepository.UpdateAsync(student);
                        return RedirectToAction("");
                    }
                    student.Id = 0;
                    await _studentRepository.AddAsync(student);
                    return RedirectToAction("");
                }
            }
            catch (Exception)
            {

                throw;
            }
            
            return View();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Student student)
        {
            try
            {
                await _studentRepository.UpdateAsync(student);
                return Json(student);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _studentRepository.DeleteAsync(id);
                return Json(new { message = "Student deleted successfully" });
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Index()
        {
            try
            {

                IList<Countries> countries = await _studentRepository.GetCountriesAsyncBy();

                ViewBag.Countries = new SelectList(countries, "Country_Id", "Country_Name");

            }
            catch (Exception)
            {

                throw;
            }


            return View(new Student());
        }

        [HttpGet]
        public async Task<IActionResult> GetStatesAsyncByCountryID(int countryId)
        {
            if (countryId <= 0 )
            {
                return NotFound();  
            }

            IList<States> States = await _studentRepository.GetStatesAsyncByCountryID(countryId);

            return Json(States);
        }

        [HttpGet]
        public async Task<IActionResult> GetCitiesAsyncByStateID(int stateId)
        {
            if (stateId <= 0)
            {
                return NotFound();
            }

            IList<Cities> cities = await _studentRepository.GetCitiesAsyncByStateID(stateId);

            return Json(cities);
        }



        [HttpGet]
        public async Task<IActionResult> GetStudentList()
        {
            var students = await _studentRepository.GetAllAsync();

            if (students == null)
            {
                students = new List<Student>();
            }

            return PartialView("_Index", students);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
