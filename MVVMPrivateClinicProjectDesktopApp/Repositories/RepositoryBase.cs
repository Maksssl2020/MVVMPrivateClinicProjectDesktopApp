using MVVMPrivateClinicProjectDesktopApp.Models.Entities;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories;

public abstract class RepositoryBase {
    protected readonly PrivateClinicContext DbContext = new();
}