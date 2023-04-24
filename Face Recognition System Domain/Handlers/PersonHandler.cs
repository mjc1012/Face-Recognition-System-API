using Face_Recognition_System_Data.Dtos;
using Face_Recognition_System_Domain.Contracts;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Face_Recognition_System_Data.Constants;

namespace Face_Recognition_System_Domain.Handlers
{
    public class PersonHandler : IPersonHandler
    {
        private readonly IPersonService _personService;
        public PersonHandler(IPersonService personService)
        {
            _personService = personService;
        }

        public async Task<List<string>> CanAdd(PersonDto person)
        {
            var validationErrors = new List<string>();

            if (person == null)
            {
                validationErrors.Add(PersonConstants.EntryInvalid);
            }
            else
            {
                Match match = Regex.Match(person.FirstName, "[^a-z]", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    validationErrors.Add(PersonConstants.FirstNameContainsDigitsOrSpecialChar);
                }
                match = Regex.Match(person.MiddleName, "[^a-z]", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    validationErrors.Add(PersonConstants.MiddleNameContainsDigitsOrSpecialChar);
                }
                match = Regex.Match(person.LastName, "[^a-z]", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    validationErrors.Add(PersonConstants.LastNameContainsDigitsOrSpecialChar);
                }
                if(await _personService.PairIdExists(person))
                {
                    validationErrors.Add(PersonConstants.PairIdExists);
                }
                if (await _personService.NameExists(person))
                {
                    validationErrors.Add(PersonConstants.Exists);
                }
            }

            return validationErrors;
        }

        public async Task<List<string>> CanUpdate(PersonDto person)
        {
            var validationErrors = new List<string>();

            if (person == null)
            {
                validationErrors.Add(PersonConstants.EntryInvalid);
            }
            else
            {
                Match match = Regex.Match(person.FirstName, "[^a-z]", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    validationErrors.Add(PersonConstants.FirstNameContainsDigitsOrSpecialChar);
                }
                match = Regex.Match(person.MiddleName, "[^a-z]", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    validationErrors.Add(PersonConstants.MiddleNameContainsDigitsOrSpecialChar);
                }
                match = Regex.Match(person.LastName, "[^a-z]", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    validationErrors.Add(PersonConstants.LastNameContainsDigitsOrSpecialChar);
                }
                if (await _personService.FindByPairId(person.PairId) == null)
                {
                    validationErrors.Add(PersonConstants.PairIdDoesNotExist);
                }
                if (await _personService.NameExists(person))
                {
                    validationErrors.Add(PersonConstants.Exists);
                }
            }

            return validationErrors;
        }

        public async Task<List<string>> CanDelete(int id)
        {
            var validationErrors = new List<string>();

            if (await _personService.FindByPairId(id) == null)
            {
                validationErrors.Add(PersonConstants.EntryInvalid);
            }

            return validationErrors;
        }

        public async Task<List<string>> CanDelete(DeleteRangeDto deleteRange)
        {
            var validationErrors = new List<string>();

            if (await _personService.RetrieveData(deleteRange.Ids) == null)
            {
                validationErrors.Add(PersonConstants.EntryInvalid);
            }

            return validationErrors;
        }
    }
}
