using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class UserVM : INotifyPropertyChanged
    {
        public int Id { get; set; }
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    NotifyPropertyCahnged(nameof(Name));
                }
            }
        }
        public string Phone { get; set; }
        public string _dateCreated;
       

        public string DateCreated
        {
            get { return _dateCreated; }
            set
            {
                _dateCreated = value;
                NotifyPropertyCahnged(nameof(DateCreated));
            }
        }


        //public DateTime? DateUpdate { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyCahnged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }

}
