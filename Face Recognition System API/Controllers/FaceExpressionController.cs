using AutoMapper;
using Face_Recognition_System_Data.Dtos;
using Face_Recognition_System_Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using static Face_Recognition_System_Data.Constants;

namespace Face_Recognition_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaceExpressionController : Controller
    {
        private readonly IUnitOfWork _uow;
        public FaceExpressionController(IUnitOfWork uow)
        {
            _uow = uow;   
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<FaceExpressionDto> responseData = await _uow.FaceExpressionService.RetrieveAll();
                return StatusCode(StatusCodes.Status200OK, new ResponseDto() { Status = true, Message = BaseConstants.RetrievedData, Value = responseData });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto() { Status = false, Message = ex.Message });
            }
        }
    }
}
