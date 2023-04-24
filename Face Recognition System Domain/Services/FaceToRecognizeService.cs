using AutoMapper;
using Face_Recognition_System_Data.Contracts;
using Face_Recognition_System_Data.Dtos;
using Face_Recognition_System_Data.Models;
using Face_Recognition_System_Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Face_Recognition_System_Domain.Services
{
    public class FaceToRecognizeService : IFaceToRecognizeService
    {
        private readonly IFaceToRecognizeRepository _faceToRecognizeRepository;
        private readonly IMapper _mapper;

        public FaceToRecognizeService(IFaceToRecognizeRepository faceToRecognizeRepository, IMapper mapper)
        {
            _faceToRecognizeRepository = faceToRecognizeRepository;
            _mapper = mapper;
        }

        public async Task<FaceToRecognizeDto> Find(int id)
        {
            try
            {
                return _mapper.Map<FaceToRecognizeDto>(await _faceToRecognizeRepository.Find(id));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Create(FaceToRecognizeDto faceToRecognize)
        {
            try
            {
                await _faceToRecognizeRepository.Create(_mapper.Map<FaceToRecognize>(faceToRecognize));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
