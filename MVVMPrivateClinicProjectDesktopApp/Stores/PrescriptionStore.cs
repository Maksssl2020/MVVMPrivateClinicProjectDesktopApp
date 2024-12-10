using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.UnitOfWork;

namespace MVVMPrivateClinicProjectDesktopApp.Stores;

public class PrescriptionStore {
    private readonly IUnitOfWork _unitOfWork;

    private readonly List<PrescriptionDto> _prescriptionsDto;
    private readonly Lazy<Task> _initializeLazy;
    private readonly List<PrescriptionDto> _selectedPatientPrescriptionsDto;

    public IEnumerable<PrescriptionDto> PrescriptionsDto => _prescriptionsDto;
    public IEnumerable<PrescriptionDto> SelectedPatientPrescriptionsDto => _selectedPatientPrescriptionsDto;

    private int _selectedPatientId;
    public int SelectedPatientId {
        get => _selectedPatientId;
        set {
            _selectedPatientId = value;
            _selectedPatientPrescriptionsDto.Clear();
        }
    }
    
    private int _selectedPrescriptionId;
    public int SelectedPrescriptionId {
        get => _selectedPrescriptionId;
        set {
            _selectedPrescriptionId = value;
            SelectedPrescription = null!;
        }
    }

    public PrescriptionDetailsDto SelectedPrescription { get; set; } = null!;

    public event Action<PrescriptionDto>? PrescriptionCreated;
    
    public PrescriptionStore(IUnitOfWork unitOfWork){
        _unitOfWork = unitOfWork;
        
        _prescriptionsDto = [];
        _selectedPatientPrescriptionsDto = [];
        
        _initializeLazy = new Lazy<Task>(InitializePrescriptions);
    }

    public async Task LoadPrescriptions(){
        await _initializeLazy.Value;
    }

    public async Task LoadPatientPrescriptions(){
        var loadedPatientPrescriptions = await _unitOfWork.PrescriptionRepository.GetPatientAllPrescriptionsDtoAsync(SelectedPatientId);
        _selectedPatientPrescriptionsDto.AddRange(loadedPatientPrescriptions);
    }

    public async Task LoadPrescriptionById(){
        var loadedPrescription = await _unitOfWork.PrescriptionRepository.GetPrescriptionDetailsDtoByIdAsync(SelectedPrescriptionId);
        SelectedPrescription = loadedPrescription;
    }
    
    public async Task CreatePrescription(SavePrescriptionRequest prescriptionRequest){
        var savedPrescription = await _unitOfWork.PrescriptionRepository.SavePrescriptionAsync(prescriptionRequest);
        _prescriptionsDto.Add(savedPrescription);
        _selectedPatientPrescriptionsDto.Add(savedPrescription);
        
        OnPrescriptionCreated(savedPrescription);
    }
    
    private void OnPrescriptionCreated(PrescriptionDto prescription){
        PrescriptionCreated?.Invoke(prescription);
    }
    
    private async Task InitializePrescriptions(){
        var prescriptionsDto = await _unitOfWork.PrescriptionRepository.GetAllPrescriptionsDtoAsync();
        
        _prescriptionsDto.Clear();
        _prescriptionsDto.AddRange(prescriptionsDto);
    }
}