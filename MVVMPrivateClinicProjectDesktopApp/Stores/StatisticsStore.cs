using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.UnitOfWork;

namespace MVVMPrivateClinicProjectDesktopApp.Stores;

public class StatisticsStore {
    private readonly IUnitOfWork _unitOfWork;

    private readonly List<StatisticDto> _statistics;
    private readonly Lazy<Task> _initializeLazy;
    
    public IEnumerable<StatisticDto> Statistics => _statistics;

    public StatisticsStore(IUnitOfWork unitOfWork){
        _unitOfWork = unitOfWork;
        _statistics = [];
        
        _initializeLazy = new Lazy<Task>(InitializeStatistics);
    }

    public async Task LoadStatistics(){
        await _initializeLazy.Value;
    }

    private async Task InitializeStatistics(){
        var amountOfPatients = await _unitOfWork.PatientRepository.CountPatientsAsync();
        var amountOfDoctors = await _unitOfWork.DoctorRepository.CountDoctorsAsync();
        var amountOfIssuedPrescriptions = await _unitOfWork.PrescriptionRepository.CountIssuedPrescriptionsAsync();
        var amountOfIssuedReferrals = await _unitOfWork.ReferralRepository.CountIssuedReferralsAsync();
        var amountAppointments = await _unitOfWork.AppointmentRepository.CountAppointmentsAsync();
        var totalInvoicesSum = await _unitOfWork.InvoiceRepository.CountTotalInvoicesSumAsync();
        var amountOfIssuedDiagnosis = await _unitOfWork.DiagnosesRepository.CountIssuedDiagnosisAsync();
        
        _statistics.Clear();
        _statistics.Add(new StatisticDto("Amount of Patients", amountOfPatients.ToString()));
        _statistics.Add(new StatisticDto("Amount of Doctors", amountOfDoctors.ToString()));
        _statistics.Add(new StatisticDto("Issued Prescriptions", amountOfIssuedPrescriptions.ToString()));
        _statistics.Add(new StatisticDto("Issued Referrals", amountOfIssuedReferrals.ToString()));
        _statistics.Add(new StatisticDto("Amount Of Appointments", amountAppointments.ToString()));
        _statistics.Add(new StatisticDto("Total Invoices Sum", $"{totalInvoicesSum:C}"));
        _statistics.Add(new StatisticDto("Issued Diagnosis", amountOfIssuedDiagnosis.ToString()));
    }
}