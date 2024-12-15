namespace MVVMPrivateClinicProjectDesktopApp.Models.DTOs;

public class StatisticDto(string statisticName, string statisticValue) {
    public string StatisticName { get; set; } = statisticName;
    public string StatisticValue { get; set; } = statisticValue;
}