using AutoMapper;
using Company.G05.BLL.Interfaces;
using Company.G05.DAL.Models;
using Company.G05.PL.Helper;
using Company.G05.PL.ViewModels.Employees;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Company.G05.PL.Controllers
{
	[Authorize]
	public class EmployeeController : Controller
    {
       private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public EmployeeController( IEmployeeRepository employeeRepository ,
            IDepartmentRepository departmentRepository,
            IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index( string InputSearch )
        {
            var employees = Enumerable.Empty<Employee>();
            if (string.IsNullOrEmpty(InputSearch))
            {
                 employees = await _employeeRepository.GetAllAsync();

            }
            else
            {
                employees= await _employeeRepository.GetByNameAsync(InputSearch);

            }
            var result =_mapper.Map<IEnumerable<EmployeeViewModel>>(employees);

            //View's Dictionary : Transfer Date From Action To View (One Way)
            //1. ViewData : Property Inherited from Class Controller ,Dictionary 
            //ViewData["Data01"] = "Hello World From ViewData";

            //2. ViewBag : Property Inherited from Class Controller ,dynamic
            //ViewBag.data02 = "Hello World From ViewBag";

            //3. TempData : Property Inherited from Class Controller ,Dictionary
            //Transfer Data from request To Another 

            //TempData["Data03"] = "Hello World From TempData";

            return View(result);

        }
        

        public async Task<IActionResult> Create()
        { 
            var departments = await _departmentRepository.GetAllAsync();
            ViewData["department"] = departments;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employee)
        {

          if(ModelState.IsValid)
            {
                try
                {
                    if(employee.Image!=null)
                    employee.ImageName = DocumentSetting.UploadFile(employee.Image,"Images");

                    //Casting EmployeeViewModle (ViewModel) ------> Employee (Model)
                    //Mapping 
                    //1.ManualMapping

                    //Employee employee1 = new Employee()
                    //{
                    //    Id = employee.Id,
                    //    Name = employee.Name,
                    //    Address = employee.Address,
                    //    Age = employee.Age,
                    //    Salary = employee.Salary,
                    //    IsActive = employee.IsActive,
                    //    HiringDate = employee.HiringDate,
                    //    Email = employee.Email,
                    //    PhoneNumber = employee.PhoneNumber,
                    //    WorkFor = employee.WorkFor,
                    //    WorkForId = employee.WorkForId,

                    //};
                    var employee1=_mapper.Map<Employee>(employee);
                    //2.AutoMapping
                    var Count = await _employeeRepository.AddAsync(employee1);
                    if (Count > 0)
                    {
                        TempData["Message"] = "Employee Is Created :)";
                    }
                    return RedirectToAction("Index");
                }
                catch (Exception Ex)
                {

                    ModelState.AddModelError(string.Empty, Ex.Message);
                }
            }
                return View(employee);
        }
        public async Task<IActionResult> Details(int? id ,string empName = "Details" )
        {
            try
            {
                if (id == null) return BadRequest();
                var employee = await _employeeRepository.GetAsync(id.Value);
                if (employee == null) return NotFound();
                //Mapping Employee ----> EmployeeViewModel
                //EmployeeViewModel employeeViewModel = new EmployeeViewModel()
                //{
                //    Id = employee.Id,
                //    Name = employee.Name,
                //    Address = employee.Address,
                //    Age = employee.Age,
                //    Salary = employee.Salary, 
                //    IsActive = employee.IsActive,
                //    HiringDate = employee.HiringDate,
                //    Email = employee.Email,
                //    PhoneNumber = employee.PhoneNumber,
                //    WorkFor = employee.WorkFor,
                //    WorkForId = employee.WorkForId,

                //};
                var employeeViewModel = _mapper.Map<EmployeeViewModel>(employee);

                return View(empName, employeeViewModel);
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction("Error", "Home");
            }

        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var departments = await _departmentRepository.GetAllAsync();
            ViewData["department"] = departments;
            return await Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute]int? id , EmployeeViewModel employee)
        {
            try
            {  
                if (id != employee.Id) return BadRequest();
                if (ModelState.IsValid)
                {
                    if(employee.ImageName!=null)
                        DocumentSetting.Delete(employee.ImageName, "Images");
                    if (employee.Image != null)
                        employee.ImageName = DocumentSetting.UploadFile(employee.Image, "Images");

                    //Employee employee1 = new Employee()
                    //{
                    //    Id = employee.Id,
                    //    Name = employee.Name,
                    //    Address = employee.Address,
                    //    Age = employee.Age,
                    //    Salary = employee.Salary,
                    //    IsActive = employee.IsActive,
                    //    HiringDate = employee.HiringDate,
                    //    Email = employee.Email,
                    //    PhoneNumber = employee.PhoneNumber,
                    //    WorkFor = employee.WorkFor,
                    //    WorkForId = employee.WorkForId,

                    //};
                    var employee1 = _mapper.Map<Employee>(employee);

                    var Count = await _employeeRepository.UpDate(employee1);
                    if (Count > 0)
                        return RedirectToAction("Index");
                }
                return View(employee);
            }
            catch (Exception ex)
            {
                    
                ModelState.AddModelError(string .Empty, ex.Message);
            }
            return View(employee);
        } [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details( id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute]int? id , EmployeeViewModel employee)
        {
            try
            {
                if (id != employee.Id) return BadRequest();
                if (ModelState.IsValid)
                {
                    //Employee employee1 = new Employee()
                    //{
                    //    Id = employee.Id,
                    //    Name = employee.Name,
                    //    Address = employee.Address,
                    //    Age = employee.Age,
                    //    Salary = employee.Salary,
                    //    IsActive = employee.IsActive,
                    //    HiringDate = employee.HiringDate,
                    //    Email = employee.Email,
                    //    PhoneNumber = employee.PhoneNumber,
                    //    WorkFor = employee.WorkFor,
                    //    WorkForId = employee.WorkForId,

                    //};
                    var employee1 = _mapper.Map<Employee>(employee);

                    var Count = await _employeeRepository.Delete(employee1);
                    if (Count > 0)
                        TempData["Message"] = "Employee Is Deleted :(";

                    if (employee.ImageName != null)
                    {
                        DocumentSetting.Delete(employee.ImageName, "Images");
                    }
                        return RedirectToAction("Index");
                }
                return View(employee);
            }
            catch (Exception ex)
            {
                    
                ModelState.AddModelError(string .Empty, ex.Message);
            }
            return View(employee);
        }
    }
}
