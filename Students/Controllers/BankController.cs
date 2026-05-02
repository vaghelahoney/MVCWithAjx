using Microsoft.AspNetCore.Mvc;
using Students.IRepository;
using Students.Models;

namespace Students.Controllers
{
    public class BankController : Controller
    {
        private readonly IBankRepository _bankRepository;

        public BankController(IBankRepository bankRepository)
        {
            _bankRepository = bankRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetBankList()
        {
            var banks = await _bankRepository.GetAllAsync();
            return PartialView("_BankList", banks);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var bank = await _bankRepository.GetByIdAsync(id);
            if (bank == null)
            {
                return NotFound();
            }
            return Json(bank);
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] Bank bank)
        {
            if (ModelState.IsValid)
            {
                if (bank.Id > 0)
                {
                    await _bankRepository.UpdateAsync(bank);
                    return Json(new { success = true, message = "Bank updated successfully" });
                }
                else
                {
                    await _bankRepository.AddAsync(bank);
                    return Json(new { success = true, message = "Bank created successfully" });
                }
            }
            return Json(new { success = false, message = "Invalid data" });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _bankRepository.DeleteAsync(id);
            if (result)
            {
                return Json(new { success = true, message = "Bank deleted successfully" });
            }
            return Json(new { success = false, message = "Failed to delete" });
        }
    }
}
