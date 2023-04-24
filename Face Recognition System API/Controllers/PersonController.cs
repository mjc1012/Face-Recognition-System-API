using Face_Recognition_System_Data.Dtos;
using Face_Recognition_System_Domain.Contracts;
using Face_Recognition_System_Domain.Services;
using Microsoft.AspNetCore.Mvc;
using static Face_Recognition_System_Data.Constants;

namespace Face_Recognition_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : Controller
    {

        private readonly IUnitOfWork _uow;
        public PersonController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet("{pairId}")]
        public async Task<IActionResult> Get(int pairId)
        {
            try
            {
                PersonDto responseData = await _uow.PersonService.FindByPairId(pairId);
                return StatusCode(StatusCodes.Status200OK, new ResponseDto() { Status = true, Message = BaseConstants.RetrievedData, Value = responseData });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto() { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PersonDto person)
        {
            try
            {
                person.FirstName = person.FirstName.ToUpper();
                person.MiddleName = (person.MiddleName != "" && person.MiddleName != null) ? person.MiddleName.ToUpper() : "";
                person.LastName = person.LastName.ToUpper();
                List<string> validationErrors = await _uow.PersonHandler.CanAdd(person);

                if (validationErrors.Any())
                {
                    return StatusCode(StatusCodes.Status200OK, new ResponseDto() { Status = false, Message = BaseConstants.ErrorList, Value = validationErrors });
                }
                else
                {
                    await _uow.PersonService.Create(person);
                    return StatusCode(StatusCodes.Status200OK, new ResponseDto() { Status = true, Message = FaceToRecognizeConstants.SuccessAdd });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto() { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] PersonDto person)
        {
            try
            {
                person.FirstName = person.FirstName.ToUpper();
                person.MiddleName = (person.MiddleName != "" && person.MiddleName != null) ? person.MiddleName.ToUpper() : "";
                person.LastName = person.LastName.ToUpper();
                List<string> validationErrors = await _uow.PersonHandler.CanUpdate(person);

                if (validationErrors.Any())
                {
                    return StatusCode(StatusCodes.Status200OK, new ResponseDto() { Status = false, Message = BaseConstants.ErrorList, Value = validationErrors });
                }
                else
                {
                    await _uow.PersonService.Update(person);
                    return StatusCode(StatusCodes.Status200OK, new ResponseDto() { Status = true, Message = FaceToRecognizeConstants.SuccessAdd });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto() { Status = false, Message = ex.Message });
            }
        }

        [HttpPut("delete-people")]
        public async Task<IActionResult> DeletePeople([FromBody] DeleteRangeDto range)
        {
            try
            {
                List<string> validationErrors = await _uow.PersonHandler.CanDelete(range);

                if (validationErrors.Any())
                {
                    return StatusCode(StatusCodes.Status200OK, new ResponseDto() { Status = false, Message = BaseConstants.ErrorList, Value = validationErrors });
                }
                else
                {
                    await _uow.PersonService.Delete(range);
                    return StatusCode(StatusCodes.Status200OK, new ResponseDto() { Status = true, Message = FaceToRecognizeConstants.SuccessAdd });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto() { Status = false, Message = ex.Message });
            }
        }

        [HttpDelete("{pairId}")]
        public async Task<IActionResult> DeleteByPairId(int pairId)
        {
            try
            {
                List<string> validationErrors = await _uow.PersonHandler.CanDelete(pairId);


                if (validationErrors.Any())
                {
                    return StatusCode(StatusCodes.Status200OK, new ResponseDto() { Status = false, Message = BaseConstants.ErrorList, Value = validationErrors });
                }
                else
                {
                    _uow.ImageService.DeleteFolder(pairId);
                    await _uow.PersonService.DeleteByPairId(pairId);
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
