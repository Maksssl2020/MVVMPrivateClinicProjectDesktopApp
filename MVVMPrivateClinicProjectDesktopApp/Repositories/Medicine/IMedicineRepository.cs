namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Medicine;

public interface IMedicineRepository {
    Task<IEnumerable<Models.Entities.Medicine>> GetAllMedicinesAsync();
}