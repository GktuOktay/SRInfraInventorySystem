using AutoMapper;
using SRInfraInventorySystem.Application.DTOs;
using SRInfraInventorySystem.Core.Entities;
using SRInfraInventorySystem.Core.Enums;
using SRInfraInventorySystem.Repository.Interfaces;
using SRInfraInventorySystem.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRInfraInventorySystem.Application.Services
{
    public class ServerService : IServerService
    {
        private readonly IServerRepository _serverRepository;
        private readonly IMapper _mapper;

        public ServerService(IServerRepository serverRepository, IMapper mapper)
        {
            _serverRepository = serverRepository;
            _mapper = mapper;
        }

        public async Task<ApiResult<IEnumerable<ServerDto>>> GetAllAsync()
        {
            try
            {
                var servers = await _serverRepository.GetServersWithDetailsAsync();
                var serverDtos = ApiResult<IEnumerable<ServerDto>>.SuccessResult(_mapper.Map<IEnumerable<ServerDto>>(servers));
                return serverDtos;
            }
            catch (Exception ex)
            {
                return ApiResult<IEnumerable<ServerDto>>.ErrorResult($"Sunucular getirilirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResult<ServerDto>> GetByIdAsync(Guid id)
        {
            try
            {
                var server = await _serverRepository.GetServerWithDetailsByIdAsync(id);
                if (server == null)
                    return ApiResult<ServerDto>.ErrorResult("Sunucu bulunamadı");

                var serverDto = _mapper.Map<ServerDto>(server);
                return ApiResult<ServerDto>.SuccessResult(serverDto);
            }
            catch (Exception ex)
            {
                return ApiResult<ServerDto>.ErrorResult($"Sunucu getirilirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResult<ServerDto>> CreateAsync(CreateServerDto createServerDto)
        {
            try
            {
                var server = _mapper.Map<Server>(createServerDto);
                var createdServer = await _serverRepository.AddAsync(server);
                var serverDto = _mapper.Map<ServerDto>(createdServer);
                return ApiResult<ServerDto>.SuccessResult(serverDto, "Sunucu başarıyla oluşturuldu");
            }
            catch (Exception ex)
            {
                return ApiResult<ServerDto>.ErrorResult($"Sunucu oluşturulurken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResult<ServerDto>> UpdateAsync(Guid id, CreateServerDto updateServerDto)
        {
            try
            {
                var existingServer = await _serverRepository.GetByIdAsync(id);
                if (existingServer == null)
                    return ApiResult<ServerDto>.ErrorResult("Sunucu bulunamadı");

                _mapper.Map(updateServerDto, existingServer);
                var updatedServer = await _serverRepository.UpdateAsync(existingServer);
                var serverDto = _mapper.Map<ServerDto>(updatedServer);
                return ApiResult<ServerDto>.SuccessResult(serverDto, "Sunucu başarıyla güncellendi");
            }
            catch (Exception ex)
            {
                return ApiResult<ServerDto>.ErrorResult($"Sunucu güncellenirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResult<bool>> DeleteAsync(Guid id)
        {
            try
            {
                var server = await _serverRepository.GetByIdAsync(id);
                if (server == null)
                    return ApiResult<bool>.ErrorResult("Sunucu bulunamadı");

                // Bağlı veri kontrolü
                var hasRelatedData = await _serverRepository.HasRelatedDataAsync(id);
                if (hasRelatedData)
                    return ApiResult<bool>.ErrorResult("Bu sunucu silinemez. Bağlı uygulamaları, veritabanları veya kullanım geçmişi bulunmaktadır.");

                // Sunucuyu soft delete yap
                await _serverRepository.DeleteAsync(server);
                return ApiResult<bool>.SuccessResult(true, "Sunucu başarıyla silindi");
            }
            catch (Exception ex)
            {
                return ApiResult<bool>.ErrorResult($"Sunucu silinirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResult<IEnumerable<ServerDto>>> GetByStatusAsync(ServerStatus status)
        {
            try
            {
                var servers = await _serverRepository.GetServersByStatusAsync(status);
                var serverDtos = ApiResult<IEnumerable<ServerDto>>.SuccessResult(_mapper.Map<IEnumerable<ServerDto>>(servers));
                return serverDtos;
            }
            catch (Exception ex)
            {
                return ApiResult<IEnumerable<ServerDto>>.ErrorResult($"Sunucular getirilirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResult<IEnumerable<ServerDto>>> GetByOperatingSystemAsync(string operatingSystem)
        {
            try
            {
                var servers = await _serverRepository.GetServersByOperatingSystemAsync(operatingSystem);
                var serverDtos = ApiResult<IEnumerable<ServerDto>>.SuccessResult(_mapper.Map<IEnumerable<ServerDto>>(servers));
                return serverDtos;
            }
            catch (Exception ex)
            {
                return ApiResult<IEnumerable<ServerDto>>.ErrorResult($"Sunucular getirilirken hata oluştu: {ex.Message}");
            }
        }
    }
} 