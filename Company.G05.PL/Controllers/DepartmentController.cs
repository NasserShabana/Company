using Company.G05.BLL.Interfaces;
using Company.G05.BLL.Repositories;
using Company.G05.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.G05.PL.Controllers
{
	[Authorize]
	public class DepartmentController : Controller
    { 
        private readonly IDepartmentRepository _repository;
        public DepartmentController(IDepartmentRepository repository)
        {
            _repository = repository;
        }
        public async Task<IActionResult> Index()
        { 
           var departments= await _repository.GetAllAsync();
            return View(departments);
        }
        public IActionResult Create()
        {
          return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Department department)
        {
            if (ModelState.IsValid) {
                var count = await _repository.AddAsync(department);
                if (count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(department);
        }

        public async Task<IActionResult> Details(int? id ,string Name = "Details") 
        { 
            if(id == null) return BadRequest(); //400
            var department = await _repository.GetAsync(id.Value);
            if (department == null) return NotFound();//404
            return View(Name,department);
        
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id) 
        { 
            //if(id == null) return BadRequest(); //400
            //var department = _repository.Get(id.Value);
            //if (department == null) return NotFound();//404
            //return View(department);
           return await  Details(id, "Edit");
        
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute]int? id ,Department model) 
        {
            try
            {
                if (id != model.Id) return BadRequest();
                if (ModelState.IsValid)
                {
                    var Count = await _repository.UpDate(model);
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
            
            }
            return View(model);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            //if (id == null) return BadRequest(); //400
            //var department = _repository.Get(id.Value);
            //if (department == null) return NotFound();//404
            //return View(department);
            return await Details(id, "Delete");


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int? id, Department model)
        {
            try
            {
                if (id != model.Id) return BadRequest();
                if (ModelState.IsValid)
                {
                    var Count = await _repository.Delete(model);
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);

            }
            return View(model);
        }
    }
}
