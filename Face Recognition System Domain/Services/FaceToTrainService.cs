using AutoMapper;
using Face_Recognition_System_Data.Contracts;
using Face_Recognition_System_Data.Dtos;
using Face_Recognition_System_Data.Models;
using Face_Recognition_System_Data.Repositories;
using Face_Recognition_System_Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Face_Recognition_System_Domain.Services
{
    public class FaceToTrainService : IFaceToTrainService
    {
        private readonly IFaceToTrainRepository _faceToTrainRepository;
        private readonly IMapper _mapper;

        public FaceToTrainService(IFaceToTrainRepository faceToTrainRepository, IMapper mapper)
        {
            _mapper = mapper;
            _faceToTrainRepository = faceToTrainRepository;
        }

        public async Task<List<FaceToTrainDto>> RetrieveAll()
        {
            try
            {
                return _mapper.Map<List<FaceToTrainDto>>(await _faceToTrainRepository.RetrieveAll());
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<FaceToTrainDto> Find(int id)
        {
            try
            {
                return _mapper.Map<FaceToTrainDto>(await _faceToTrainRepository.Find(id));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<FaceToTrainDto>> RetrieveData(int id)
        {
            try
            {
                return _mapper.Map<List<FaceToTrainDto>>(await _faceToTrainRepository.RetrieveData(id));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<FaceExpressionDto> FindExpression(int pairId, List<FaceExpressionDto> expressions)
        {
            try
            {
                return _mapper.Map<FaceExpressionDto>(await _faceToTrainRepository.FindExpression(pairId, _mapper.Map<List<FaceExpression>>(expressions)));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Exists(int pairId, int expressionId)
        {
            try
            {
                return await _faceToTrainRepository.Exists(pairId, expressionId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<FaceToTrainDto> Create(FaceToTrainDto face, PersonDto person, FaceExpressionDto expression)
        {
            try
            {
                return _mapper.Map<FaceToTrainDto>(await _faceToTrainRepository.Create(_mapper.Map<FaceToTrain>(face), _mapper.Map<Person>(person), _mapper.Map<FaceExpression>(expression)));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                await _faceToTrainRepository.Delete(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Exists(FaceToTrainDto face, PersonDto person, FaceExpressionDto expression)
        {
            try
            {
                return await _faceToTrainRepository.Exists(_mapper.Map<FaceToTrain>(face), _mapper.Map<Person>(person), _mapper.Map<FaceExpression>(expression));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
