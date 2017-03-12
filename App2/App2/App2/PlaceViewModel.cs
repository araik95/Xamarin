using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Xamarin.Forms;

namespace App2
{
    class PlaceViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Result Place { get; set; }
        public PlacesListViewModel ListViewModel { get; set; }

        public PlaceViewModel()
        {
            Place = new Result();
        }

        public string Name
        {
            get { return Place.name; }
            set
            {
                if (Place.name != value)
                {
                    Place.name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        public double Rating
        {
            get { return Place.rating; }
            set
            {
                if (Place.rating != value)
                {
                    Place.rating = value;
                    OnPropertyChanged("Rating");
                }
            }
        }

        public string Icon
        {
            get { return Place.icon; }
            set
            {
                if (Place.icon != value)
                {
                    Place.icon = value;
                    OnPropertyChanged("Icon");
                }
            }
        }

     
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

    }
}

