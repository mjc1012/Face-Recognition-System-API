using AutoMapper;
using Face_Recognition_System_Data.Dtos;
using Face_Recognition_System_Data.Models;
using Face_Recognition_System_Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using static Face_Recognition_System_Data.Constants;

namespace Face_Recognition_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaceRecognitionStatusController : Controller
    {
        private readonly IUnitOfWork _uow;
        public FaceRecognitionStatusController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FaceRecognitionStatusDto status)
        {
            try
            {
                List<string> validationErrors = await _uow.FaceRecognitionStatusHandler.CanAdd(status);

                if (validationErrors.Any())
                {
                    return StatusCode(StatusCodes.Status200OK, new ResponseDto() { Status = false, Message = BaseConstants.ErrorList, Value = validationErrors });
                }
                else
                {
                    FaceToRecognizeDto face = await _uow.FaceToRecognizeService.Find(status.FaceToRecognizeId);
                    PersonDto person = await _uow.PersonService.FindById(status.PredictedPersonId);
                    await _uow.FaceRecognitionStatusService.Create(status, face, person);
                    return StatusCode(StatusCodes.Status200OK, new ResponseDto() { Status = true, Message = FaceToRecognizeConstants.SuccessAdd });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto() { Status = false, Message = ex.Message });
            }
        }
    }
}
