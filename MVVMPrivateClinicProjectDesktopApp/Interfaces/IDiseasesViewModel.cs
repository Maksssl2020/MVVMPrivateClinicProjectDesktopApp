using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Interfaces;

public interface IDiseasesViewModel {
    public void UpdateDiseasesDto(IEnumerable<DiseaseDto> diseaseDtos);
}