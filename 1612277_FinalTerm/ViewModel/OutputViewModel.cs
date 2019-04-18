using _1612277_FinalTerm.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace _1612277_FinalTerm.ViewModel
{
    public class OutputViewModel : BaseViewModel
    {
        private ObservableCollection<Model.OutputInfo> _List;
        public ObservableCollection<Model.OutputInfo> List { get => _List; set { _List = value; OnPropertyChanged(); } }
        
        private ObservableCollection<Model.Object> _Object;
        public ObservableCollection<Model.Object> Object { get => _Object; set { _Object = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.InputInfo> _InputInfo;
        public ObservableCollection<Model.InputInfo> InputInfo { get => _InputInfo; set { _InputInfo = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.Customer> _Customer;
        public ObservableCollection<Model.Customer> Customer { get => _Customer; set { _Customer = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.Output> _Output;
        public ObservableCollection<Model.Output> Output { get => _Output; set { _Output = value; OnPropertyChanged(); } }

        private Model.OutputInfo _SelectedItem;
        public Model.OutputInfo SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    Count = SelectedItem.Count;
                    SelectedObject = SelectedItem.Object;
                    SelectedOutput = SelectedItem.Output.DateOutput;
                    var ido = SelectedItem.Object.Id;
                    
                    var tmp = DataProvider.Ins.DB.InputInfoes.Where(p => p.IdObject == ido).FirstOrDefault();
                    if( tmp != null)
                        SelectedOutputPrice = tmp.OutputPrice;
                    Status = SelectedItem.Status;
                    SelectedCustomer = SelectedItem.Customer;
                    Discount = SelectedItem.Discount;
                }
            }
        }

        private Model.Object _SelectedObject;
        public Model.Object SelectedObject
        {
            get => _SelectedObject;
            set
            {
                _SelectedObject = value;
                OnPropertyChanged();
            }
        }

        private Model.InputInfo _SelectedInputInfo;
        public Model.InputInfo SelectedInputInfo
        {
            get => _SelectedInputInfo;
            set
            {
                _SelectedInputInfo = value;
                OnPropertyChanged();
            }
        }

        private Model.Customer _SelectedCustomer;
        public Model.Customer SelectedCustomer
        {
            get => _SelectedCustomer;
            set
            {
                _SelectedCustomer = value;
                OnPropertyChanged();
            }
        }

      

        private DateTime? _SelectedOutput;
        public DateTime? SelectedOutput { get => _SelectedOutput; set { _SelectedOutput = value; OnPropertyChanged(); } }

        private string _ObjectDisplayName;
        public string ObjectDisplayName { get => _ObjectDisplayName; set { _ObjectDisplayName = value; OnPropertyChanged(); } }

        private int? _Count;
        public int? Count
        {
            get => _Count;
            set
            {
                _Count = value;
                OnPropertyChanged();
            }
        }

        private double? _SelectedOutputPrice;
        public double? SelectedOutputPrice
        {
            get => _SelectedOutputPrice;
            set
            {
                _SelectedOutputPrice = value;
                OnPropertyChanged();
            }
        }

        private string _Status;
        public string Status
        {
            get => _Status;
            set
            {
                _Status = value;
                OnPropertyChanged();
            }
        }

        private string _Discount;
        public string Discount
        {
            get => _Discount;
            set
            {
                _Discount = value;
                OnPropertyChanged();
            }
        }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public OutputViewModel()
        {
            List = new ObservableCollection<Model.OutputInfo>(DataProvider.Ins.DB.OutputInfoes.Where(p=> p.Isdelete != 1));
            Object = new ObservableCollection<Model.Object>(DataProvider.Ins.DB.Objects);
            Customer = new ObservableCollection<Model.Customer>(DataProvider.Ins.DB.Customers);
            Output = new ObservableCollection<Model.Output>(DataProvider.Ins.DB.Outputs);

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedOutputPrice == null || SelectedOutput == null || SelectedCustomer == null)
                    return false;
                return true;

            }, (p) =>
            {
                var tmp = Guid.NewGuid().ToString();
              //  var maxid2 = DataProvider.Ins.DB.Outputs.OrderByDescending(u => u.Id).FirstOrDefault();
               // Int32.TryParse(maxid2.Id, out tmp);

                var out2 = new Model.Output() { Id = tmp, DateOutput = SelectedOutput };


                DataProvider.Ins.DB.Outputs.Add(out2);
                DataProvider.Ins.DB.SaveChanges();
                var  tmp2 = Guid.NewGuid().ToString();
                var maxid = DataProvider.Ins.DB.OutputInfoes.OrderByDescending(u => u.IdOutputInfo).FirstOrDefault();
               
                //Int32.TryParse(maxid.Id, out tmp2);
               // tmp2++;

                float total = (float)Count * (float)SelectedOutputPrice;
                if (Discount == "blackfriday")
                {
                    double tt = total * 0.7;
                    total = (float)tt;
                }
                else if (Discount == "cybermonday")
                {
                    double tt = total * 0.8;
                    total = (float)tt;
                }
                var Out = new Model.OutputInfo() { Id = tmp, IdObject = SelectedObject.Id, IdOutputInfo = tmp2, IdCustomer = SelectedCustomer.Id, Count = Count, Status = Status, ToTal= total, Discount = Discount };


                DataProvider.Ins.DB.OutputInfoes.Add(Out);
                DataProvider.Ins.DB.SaveChanges();

                
                List.Add(Out);
         
            });


            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedOutputPrice == null || SelectedOutput == null || SelectedCustomer == null || SelectedItem == null)
                    return false;

                var displayList = DataProvider.Ins.DB.OutputInfoes.Where(x => x.Id == SelectedItem.Id);
                if (displayList != null && displayList.Count() != 0)
                    return true;
                return false;

            }, (p) =>
            {
                var outt = DataProvider.Ins.DB.OutputInfoes.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();
                outt.Count = Count;
                outt.Status = Status;
                outt.Discount = Discount;
                outt.Customer.Id = SelectedCustomer.Id;
                outt.Object.Id = SelectedObject.Id;
                outt.Output.Id = SelectedItem.Output.Id;
                outt.ToTal = Count * SelectedOutputPrice;
                if (Discount == "blackfriday")
                {
                    outt.ToTal = Count * SelectedOutputPrice*0.7;
                }
                else if (Discount == "cybermonday")
                {
                    outt.ToTal = Count * SelectedOutputPrice * 0.8;
                }
                
                DataProvider.Ins.DB.SaveChanges();

                SelectedItem.Count = Count;
            });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedOutputPrice == null || SelectedOutput == null || SelectedCustomer == null || SelectedItem == null)
                    return false;

                return true;

            }, (p) =>
            {
                var outt = DataProvider.Ins.DB.OutputInfoes.Where(x => x.Id == SelectedItem.Id).FirstOrDefault();
                outt.Isdelete = 1;

                DataProvider.Ins.DB.SaveChanges();
                List.Remove(outt);
            });
        }

    }
}
