using AutoMapper;
using MotorcycleLicenseTrainingAPI.DTO;
using MotorcycleLicenseTrainingAPI.Model;

namespace MotorcycleLicenseTrainingAPI.Mapper
{
    public class PracticeHistoryProfile : Profile
    {
        public PracticeHistoryProfile()
        {
            CreateMap<PracticeHistory, PracticeHistoryDto>().ReverseMap();
        }
    }
}
