using _1612277_FinalTerm.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace _1612277_FinalTerm.ViewModel
{
   public class MainViewModel : BaseViewModel
    {
        private ObservableCollection<TonKho> _TonKhoList;
        public ObservableCollection<TonKho> TonKhoList { get => _TonKhoList; set { _TonKhoList = value; OnPropertyChanged(); } }
        public bool Isloaded = false;
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand UnitCommand { get; set; }
        public ICommand SuplierCommand { get; set; }
        public ICommand CustomerCommand { get; set; }
        public ICommand ObjectCommand { get; set; }
        public ICommand UserCommand { get; set; }
        public ICommand InputCommand { get; set; }
        public ICommand OutputCommand { get; set; }

        // mọi thứ xử lý sẽ nằm trong này
        public MainViewModel()
        {
            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                Isloaded = true;
                if (p == null)
                    return;
                p.Hide();
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();

                if (loginWindow.DataContext == null)
                    return;
                var loginVM = loginWindow.DataContext as LoginViewModel;

                if (loginVM.IsLogin)
                {
                    p.Show();
                    LoadTonKhoData();
                }
                else
                {
                    p.Close();
                }
            }
              );
            UnitCommand = new RelayCommand<object>((p) => { return true; }, (p) => { UnitWindow wd = new UnitWindow(); wd.ShowDialog(); });
            SuplierCommand = new RelayCommand<object>((p) => { return true; }, (p) => { SuplierWindow wd = new SuplierWindow(); wd.ShowDialog(); });
            CustomerCommand = new RelayCommand<object>((p) => { return true; }, (p) => { CustomerWindow wd = new CustomerWindow(); wd.ShowDialog(); });
            ObjectCommand = new RelayCommand<object>((p) => { return true; }, (p) => { ObjectWindow wd = new ObjectWindow(); wd.ShowDialog(); });
            UserCommand = new RelayCommand<object>((p) => { return true; }, (p) => { UserWindow wd = new UserWindow(); wd.ShowDialog(); });
            InputCommand = new RelayCommand<object>((p) => { return true; }, (p) => { InputWindow wd = new InputWindow(); wd.ShowDialog(); });
            OutputCommand = new RelayCommand<object>((p) => { return true; }, (p) => { OutputWindow wd = new OutputWindow(); wd.ShowDialog(); });
        }

        private int _sumInput;
        public int sumInput { get => _sumInput; set { _sumInput = value; OnPropertyChanged(); } }

        private int _sumOutput;
        public int sumOutput { get => _sumOutput; set { _sumOutput = value; OnPropertyChanged(); } }

        private int _ton;
        public int ton { get => _ton; set { _ton = value; OnPropertyChanged(); } }
        

        void LoadTonKhoData()
        {
            int sum1 = 0;
            int sum2 = 0;
            TonKhoList = new ObservableCollection<TonKho>();
            var objectlist = DataProvider.Ins.DB.Objects;
            int i = 1;
            foreach (var item in objectlist)
            {
                var inputList = DataProvider.Ins.DB.InputInfoes.Where(p => p.IdObject == item.Id);
                var outputList = DataProvider.Ins.DB.OutputInfoes.Where(p => p.IdObject == item.Id);
               
                if (inputList.Sum(p => p.Count) != null)
                {
                    sum1 = (int)inputList.Sum(p => p.Count);
                    sumInput = sumInput + sum1;
                }
                if (outputList.Sum(p => p.Count) != null)
                {
                    sum2 = (int)outputList.Sum(p => p.Count);
                    sumOutput = sumOutput + sum2;
                }
                TonKho tonkho = new TonKho();
                tonkho.Stt = i;
                tonkho.Count = sum1 - sum2;
                tonkho.Object = item;
                TonKhoList.Add(tonkho);
                i++;
            }
            ton = sumInput - sumOutput;
        }
    }
}
