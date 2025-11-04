using AutoMapper;
using Clean.Core.DTOs;
using Clean.Core.Entities;

namespace Clean.Service.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User mappings
            CreateMap<User, UserResponseDTO>();
            CreateMap<RegisterUserDTO, User>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.Tasks, opt => opt.Ignore());

            // Task mappings
            CreateMap<TaskItem, TaskDTO>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));
            CreateMap<TaskInputDTO, TaskItem>()
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

            // Category mappings
            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryDTO, Category>()
                .ForMember(dest => dest.Tasks, opt => opt.Ignore());

            // Additional User mappings for responses
            CreateMap<UserResponseDTO, User>()
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ForMember(dest => dest.Tasks, opt => opt.Ignore());
        }
    }
}
    
