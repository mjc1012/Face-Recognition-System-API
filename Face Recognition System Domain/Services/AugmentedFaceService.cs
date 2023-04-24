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
    public class AugmentedFaceService : IAugmentedFaceService
    {
        private readonly IAugmentedFaceRepository _augmentedFaceRepository;
        private readonly IMapper _mapper;

        public AugmentedFaceService(IAugmentedFaceRepository augmentedFaceRepository, IMapper mapper)
        {
            _augmentedFaceRepository = augmentedFaceRepository;
            _mapper = mapper;
        }
        public async Task<List<AugmentedFaceDto>> RetrieveData(int faceToTrainId)
        {
            try
            {
                return _mapper.Map<List<AugmentedFaceDto>>(await _augmentedFaceRepository.RetrieveData(faceToTrainId));
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task Create(AugmentedFace augmentedFace)
        {
            try
            {
                await _augmentedFaceRepository.Create(augmentedFace);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
