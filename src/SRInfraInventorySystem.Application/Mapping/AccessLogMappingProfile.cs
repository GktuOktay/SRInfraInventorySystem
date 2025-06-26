using AutoMapper;
using SRInfraInventorySystem.Application.DTOs;
using SRInfraInventorySystem.Core.Entities;

namespace SRInfraInventorySystem.Application.Mapping
{
    public class AccessLogMappingProfile : Profile
    {
        public AccessLogMappingProfile()
        {
            CreateMap<AccessLog, AccessLogDto>()
                .ForMember(dest => dest.ServerName, opt => opt.MapFrom(src => src.Server.Name))
                .ForMember(dest => dest.ApplicationName, opt => opt.MapFrom(src => src.Application.Name))
                .ForMember(dest => dest.DatabaseName, opt => opt.MapFrom(src => src.Database.Name));

            CreateMap<CreateAccessLogDto, AccessLog>();
        }
    }
} 