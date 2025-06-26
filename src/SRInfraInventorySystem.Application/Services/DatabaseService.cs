using AutoMapper;
using SRInfraInventorySystem.Application.DTOs;
using SRInfraInventorySystem.Core.Entities;
using SRInfraInventorySystem.Repository.Interfaces;
using SRInfraInventorySystem.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRInfraInventorySystem.Application.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly IDatabaseRepository _databaseRepository;
        private readonly IMapper _mapper;

        public DatabaseService(IDatabaseRepository databaseRepository, IMapper mapper)
        {
            _databaseRepository = databaseRepository;
            _mapper = mapper;
        }

        public async Task<ApiResult<IEnumerable<DatabaseDto>>> GetAllAsync()
        {
            try
            {
                var databases = await _databaseRepository.GetDatabasesWithDetailsAsync();
                var databaseDtos = _mapper.Map<IEnumerable<DatabaseDto>>(databases);
                return ApiResult<IEnumerable<DatabaseDto>>.SuccessResult(databaseDtos);
            }
            catch (Exception ex)
            {
                return ApiResult<IEnumerable<DatabaseDto>>.ErrorResult($"Veritabanları getirilirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResult<DatabaseDto>> GetByIdAsync(Guid id)
        {
            try
            {
                var database = await _databaseRepository.GetDatabaseWithDetailsByIdAsync(id);
                if (database == null)
                    return ApiResult<DatabaseDto>.ErrorResult("Veritabanı bulunamadı");

                var databaseDto = _mapper.Map<DatabaseDto>(database);
                return ApiResult<DatabaseDto>.SuccessResult(databaseDto);
            }
            catch (Exception ex)
            {
                return ApiResult<DatabaseDto>.ErrorResult($"Veritabanı getirilirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResult<DatabaseDto>> CreateAsync(CreateDatabaseDto createDatabaseDto)
        {
            try
            {
                var database = _mapper.Map<Database>(createDatabaseDto);
                var createdDatabase = await _databaseRepository.AddAsync(database);
                var databaseDto = _mapper.Map<DatabaseDto>(createdDatabase);
                return ApiResult<DatabaseDto>.SuccessResult(databaseDto, "Veritabanı başarıyla oluşturuldu");
            }
            catch (Exception ex)
            {
                return ApiResult<DatabaseDto>.ErrorResult($"Veritabanı oluşturulurken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResult<DatabaseDto>> UpdateAsync(Guid id, CreateDatabaseDto updateDatabaseDto)
        {
            try
            {
                var existingDatabase = await _databaseRepository.GetByIdAsync(id);
                if (existingDatabase == null)
                    return ApiResult<DatabaseDto>.ErrorResult("Veritabanı bulunamadı");

                _mapper.Map(updateDatabaseDto, existingDatabase);
                var updatedDatabase = await _databaseRepository.UpdateAsync(existingDatabase);
                var databaseDto = _mapper.Map<DatabaseDto>(updatedDatabase);
                return ApiResult<DatabaseDto>.SuccessResult(databaseDto, "Veritabanı başarıyla güncellendi");
            }
            catch (Exception ex)
            {
                return ApiResult<DatabaseDto>.ErrorResult($"Veritabanı güncellenirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResult<DatabaseDto>> UpdatePartialAsync(Guid id, UpdateDatabaseDto updateDatabaseDto)
        {
            try
            {
                var existingDatabase = await _databaseRepository.GetByIdAsync(id);
                if (existingDatabase == null)
                    return ApiResult<DatabaseDto>.ErrorResult("Veritabanı bulunamadı");

                // Sadece null olmayan alanları güncelle
                if (updateDatabaseDto.Name != null)
                    existingDatabase.Name = updateDatabaseDto.Name;
                
                if (updateDatabaseDto.Type != null)
                    existingDatabase.Type = updateDatabaseDto.Type;
                
                if (updateDatabaseDto.Version != null)
                    existingDatabase.Version = updateDatabaseDto.Version;
                
                if (updateDatabaseDto.ConnectionString != null)
                    existingDatabase.ConnectionString = updateDatabaseDto.ConnectionString;
                
                if (updateDatabaseDto.ResponsiblePerson != null)
                    existingDatabase.ResponsiblePerson = updateDatabaseDto.ResponsiblePerson;
                
                if (updateDatabaseDto.AccessPermissions != null)
                    existingDatabase.AccessPermissions = updateDatabaseDto.AccessPermissions;
                
                if (updateDatabaseDto.ConnectionTools != null)
                    existingDatabase.ConnectionTools = updateDatabaseDto.ConnectionTools;
                
                if (updateDatabaseDto.Description != null)
                    existingDatabase.Description = updateDatabaseDto.Description;

                var updatedDatabase = await _databaseRepository.UpdateAsync(existingDatabase);
                var databaseDto = _mapper.Map<DatabaseDto>(updatedDatabase);
                return ApiResult<DatabaseDto>.SuccessResult(databaseDto, "Veritabanı başarıyla güncellendi");
            }
            catch (Exception ex)
            {
                return ApiResult<DatabaseDto>.ErrorResult($"Veritabanı güncellenirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResult<bool>> DeleteAsync(Guid id)
        {
            try
            {
                var database = await _databaseRepository.GetByIdAsync(id);
                if (database == null)
                    return ApiResult<bool>.ErrorResult("Veritabanı bulunamadı");

                // Bağlı veri kontrolü
                var hasRelatedData = await _databaseRepository.HasRelatedDataAsync(id);
                if (hasRelatedData)
                    return ApiResult<bool>.ErrorResult("Bu veritabanı silinemez. Erişim logları bulunmaktadır.");

                // Veritabanını soft delete yap
                await _databaseRepository.DeleteAsync(database);
                return ApiResult<bool>.SuccessResult(true, "Veritabanı başarıyla silindi");
            }
            catch (Exception ex)
            {
                return ApiResult<bool>.ErrorResult($"Veritabanı silinirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResult<IEnumerable<DatabaseDto>>> GetByServerAsync(Guid serverId)
        {
            try
            {
                var databases = await _databaseRepository.GetDatabasesByServerAsync(serverId);
                var databaseDtos = _mapper.Map<IEnumerable<DatabaseDto>>(databases);
                return ApiResult<IEnumerable<DatabaseDto>>.SuccessResult(databaseDtos);
            }
            catch (Exception ex)
            {
                return ApiResult<IEnumerable<DatabaseDto>>.ErrorResult($"Veritabanları getirilirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResult<IEnumerable<DatabaseDto>>> GetByTypeAsync(string type)
        {
            try
            {
                var databases = await _databaseRepository.GetDatabasesByTypeAsync(type);
                var databaseDtos = _mapper.Map<IEnumerable<DatabaseDto>>(databases);
                return ApiResult<IEnumerable<DatabaseDto>>.SuccessResult(databaseDtos);
            }
            catch (Exception ex)
            {
                return ApiResult<IEnumerable<DatabaseDto>>.ErrorResult($"Veritabanları getirilirken hata oluştu: {ex.Message}");
            }
        }
    }
} 