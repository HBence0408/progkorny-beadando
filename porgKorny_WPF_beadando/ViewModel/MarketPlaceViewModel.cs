using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using porgKorny_WPF_beadando.Model;
using porgKorny_WPF_beadando.Services;

public class MarketplaceViewModel : INotifyPropertyChanged
{
    public ObservableCollection<Ad> Ads { get; set; } = new ObservableCollection<Ad>();
    public ObservableCollection<Ad> SearchResults { get; set; } = new ObservableCollection<Ad>();


    public string SearchQuery { get; set; }
    public ICommand PurchaseAdCommand => new RelayCommand<Ad>(PurchaseAd);

    private User CurrentUser;
    public void SetUser(string u)
    {
        CurrentUser = new User();
        CurrentUser.UserName = u;
    }

    private int _balance => CurrentUser.Balance;
    public int Balance
    {
        get => _balance;
        set
        {
            if (_balance != value)
            {
                CurrentUser.Balance = value;
                OnPropertyChanged();
            }
        }
    }

    private string _title;
    public string Title
    {
        get => _title;
        set { _title = value; OnPropertyChanged(); }
    }

    private string _description;
    public string Description
    {
        get => _description;
        set { _description = value; OnPropertyChanged(); }
    }

    private string _price;
    public string Price
    {
        get => _price;
        set { _price = value; OnPropertyChanged(); }
    }

    //private string _searchQuery;

    //public string SearchQuery
    //{
    //    get => _searchQuery;
    //    set { _searchQuery = value; OnPropertyChanged(); }
    //}

    public ICommand AddAdCommand { get; }
    public ICommand SearchCommand { get; }

    public MarketplaceViewModel(string u)
    {
        SetUser(u);
        CurrentUser = UserDatabase.Instance.LoadUserData(u);
        //AddAdCommand = new RelayCommand(AddAd);
        //SearchCommand = new RelayCommand(SearchAds);
        LoadOwnAds();
    }

    //private void AddAd()
    //{
    //    if (decimal.TryParse(Price, out var parsedPrice))
    //    {
    //        Ads.Add(new Ad
    //        {
    //            Title = Title,
    //            Description = Description,
    //            Price = parsedPrice,
    //            CreatedAt = DateTime.Now
    //        });

    //        // Clear input fields
    //        Title = string.Empty;
    //        Description = string.Empty;
    //        Price = string.Empty;
    //    }
    //}

    //private void SearchAds()
    //{
    //    SearchResults.Clear();

    //    foreach (var ad in Ads)
    //    {
    //        if (!string.IsNullOrWhiteSpace(SearchQuery) && ad.Title.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase))
    //        {
    //            SearchResults.Add(ad);
    //        }
    //    }
    //}

    public void PurchaseAd(Ad ad)
    {
       // MessageBox.Show($"Buy clicked for: {ad.Id}");
        if (CurrentUser.Balance < ad.Price) return;

        if (AdDatabase.Instance.PurchaseAd(ad.Id, ad.Price, CurrentUser.UserName))
        {
            Balance -= ad.Price;
            SearchResults.Remove(ad);
            OnPropertyChanged(nameof(CurrentUser));
        }
    }

    public void LoadOwnAds()
    {
        Ads.Clear();
        var ownAds = AdDatabase.Instance.GetOwnAds(CurrentUser.UserName);
        foreach (var ad in ownAds)
        {
            Ads.Add(ad);
        }
    }

    public void SearchOtherAds()
    {
        SearchResults.Clear();
        var otherAds = AdDatabase.Instance.SearchOtherAds(CurrentUser.UserName, SearchQuery);
        foreach (var ad in otherAds)
        {
            SearchResults.Add(ad);
        }
    }

    public void AddAd(string title, string desc, decimal price)
    {
        AdDatabase.Instance.InsertAd(title, desc, price, CurrentUser.UserName);
        LoadOwnAds();
    }





    public event PropertyChangedEventHandler PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
