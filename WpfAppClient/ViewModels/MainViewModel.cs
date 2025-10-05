using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfAppClient.Helpers;
using WpfAppClient.Models;

namespace WpfAppClient.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {

        private HubConnection _connection;
        public ObservableCollection<Population> Populations { get; set; } = new ObservableCollection<Population>();

        private string _country;
        public string Country
        {
            get => _country;
            set { _country = value; OnPropertyChanged(); }
        }

        private int _quantity;
        public int Quantity
        {
            get => _quantity;
            set { _quantity = value; OnPropertyChanged(); }
        }

        private int _id;
        public int Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(); }
        }
        private Population _selectedPopulation;
        public Population SelectedPopulation
        {
            get => _selectedPopulation;
            set
            {
                _selectedPopulation = value;
                if (value != null)
                {
                    Id = value.Id;
                    Country = value.Country;
                    Quantity = (int)value.Quantity;
                }
                OnPropertyChanged();
            }
        }

        public ICommand AddCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }

        public MainViewModel()
        {
            AddCommand = new RelayCommand(_ => AddPopulation());
            UpdateCommand = new RelayCommand(_ => UpdatePopulation());
            DeleteCommand = new RelayCommand(_ => DeletePopulation());

            _connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7100/population")
                .WithAutomaticReconnect()
                .Build();

            _connection.On<List<Population>>("Receive", (data) =>
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    Populations.Clear();
                    foreach (var item in data)
                        Populations.Add(item);
                });
            });

            Connect();
        }

        private async void Connect()
        {
            await _connection.StartAsync();
        }

        private async void AddPopulation()
        {
            await _connection.InvokeAsync("AddPopulation", new Population { Country = Country, Quantity = Quantity });
            Country = string.Empty;
            Quantity = 0;
        }

        private async void UpdatePopulation()
        {
            await _connection.InvokeAsync("UpdatePopulation", new Population { Id = Id, Country = Country, Quantity = Quantity });
        }

        private async void DeletePopulation()
        {
            await _connection.InvokeAsync("DeletePopulation", Id);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    }
}
