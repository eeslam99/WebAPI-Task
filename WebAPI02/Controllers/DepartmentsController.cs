using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI02.DTOs;
using WebAPI02.Models;
using WebAPI02.UnitofWork;

namespace WebAPI02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // api/departments
        [HttpGet]
        public IActionResult get()
        {
            var departments = _unitOfWork.Departments.GetAllIncludeStudents();
            if (departments.Count == 0)
                return NotFound();

            return Ok(_mapper.Map<List<Department>, List<DepartmentDTO>>(departments));
        }

        // api/departments/5
        [HttpDelete("{id}")]
        public IActionResult deleteDepartment(int id)
        {
            var department = _unitOfWork.Departments.GetById(id);
            if (department == null)
                return NotFound();

            _unitOfWork.Departments.Delete(department);
            _unitOfWork.Complete();

            return Ok(_unitOfWork.Departments.GetAll());
        }

        // api/departments/5
        [HttpGet("{id}")]
        public IActionResult getbyId(int id)
        {
            var department = _unitOfWork.Departments.GetById(id);
            if (department == null)
                return NotFound();

            return Ok(_mapper.Map<Department, DepartmentDTO>(department));
        }

        // api/departments/5
        [HttpPut("{id}")]
        public IActionResult put(int id, Department department)
        {
            if (id != department.DeptId)
                return BadRequest();

            var existingDepartment = _unitOfWork.Departments.GetById(id);
            if (existingDepartment == null)
                return NotFound();

            _unitOfWork.Departments.DetachAndModify(existingDepartment, department);

            try
            {
                _unitOfWork.Complete();
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // api/departments
        [HttpPost]
        public IActionResult post(Department department)
        {
            _unitOfWork.Departments.Add(department);
            _unitOfWork.Complete();
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}