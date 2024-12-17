using System.ComponentModel;

namespace MVVMPrivateClinicProjectDesktopApp.Helpers;

public static class ApplySortingOptions {
    public static void ApplySortingWithTwoProperties(ICollectionView collectionView, SortingOptions selectedOption, string primarySortProperty, string secondarySortProperty){
        collectionView.SortDescriptions.Clear();
        
        switch (selectedOption) {
            case SortingOptions.AlphabeticalAscending: {
                collectionView.SortDescriptions.Add(
                    new SortDescription(primarySortProperty, ListSortDirection.Ascending)
                ); 
                collectionView.SortDescriptions.Add(
                    new SortDescription(secondarySortProperty, ListSortDirection.Ascending)
                ); 
                break;
            }
            case SortingOptions.AlphabeticalDescending: {
                collectionView.SortDescriptions.Add(
                    new SortDescription(primarySortProperty, ListSortDirection.Descending)
                ); 
                collectionView.SortDescriptions.Add(
                    new SortDescription(secondarySortProperty, ListSortDirection.Descending)
                ); 
                break;
            }
            default: {
                collectionView.SortDescriptions.Add(
                    new SortDescription(primarySortProperty, ListSortDirection.Ascending)
                ); 
                collectionView.SortDescriptions.Add(
                    new SortDescription(secondarySortProperty, ListSortDirection.Ascending)
                ); 
                break;
            }
        }
    }
    
    public static void ApplySortingWithOneProperty(ICollectionView collectionView, SortingOptions selectedOption, string primarySortProperty){
        collectionView.SortDescriptions.Clear();
        
        switch (selectedOption) {
            case SortingOptions.AlphabeticalAscending:
            case SortingOptions.IdAscending:
            case SortingOptions.DateAscending:
            case SortingOptions.PriceAscending: {
                collectionView.SortDescriptions.Add(
                    new SortDescription(primarySortProperty, ListSortDirection.Ascending)
                ); 
                break;
            }
            case SortingOptions.AlphabeticalDescending:
            case SortingOptions.IdDescending:
            case SortingOptions.DateDescending:
            case SortingOptions.PriceDescending: {
                collectionView.SortDescriptions.Add(
                    new SortDescription(primarySortProperty, ListSortDirection.Descending)
                ); 
                break;
            }
            default: {
                collectionView.SortDescriptions.Add(
                    new SortDescription(primarySortProperty, ListSortDirection.Ascending)
                ); 
                break;
            }
        }
    }
}