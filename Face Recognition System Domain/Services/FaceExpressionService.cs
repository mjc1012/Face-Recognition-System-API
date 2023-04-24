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
    public class FaceExpressionService : IFaceExpressionService
    {
        private readonly IFaceExpressionRepository _faceExpressionRepository;
        private readonly IMapper _mapper;

        public FaceExpressionService(IFaceExpressionRepository faceExpressionRepository, IMapper mapper)
        {
            _faceExpressionRepository = faceExpressionRepository;
            _mapper = mapper;
        }

        public async Task<List<FaceExpressionDto>> RetrieveAll()
        {
            try
            {
                return _mapper.Map<List<FaceExpressionDto>>(await _faceExpressionRepository.RetrieveAll());
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<FaceExpressionDto> Find(int id)
        {
            try
            {
                return _mapper.Map<FaceExpressionDto>(await _faceExpressionRepository.Find(id));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
