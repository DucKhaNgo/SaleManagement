﻿using _1612277_FinalTerm.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace _1612277_FinalTerm.ViewModel
{
    public class CustomerViewModel : BaseViewModel
    {
        private ObservableCollection<Customer> _List;
        public ObservableCollection<Customer> List { get => _List; set { _List = value; OnPropertyChanged(); } }
        private Customer _SelectedItem;
        public Customer SelectedItem
        {
            get => _SelectedItem; set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    DisplayName = SelectedItem.DisplayName;
                    Phone = SelectedItem.Phone;
                    Email = SelectedItem.Email;
                    Address = SelectedItem.Address;
                    MoreInfo = SelectedItem.MoreInfo;
                    ContractDate = SelectedItem.ContractDate;
                }
            }
        }
        private string _DisplayName;
        public string DisplayName { get => _DisplayName; set { _DisplayName = value; OnPropertyChanged(); } }

        private string _Phone;
        public string Phone { get => _Phone; set { _Phone = value; OnPropertyChanged(); } }


        private string _Address;
        public string Address { get => _Address; set { _Address = value; OnPropertyChanged(); } }


        private string _Email;
        public string Email { get => _Email; set { _Email = value; OnPropertyChanged(); } }


        private string _MoreInfo;
        public string MoreInfo { get => _MoreInfo; set { _MoreInfo = value; OnPropertyChanged(); } }


        private DateTime? _ContractDate;
        public DateTime? ContractDate { get => _ContractDate; set { _ContractDate = value; OnPropertyChanged(); } }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }

        public CustomerViewModel()
        {
            List = new ObservableCollection<Customer>(DataProvider.Ins.DB.Customers);
            AddCommand = new RelayCommand<object>((p) =>
            {
                return true;

            }, (p) =>
            {
                var Customer = new Customer() { DisplayName = DisplayName, Phone = Phone, Address = Address, Email = Email, ContractDate = ContractDate, MoreInfo = MoreInfo };

                DataProvider.Ins.DB.Customers.Add(Customer);
                DataProvider.Ins.DB.SaveChanges();

                List.Add(Customer);
            });


            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;

                var displayList = DataProvider.Ins.DB.Customers.Where(x => x.Id == SelectedItem.Id);
                if (displayList != null && displayList.Count() != 0)
                    return true;

                return false;

            }, (p) =>
            {
                var Customer = DataProvider.Ins.DB.Customers.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();
                Customer.DisplayName = DisplayName;
                Customer.Phone = Phone;
                Customer.Address = Address;
                Customer.Email = Email;
                Customer.ContractDate = ContractDate;
                Customer.MoreInfo = MoreInfo;
                DataProvider.Ins.DB.SaveChanges();

                SelectedItem.DisplayName = DisplayName;
            });
        }


    }
}
