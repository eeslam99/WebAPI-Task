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
    public class StudentsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StudentsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // api/students/5
        [HttpDelete("{id}")]
        public IActionResult deleteStudent(int id)
        {
            var student = _unitOfWork.Students.GetById(id);
            if (student == null)
                return NotFound();

            _unitOfWork.Students.Delete(student);
            _unitOfWork.Complete();

            var students = _unitOfWork.Students.GetAllIncludeDepartment();
            return Ok(_mapper.Map<List<Student>, List<StudentDTO>>(students));
        }

        // api/students/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var student = _unitOfWork.Students.GetByIdIncludeDepartment(id);
            if (student == null)
                return NotFound();

            return Ok(_mapper.Map<Student, StudentDTO>(student));
        }

        // api/students/5
        [HttpPut("{id}")]
        public IActionResult put(int id, Student student)
        {
            if (id != student.StId)
                return BadRequest();

            var existingStudent = _unitOfWork.Students.GetById(id);
            if (existingStudent == null)
                return NotFound();

            _unitOfWork.Students.Update(student);

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

        // api/students
        [HttpPost]
        public IActionResult post(Student student)
        {
            _unitOfWork.Students.Add(student);
            _unitOfWork.Complete();
            return StatusCode(StatusCodes.Status201Created);
        }

        // api/students
        [HttpGet]
        public IActionResult GetStudents(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber < 1 || pageSize < 1)
                return BadRequest("Page number and page size must be greater than zero");

            var (students, totalCount) = _unitOfWork.Students.GetPaginatedStudents(pageNumber, pageSize);

            if (!students.Any())
                return NotFound();

            var studentDtos = _mapper.Map<List<Student>, List<StudentDTO>>(students);
            var paginationMetadata = new
            {
                TotalItems = totalCount,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };

            return Ok(new { Data = studentDtos, Pagination = paginationMetadata });
        }
    }
}