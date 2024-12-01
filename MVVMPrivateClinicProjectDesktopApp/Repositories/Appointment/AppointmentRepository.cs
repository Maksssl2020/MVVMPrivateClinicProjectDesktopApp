using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

namespace MVVMPrivateClinicProjectDesktopApp.Repositories.Appointment;

public class AppointmentRepository(IMapper mapper) : RepositoryBase, IAppointmentRepository {
    public async Task<IEnumerable<AppointmentDto>> GetAllAppointmentsAsync(){
        return await DbContext.Appointments
            .ProjectTo<AppointmentDto>(mapper.ConfigurationProvider)
            .ToListAsync();
    }
}