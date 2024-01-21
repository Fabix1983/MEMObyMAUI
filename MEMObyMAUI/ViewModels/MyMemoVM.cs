using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MEMObyMAUI.ViewModels
{
    public class MyMemoVM : INotifyPropertyChanged
    {

        public long Id { get; set; }
        public DateTime Data { get; set; }
        public string Descrizione { get; set; }

        private bool _eseguito;
        public bool Eseguito
        {
            get { return _eseguito; }
            set
            {
                _eseguito = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = "") =>
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
