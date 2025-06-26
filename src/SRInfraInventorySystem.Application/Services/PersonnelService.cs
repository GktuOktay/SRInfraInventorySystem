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
    public class PersonnelService : IPersonnelService
    {
        private readonly IPersonnelRepository _personnelRepository;
        private readonly IMapper _mapper;

        public PersonnelService(IPersonnelRepository personnelRepository, IMapper mapper)
        {
            _personnelRepository = personnelRepository;
            _mapper = mapper;
        }

        public async Task<ApiResult<IEnumerable<PersonnelDto>>> GetAllAsync()
        {
            try
            {
                var personnel = await _personnelRepository.GetPersonnelWithDetailsAsync();
                var personnelDtos = _mapper.Map<IEnumerable<PersonnelDto>>(personnel);
                return ApiResult<IEnumerable<PersonnelDto>>.SuccessResult(personnelDtos);
            }
            catch (Exception ex)
            {
                return ApiResult<IEnumerable<PersonnelDto>>.ErrorResult($"Personel listesi getirilirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResult<PersonnelDto>> GetByIdAsync(Guid id)
        {
            try
            {
                var personnel = await _personnelRepository.GetPersonnelWithDetailsByIdAsync(id);
                if (personnel == null)
                    return ApiResult<PersonnelDto>.ErrorResult("Personel bulunamadı");

                var personnelDto = _mapper.Map<PersonnelDto>(personnel);
                return ApiResult<PersonnelDto>.SuccessResult(personnelDto);
            }
            catch (Exception ex)
            {
                return ApiResult<PersonnelDto>.ErrorResult($"Personel getirilirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResult<PersonnelDto>> CreateAsync(CreatePersonnelDto createPersonnelDto)
        {
            try
            {
                var personnel = _mapper.Map<Personnel>(createPersonnelDto);
                var createdPersonnel = await _personnelRepository.AddAsync(personnel);
                var personnelDto = _mapper.Map<PersonnelDto>(createdPersonnel);
                return ApiResult<PersonnelDto>.SuccessResult(personnelDto, "Personel başarıyla oluşturuldu");
            }
            catch (Exception ex)
            {
                return ApiResult<PersonnelDto>.ErrorResult($"Personel oluşturulurken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResult<PersonnelDto>> UpdateAsync(Guid id, CreatePersonnelDto updatePersonnelDto)
        {
            try
            {
                var existingPersonnel = await _personnelRepository.GetByIdAsync(id);
                if (existingPersonnel == null)
                    return ApiResult<PersonnelDto>.ErrorResult("Personel bulunamadı");

                _mapper.Map(updatePersonnelDto, existingPersonnel);
                var updatedPersonnel = await _personnelRepository.UpdateAsync(existingPersonnel);
                var personnelDto = _mapper.Map<PersonnelDto>(updatedPersonnel);
                return ApiResult<PersonnelDto>.SuccessResult(personnelDto, "Personel başarıyla güncellendi");
            }
            catch (Exception ex)
            {
                return ApiResult<PersonnelDto>.ErrorResult($"Personel güncellenirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResult<bool>> DeleteAsync(Guid id)
        {
            try
            {
                var personnel = await _personnelRepository.GetByIdAsync(id);
                if (personnel == null)
                    return ApiResult<bool>.ErrorResult("Personel bulunamadı");

                // Bağlı veri kontrolü
                var hasRelatedData = await _personnelRepository.HasRelatedDataAsync(id);
                if (hasRelatedData)
                    return ApiResult<bool>.ErrorResult("Bu personel silinemez. Manager olduğu departmanlar bulunmaktadır.");

                // Personeli soft delete yap
                await _personnelRepository.DeleteAsync(personnel);
                return ApiResult<bool>.SuccessResult(true, "Personel başarıyla silindi");
            }
            catch (Exception ex)
            {
                return ApiResult<bool>.ErrorResult($"Personel silinirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResult<IEnumerable<PersonnelDto>>> GetByDepartmentAsync(Guid departmentId)
        {
            try
            {
                var personnel = await _personnelRepository.GetPersonnelByDepartmentAsync(departmentId);
                var personnelDtos = _mapper.Map<IEnumerable<PersonnelDto>>(personnel);
                return ApiResult<IEnumerable<PersonnelDto>>.SuccessResult(personnelDtos);
            }
            catch (Exception ex)
            {
                return ApiResult<IEnumerable<PersonnelDto>>.ErrorResult($"Personel listesi getirilirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResult<IEnumerable<PersonnelDto>>> GetActivePersonnelAsync()
        {
            try
            {
                var personnel = await _personnelRepository.GetActivePersonnelAsync();
                var personnelDtos = _mapper.Map<IEnumerable<PersonnelDto>>(personnel);
                return ApiResult<IEnumerable<PersonnelDto>>.SuccessResult(personnelDtos);
            }
            catch (Exception ex)
            {
                return ApiResult<IEnumerable<PersonnelDto>>.ErrorResult($"Aktif personel listesi getirilirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResult<PersonnelDto>> GetByUserNameAsync(string userName)
        {
            try
            {
                var personnel = await _personnelRepository.GetPersonnelByUserNameAsync(userName);
                if (personnel == null)
                    return ApiResult<PersonnelDto>.ErrorResult("Personel bulunamadı");

                var personnelDto = _mapper.Map<PersonnelDto>(personnel);
                return ApiResult<PersonnelDto>.SuccessResult(personnelDto);
            }
            catch (Exception ex)
            {
                return ApiResult<PersonnelDto>.ErrorResult($"Personel getirilirken hata oluştu: {ex.Message}");
            }
        }
    }
} 