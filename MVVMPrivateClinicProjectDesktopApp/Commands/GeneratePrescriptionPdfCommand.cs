using MVVMPrivateClinicProjectDesktopApp.Stores;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class GeneratePrescriptionPdfCommand(PrescriptionStore prescriptionStore) : AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        try {
            if (parameter is int prescriptionId) {
                await prescriptionStore.GeneratePrescriptionPdfDocument(prescriptionId);
            }
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}