using MVVMPrivateClinicProjectDesktopApp.Stores;
using MVVMPrivateClinicProjectDesktopApp.ViewModels;

namespace MVVMPrivateClinicProjectDesktopApp.Commands;

public class LoadStatisticsCommand(HomeViewModel viewModel, StatisticsStore statisticsStore): AsyncRelayCommand {
    public override async Task ExecuteAsync(object? parameter){
        try {
            await statisticsStore.LoadStatistics();
            viewModel.UpdateStatistics(statisticsStore.Statistics);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}