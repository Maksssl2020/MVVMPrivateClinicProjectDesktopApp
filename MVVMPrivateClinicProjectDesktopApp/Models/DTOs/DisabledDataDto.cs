using System.ComponentModel.DataAnnotations;
using MVVMPrivateClinicProjectDesktopApp.Interfaces;

namespace MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

public class DisabledDataDto : IEntity {
    public int Id { get; set; }
    public required DisabledDataType DisabledDataType { get; set; }
    public required DateTime DisabledDate { get; set; }
    public required string DataDetails { get; set; }
}