using Face_Recognition_System_Data.Dtos;
using Face_Recognition_System_Data.Models;
using Face_Recognition_System_Domain.Contracts;
using Face_Recognition_System_Domain.Services;
using Microsoft.AspNetCore.Mvc;
using static Face_Recognition_System_Data.Constants;

namespace Face_Recognition_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaceToRecognizeController : Controller
    {
        private readonly IUnitOfWork _uow;
        public FaceToRecognizeController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpPost]
        public async Task<IActionResult> RecognizeFace([FromBody] FaceToRecognizeDto face)
        {
            try
            {
                List<string> validationErrors = _uow.FaceToRecognizeHandler.CanAdd(face);

                if (validationErrors.Any())
                {
                    return StatusCode(StatusCodes.Status200OK, new ResponseDto() { Status = false, Message = BaseConstants.ErrorList, Value = validationErrors });
                }
                else
                {
                    face.ImageFile = _uow.ImageService.SaveImage(face.Base64String, -1);
                    await _uow.FaceToRecognizeService.Create(face);
                    int predictedPersonId = _uow.FaceRecognitionService.RecognizeFace(face);
                    if (predictedPersonId == -1)
                    {
                        return StatusCode(StatusCodes.Status200OK, new ResponseDto() { Status = false, Message = BaseConstants.ErrorList, Value = new List<string>() { "Cannot Predict Face"} });
                    }
                    return StatusCode(StatusCodes.Status200OK, new ResponseDto() { Status = true, Message = FaceToRecognizeConstants.SuccessAdd, Value = await _uow.PersonService.FindById(predictedPersonId)});
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto() { Status = false, Message = ex.Message });
            }
        }
    }
}
