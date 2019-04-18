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
    public class InputViewModel : BaseViewModel
    {
        private ObservableCollection<Model.InputInfo> _List;
        public ObservableCollection<Model.InputInfo> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.Object> _Object;
        public ObservableCollection<Model.Object> Object { get => _Object; set { _Object = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.Input> _Input;
        public ObservableCollection<Model.Input> Input { get => _Input; set { _Input = value; OnPropertyChanged(); } }

        private Model.InputInfo _SelectedItem;
        public Model.InputInfo SelectedItem
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
                    Status = SelectedItem.Status;
                    InputPrice = SelectedItem.InputPrice;
                    OutputPrice = SelectedItem.OutputPrice;
                    SelectedInput = SelectedItem.Input.DateInput;
                }
            }
        }

        private DateTime? _SelectedInput;
        public DateTime? SelectedInput
        {
            get => _SelectedInput;
            set
            {
                _SelectedInput = value;
                OnPropertyChanged();
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

        private double? _InputPrice;
        public double? InputPrice { get => _InputPrice; set { _InputPrice = value; OnPropertyChanged(); } }

        private double? _OutputPrice;
        public double? OutputPrice { get => _OutputPrice; set { _OutputPrice = value; OnPropertyChanged(); } }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public InputViewModel()
        {
            List = new ObservableCollection<Model.InputInfo>(DataProvider.Ins.DB.InputInfoes.Where(x=> x.Isdelete != 1));
            Object = new ObservableCollection<Model.Object>(DataProvider.Ins.DB.Objects);
            Input = new ObservableCollection<Model.Input>(DataProvider.Ins.DB.Inputs);


            AddCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedObject == null || Count == null || InputPrice == null || OutputPrice == null)
                    return false;
                return true;

            }, (p) =>
            {
                var maxid = DataProvider.Ins.DB.Inputs.Max(u => u.Id);
                var tmp = Guid.NewGuid().ToString();
              //  Int32.TryParse(maxid, out tmp);
             //   tmp++;

                var Input = new Model.Input() { Id = tmp, DateInput = SelectedInput };

                DataProvider.Ins.DB.Inputs.Add(Input);
                DataProvider.Ins.DB.SaveChanges();

                var maxid2 = DataProvider.Ins.DB.InputInfoes.Max(u => u.Id);
                var tmp2 = Guid.NewGuid().ToString();
               // Int32.TryParse(maxid2, out tmp2);
              //  tmp2++;
                var InputInfo = new Model.InputInfo() { Id = tmp2, IdObject = SelectedObject.Id, IdInput = tmp, Count = Count, InputPrice = InputPrice, OutputPrice = OutputPrice, Status = Status };

                DataProvider.Ins.DB.InputInfoes.Add(InputInfo);
                DataProvider.Ins.DB.SaveChanges();

                List.Add(InputInfo);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedObject == null || Count == null || InputPrice == null || OutputPrice == null)
                    return false;
                var displayList = DataProvider.Ins.DB.InputInfoes.Where(x => x.Id == SelectedItem.Id);
                if (displayList != null && displayList.Count() != 0)
                    return true;
                return false;
            }, (p) =>
            {
                var outt = DataProvider.Ins.DB.InputInfoes.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();
                outt.Count = Count;
                outt.Status = Status;
                outt.InputPrice = InputPrice;
                outt.IdObject = SelectedObject.Id;
                outt.Input.DateInput= SelectedItem.Input.DateInput;
                outt.OutputPrice = OutputPrice;
                

                DataProvider.Ins.DB.SaveChanges();
                SelectedItem.Count = Count;
            });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedObject == null || Count == null || InputPrice == null || OutputPrice == null)
                    return false;
                return true;
            }, (p) =>
            {
                var outt = DataProvider.Ins.DB.InputInfoes.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();
                outt.Isdelete = 1;
                DataProvider.Ins.DB.SaveChanges();
                List.Remove(outt);

            });
        }
    }
}
