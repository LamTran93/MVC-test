using Microsoft.AspNetCore.Mvc;
using Business;
using Model;

namespace mvc_part1.Controllers
{
    public class RookiesController : Controller
    {
        private readonly IPersonService _service;

        public RookiesController(IPersonService service)
        {
            _service = service;
        }

        public IActionResult FullList()
        {
            ViewData["Title"] = "Full member list";
            return View("Persons", _service.GetAll());
        }

        public IActionResult Male()
        {
            ViewData["Title"] = "All males list";
            return View("Persons", _service.GetAllMales());
        }

        public IActionResult Oldest()
        {
            return View(_service.GetOldest());
        }

        public IActionResult FullName()
        {
            return View(_service.GetAll());
        }

        public IActionResult AgeFilter([FromForm] string year, [FromForm] string compare)
        {
            if (!int.TryParse(year, out int result)) return RedirectToAction("Error", "Home");

            switch (compare)
            {
                case "equal":
                    return RedirectToAction("Equal", new { year = result });
                case "higher":
                    return RedirectToAction("Higher", new { year = result });
                case "lower":
                    return RedirectToAction("Lower", new { year = result });
                default:
                    return RedirectToAction("Error", "Home");
            }
        }

        public IActionResult Equal([FromQuery] int year)
        {
            var persons = _service.GetPersonsByYear(year);
            return View("Persons", persons);
        }

        public IActionResult Higher([FromQuery] int year)
        {
            var persons = _service.GetPersonsByYear(year, AgeComparer.Higher);
            return View("Persons", persons);
        }

        public IActionResult Lower([FromQuery] int year)
        {
            var persons = _service.GetPersonsByYear(year, AgeComparer.Lower);
            return View("Persons", persons);
        }

        public IActionResult Excel()
        {
            try
            {
                var excelStream = _service.ToExcel();

                return File(excelStream,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "persons.xlsx");

            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        public IActionResult CreatePerson([Bind] RequestPerson person)
        {
            if (ModelState.IsValid)
            {
                var createdPerson = _service.Create(person);
                return RedirectToAction("GetDetails", new { personId = createdPerson.Id });
            }
            return BadRequest();
        }

        public IActionResult Edit([FromQuery]string personId)
        {
            var foundPerson = _service.Get(personId);
            if (foundPerson == null) return NotFound();
            var requestPerson = new RequestPerson(foundPerson);
            return View("Edit", requestPerson);
        }

        [ValidateAntiForgeryToken]
        public IActionResult EditPerson([Bind] RequestPerson person)
        {
            if (ModelState.IsValid)
            {
                var updated = _service.Update(person);
                return RedirectToAction("GetDetails", new { personId = updated.Id });
            }
            return BadRequest();
        }

        [ValidateAntiForgeryToken]
        public IActionResult Delete(string id)
        {
            var deletedPerson = _service.Delete(id);
            if (deletedPerson != null) return RedirectToAction("DeleteConfirm", new { success = true, name = $"{deletedPerson.FirstName} {deletedPerson.LastName}" });
            else return RedirectToAction("DeleteConfirm", new { success = false, name = $"{deletedPerson.FirstName} {deletedPerson.LastName}" });
        }

        public IActionResult DeleteConfirm([FromQuery] bool success, [FromQuery] string name)
        {
            if (success) ViewData["message"] = $"Member {name} deleted successfully";
            else ViewData["success"] = $"Member {name} deleted failed";
            return View(success);
        }

        public IActionResult GetDetails([FromQuery] string personId)
        {
            var foundPerson = _service.Get(personId);
            if (foundPerson == null) return NotFound();
            return View("PersonDetails", foundPerson);
        }
    }
}
