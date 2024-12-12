using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class CreateAppointmentCommand(
    AddNewAppointmentViewModel viewModel,
    AppointmentStore appointmentStore,
    AppointmentDateStore appointmentDateStore,
    InvoiceStore invoiceStore
    ) : AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        try {
            var saveAppointmentRequest = new SaveAppointmentRequest {
                AppointmentCost = viewModel.SelectedPricing.Price,
                AppointmentDate = viewModel.SelectedDay!.Value,
                AppointmentTime = viewModel.SelectedTime!.Value,
                IdPatient = viewModel.SelectedPatient.Id,
                IdDoctor = viewModel.SelectedDoctor.Id,
            };

            var saveAppointmentDateRequest = new SaveAppointmentDateRequest {
                AppointmentDate = viewModel.SelectedDay!.Value,
                AppointmentTime = viewModel.SelectedTime!.Value,
                IdPatient = viewModel.SelectedPatient.Id,
                IdDoctor = viewModel.SelectedDoctor.Id,
            };

            var saveNewInvoiceRequest = new SaveNewInvoiceRequest {
                Amount = viewModel.SelectedPricing.Price,
                IdPatient = viewModel.SelectedPatient.Id,
                IdPricing = viewModel.SelectedPricing.Id,
            };

            await appointmentStore.CreateAppointment(saveAppointmentRequest);
            await appointmentDateStore.CreateAppointmentDate(saveAppointmentDateRequest);
            await invoiceStore.CreateInvoice(saveNewInvoiceRequest);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}