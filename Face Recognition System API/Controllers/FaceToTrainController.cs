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
    public class FaceToTrainController : Controller
    {

        private readonly IUnitOfWork _uow;
        public FaceToTrainController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet("{pairId}/person-faces")]
        public async Task<IActionResult> GetData(int pairId)
        {
            try
            {
                List<FaceToTrainDto> responseData = await _uow.FaceToTrainService.RetrieveData(pairId);
                return StatusCode(StatusCodes.Status200OK, new ResponseDto() { Status = true, Message = BaseConstants.RetrievedData, Value = responseData });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto() { Status = false, Message = ex.Message });
            }
        }

        [HttpGet("{pairId}/missing-expression")]
        public async Task<IActionResult> GetExpression(int pairId)
        {
            try
            {
                List<FaceExpressionDto> faceExpressions = await _uow.FaceExpressionService.RetrieveAll();
                FaceExpressionDto responseData = await _uow.FaceToTrainService.FindExpression(pairId, faceExpressions);
                return StatusCode(StatusCodes.Status200OK, new ResponseDto() { Status = true, Message = BaseConstants.RetrievedData, Value = responseData });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto() { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FaceToTrainDto face)
        {
            try
            {
                List<string> validationErrors = await _uow.FaceToTrainHandler.CanAdd(face);

                if (validationErrors.Any())
                {
                    return StatusCode(StatusCodes.Status200OK, new ResponseDto() { Status = false, Message = BaseConstants.ErrorList, Value = validationErrors });
                }
                else
                {
                    PersonDto person = await _uow.PersonService.FindByPairId(face.PairId);
                    face.ImageFile = _uow.ImageService.SaveImage(face.Base64String, person.PairId);
                    FaceExpressionDto expression = await _uow.FaceExpressionService.Find(face.FaceExpressionId);
                    FaceToTrainDto createdFace = await _uow.FaceToTrainService.Create(face, person, expression);
                    createdFace.PairId = person.PairId;
                    await _uow.ImageAugmentationService.RunImageAugmentation(createdFace);
                    return StatusCode(StatusCodes.Status200OK, new ResponseDto() { Status = true, Message = FaceToTrainConstants.SuccessAdd, Value = person });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto() { Status = false, Message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> HardDelete(int id)
        {
            try
            {
                List<string> validationErrors = await _uow.FaceToTrainHandler.CanDelete(id);


                if (validationErrors.Any())
                {
                    return StatusCode(StatusCodes.Status200OK, new ResponseDto() { Status = false, Message = BaseConstants.ErrorList, Value = validationErrors });
                }
                else
                {

                    FaceToTrainDto face = await _uow.FaceToTrainService.Find(id);
                    PersonDto person = await _uow.PersonService.FindById(face.PersonId);
                    face.PairId = person.PairId;
                    List<AugmentedFaceDto> augmentedFaces = await _uow.AugmentedFaceService.RetrieveData(face.Id);
                    _uow.ImageService.DeleteImage(face, augmentedFaces);
                    await _uow.FaceToTrainService.Delete(id);
                    return StatusCode(StatusCodes.Status200OK, new ResponseDto() { Status = true, Message = FaceToTrainConstants.SuccessDelete });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto() { Status = false, Message = ex.Message });
            }
        }
    }
}
