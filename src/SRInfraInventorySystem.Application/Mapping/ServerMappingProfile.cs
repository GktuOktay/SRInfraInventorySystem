using AutoMapper;
using SRInfraInventorySystem.Application.DTOs;
using SRInfraInventorySystem.Core.Entities;

namespace SRInfraInventorySystem.Application.Mapping
{
    public class ServerMappingProfile : Profile
    {
        public ServerMappingProfile()
        {
            CreateMap<Server, ServerDto>()
                .ForMember(dest => dest.Applications, opt => opt.MapFrom(src => src.Applications))
                .ForMember(dest => dest.Databases, opt => opt.MapFrom(src => src.Databases));
                
            CreateMap<CreateServerDto, Server>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.Applications, opt => opt.Ignore())
                .ForMember(dest => dest.Databases, opt => opt.Ignore())
                .ForMember(dest => dest.UsageHistory, opt => opt.Ignore());
        }
    }
} 