using MVVMPrivateClinicProjectDesktopApp.Models.DTOs;
using MVVMPrivateClinicProjectDesktopApp.UnitOfWork;

namespace MVVMPrivateClinicProjectDesktopApp.Stores;

public class InvoiceStore(IUnitOfWork unitOfWork) : EntityStore<InvoiceDto, InvoiceDetailsDto>(unitOfWork) {
    public override async Task CreateEntity(object entityRequest){
        if (entityRequest is SaveInvoiceRequest saveInvoiceRequest) {
            var savedInvoice = await UnitOfWork.InvoiceRepository.SaveInvoiceAsync(saveInvoiceRequest);
            Entities.Add(savedInvoice);
            
            OnEntityCreated(savedInvoice);
        }
    }

    public override async Task DeleteEntity(int entityId){
        await UnitOfWork.InvoiceRepository.DeleteEntityAsync(entityId);
        Entities.RemoveAll(e => e.Id == entityId);
        OnEntityDeleted(entityId);
    }

    public override async Task LoadEntityDetails(){
        var loadedInvoice = await UnitOfWork.InvoiceRepository.GetInvoiceDetailsDtoAsync(EntityIdToShowDetails);
        if (loadedInvoice != null) SelectedEntityDetails = loadedInvoice;
    }

    protected override async Task InitializeEntities(){
        var loadedInvoices = await UnitOfWork.InvoiceRepository.GetAllInvoicesDtoAsync();
        
        Entities.Clear();
        Entities.AddRange(loadedInvoices);
    }
}