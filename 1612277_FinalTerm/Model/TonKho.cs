using _1612277_FinalTerm.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1612277_FinalTerm.Model
{
   public class TonKho : BaseViewModel
    {
        private Object _Object;
       public Object Object { get=> _Object; set { _Object = value; OnPropertyChanged(); } }

        private int _Stt;
        public int Stt { get=> _Stt; set { _Stt = value; OnPropertyChanged(); } }

        private int _Count;
        public int Count { get=> _Count; set { _Count = value; OnPropertyChanged(); } }
    }
}
