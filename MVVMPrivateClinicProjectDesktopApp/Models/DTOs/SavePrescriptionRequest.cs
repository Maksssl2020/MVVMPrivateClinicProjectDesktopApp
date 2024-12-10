using System.Windows.Documents;

namespace MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

public class SavePrescriptionRequest {
    public PrescriptionValidity PrescriptionValidity { get; set; }
    public required string PrescriptionDescription { get; set; }
    public int IdPatient { get; set; }
    public int IdDoctor { get; set; }
    public required List<MedicineDto> SelectedMedicines { get; set; }
}