using AutoMapper;
using EasyApply.Core;
using EasyApply.Core.Domian;
using EasyApply.Dto;
using EasyApply.Model;
using EasyApply.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public CompanyController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
       
        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(int Id)
        {
            var category = await unitOfWork.Company.GetAsync(Id);
            var categoryResponse = mapper.Map<CompanyDto>(category);
            return Ok(new Response<CompanyDto>(categoryResponse));
        }

        [HttpGet("GetById/{Id}")]
        public async Task<IActionResult> GetByGuid(int Id)
        {
            var category = await unitOfWork.Company.FirstOrDefaultAsync(e => e.Id == Id);
            var categoryResponse = mapper.Map<CompanyDto>(category);
            return StatusCode(StatusCodes.Status200OK, new Response<CompanyDto>(categoryResponse));
        }

        [HttpPost]
        public async Task<IActionResult> Post(CompanyModel model)
        {
            var company = mapper.Map<Company>(model);          

            unitOfWork.Company.Add(company);
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0)
                return StatusCode(StatusCodes.Status201Created, new Response<CompanyDto>(mapper.Map<CompanyDto>(company)));

            return StatusCode(StatusCodes.Status400BadRequest);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, CompanyModel model)
        {
            var company = await unitOfWork.Company.FirstOrDefaultAsync(e => e.Id == id);
            if (company == null)
                return NotFound();

            company.Name = model.Name;            

            unitOfWork.Company.Update(company);
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0)
                return StatusCode(StatusCodes.Status200OK, new Response<CompanyDto>(mapper.Map<CompanyDto>(company)));

            return StatusCode(StatusCodes.Status400BadRequest);
        }        

        #region NonAction - Functions
        [NonAction]
        public ObjectResult BadInput()
        {
            return StatusCode(StatusCodes.Status400BadRequest, new Response<CompanyModel>()
            {
                Success = false,
                ErrorCode = ErrorCode.ERROR_4,
                Message = "",
                Errors = new List<string> { "Bad input. Some error occured" }
            });
        }       

        [NonAction]
        public ObjectResult NotFound()
        {
            return StatusCode(StatusCodes.Status404NotFound, new Response<CompanyModel>()
            {
                Success = false,
                ErrorCode = ErrorCode.ERROR_5,
                Message = "Category not found",
                Errors = new List<string> { "Not found" }
            });
        }
        #endregion
    }
}
