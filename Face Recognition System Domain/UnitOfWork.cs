using Face_Recognition_System_Data.Contracts;
using Face_Recognition_System_Data.Repositories;
using Face_Recognition_System_Data;
using Face_Recognition_System_Domain.Contracts;
using Face_Recognition_System_Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Face_Recognition_System_Domain.Handlers;

namespace Face_Recognition_System_Domain
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IMapper _mapper;
        private readonly IAugmentedFaceRepository _augmentedFaceRepository;
        private readonly IFaceExpressionRepository _faceExpressionRepository;
        private readonly IFaceRecognitionStatusRepository _faceRecognitionStatusRepository;
        private readonly IFaceToRecognizeRepository _faceToRecognizeRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IFaceToTrainRepository _faceToTrainRepository;
        public UnitOfWork(IMapper mapper, IAugmentedFaceRepository augmentedFaceRepository, IFaceExpressionRepository faceExpressionRepository, 
            IFaceRecognitionStatusRepository faceRecognitionStatusRepository, IFaceToRecognizeRepository faceToRecognizeRepository, 
            IPersonRepository personRepository, IFaceToTrainRepository faceToTrainRepository)
        {
            _mapper = mapper;
            _augmentedFaceRepository = augmentedFaceRepository;
            _faceExpressionRepository = faceExpressionRepository;
            _faceRecognitionStatusRepository = faceRecognitionStatusRepository;
            _faceToRecognizeRepository = faceToRecognizeRepository;
            _personRepository = personRepository;
            _faceToTrainRepository = faceToTrainRepository;
        }

        public IAugmentedFaceService AugmentedFaceService => new AugmentedFaceService(_augmentedFaceRepository, _mapper);
        public IFaceExpressionService FaceExpressionService => new FaceExpressionService(_faceExpressionRepository, _mapper);
        public IFaceRecognitionStatusService FaceRecognitionStatusService => new FaceRecognitionStatusService(_faceRecognitionStatusRepository, _mapper);
        public IFaceToRecognizeService FaceToRecognizeService => new FaceToRecognizeService(_faceToRecognizeRepository, _mapper);
        public IPersonService PersonService => new PersonService(_personRepository, _mapper);
        public IFaceRecognitionService FaceRecognitionService => new FaceRecognitionService();
        public IFaceToTrainService FaceToTrainService => new FaceToTrainService(_faceToTrainRepository, _mapper);
        public IImageService ImageService => new ImageService(FaceRecognitionService);

        public IImageAugmentationService ImageAugmentationService => new ImageAugmentationService(AugmentedFaceService, ImageService);
        public IFaceRecognitionStatusHandler FaceRecognitionStatusHandler => new FaceRecognitionStatusHandler(FaceRecognitionStatusService, FaceToRecognizeService, PersonService);
        public IFaceToRecognizeHandler FaceToRecognizeHandler => new FaceToRecognizeHandler();
        public IFaceToTrainHandler FaceToTrainHandler => new FaceToTrainHandler(FaceToTrainService, FaceExpressionService, PersonService);
        public IPersonHandler PersonHandler => new PersonHandler(PersonService);

    }
}
