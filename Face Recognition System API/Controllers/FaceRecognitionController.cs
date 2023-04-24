using AutoMapper;
using Face_Recognition_System_Data.Dtos;
using Face_Recognition_System_Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using static Face_Recognition_System_Data.Constants;

namespace Face_Recognition_System_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class FaceRecognitionController : Controller
    {
        private readonly IUnitOfWork _uow;
        public FaceRecognitionController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet("train-model")]
        public IActionResult TrainModel()
        {
            try
            {
                _uow.FaceRecognitionService.TrainModel();
                return StatusCode(StatusCodes.Status200OK, new ResponseDto() { Status = true, Message = FaceRecognitionConstants.SuccessTrain });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto() { Status = false, Message = ex.Message });
            }


        }
    }
}
