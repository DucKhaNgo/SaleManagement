//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace _1612277_FinalTerm.Model
{
    using _1612277_FinalTerm.ViewModel;
    using System;
    using System.Collections.Generic;

    public partial class Output : BaseViewModel
    {

        private string _Id;
        public string Id { get => _Id; set { _Id = value; OnPropertyChanged(); } }

        private Nullable<System.DateTime> _DateOutput;
        public Nullable<System.DateTime> DateOutput { get => _DateOutput; set { _DateOutput = value; OnPropertyChanged(); } }

        private OutputInfo _OutputInfo;
        public virtual OutputInfo OutputInfo { get => _OutputInfo; set { _OutputInfo = value; OnPropertyChanged(); } }
    }
}
