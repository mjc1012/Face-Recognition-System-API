using AutoMapper;
using Face_Recognition_System_Data.Contracts;
using Face_Recognition_System_Data.Dtos;
using Face_Recognition_System_Data.Models;
using Face_Recognition_System_Data.Repositories;
using Face_Recognition_System_Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Face_Recognition_System_Domain.Services
{
    public class FaceRecognitionStatusService : IFaceRecognitionStatusService
    {
        private readonly IFaceRecognitionStatusRepository _faceRecognitionStatusRepository;
        private readonly IMapper _mapper;

        public FaceRecognitionStatusService(IFaceRecognitionStatusRepository faceRecognitionStatusRepository, IMapper mapper)
        {
            _faceRecognitionStatusRepository = faceRecognitionStatusRepository;
            _mapper = mapper;
        }

        public async Task<List<FaceRecognitionStatusDto>> RetrieveAll()
        {
            try
            {
                return _mapper.Map<List<FaceRecognitionStatusDto>>(await _faceRecognitionStatusRepository.RetrieveAll());
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task Create(FaceRecognitionStatusDto status, FaceToRecognizeDto face, PersonDto person)
        {
            try
            {
                await _faceRecognitionStatusRepository.Create(_mapper.Map<FaceRecognitionStatus>(status), _mapper.Map<FaceToRecognize>(face), _mapper.Map<Person>(person));
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> Exists(FaceRecognitionStatusDto status, FaceToRecognizeDto face, PersonDto person)
        {
            try
            {
                return await _faceRecognitionStatusRepository.Exists(_mapper.Map<FaceRecognitionStatus>(status), _mapper.Map<FaceToRecognize>(face), _mapper.Map<Person>(person));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
