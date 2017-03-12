using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace App2
{
    class PlacesListViewModel
    {
        public ObservableCollection<PlaceViewModel> Places { get; set; }
        public ObservableCollection<PlaceViewModel> PlacesSearhByClient { get; set; }
        public ICommand MoveToTopCommand { protected set; get; }
        public ICommand MoveToBottomCommand { protected set; get; }
        public ICommand RemoveCommand { protected set; get; }
        public string nextPageToken { get; set; }

        
        public PlacesListViewModel()
        {
            Places = new ObservableCollection<PlaceViewModel>();
            LoadData();
            
            foreach (var item in Places)
            {
                System.Diagnostics.Debug.WriteLine(item.Name);
                System.Diagnostics.Debug.WriteLine(item.Rating);
                System.Diagnostics.Debug.WriteLine(item.Icon);
                System.Diagnostics.Debug.WriteLine("\n");
            }

            MoveToTopCommand = new Command(MoveToTop);
            MoveToBottomCommand = new Command(MoveToBottom);
            RemoveCommand = new Command(Remove);
           
        }




        private async void LoadData()
        {
            string url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=-33.8670522,151.1957362&rankby=distance&types=food&key=AIzaSyCK8SquzPry-orS9Lc80hXbPyVUhIyni_Y";
            try
            {
                HttpClient client = new HttpClient();
                var response = await client.GetStringAsync(string.Format(url));

                var result = JsonConvert.DeserializeObject<PlacesApiQueryResponse>(response.ToString());
                foreach (var item in result.results)
                {
                    PlaceViewModel place = new PlaceViewModel()
                    {
                        Name = item.name,
                        Rating = item.rating,
                        Icon = item.icon,
                  
                    };
                    Places.Add(place);
                    System.Diagnostics.Debug.WriteLine(place.Name);
                }

                var result1 = JsonConvert.DeserializeObject<PlacesApiQueryResponse>(response.ToString());
                nextPageToken =  result1.next_page_token;
                System.Diagnostics.Debug.WriteLine(nextPageToken);

            }
            catch (Exception ex)
            { }
        }

        public async void LoadMore()
        {
            
           
            string url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?pagetoken=" + nextPageToken +"&key=AIzaSyCK8SquzPry-orS9Lc80hXbPyVUhIyni_Y";
            try
            {
                HttpClient client = new HttpClient();
                var response = await client.GetStringAsync(string.Format(url));

                var result = JsonConvert.DeserializeObject<PlacesApiQueryResponse>(response.ToString());
                foreach (var item in result.results)
                {
                    PlaceViewModel place = new PlaceViewModel()
                    {
                        Name = item.name,
                        Rating = item.rating,
                        Icon = item.icon
                    };

                    Places.Add(place);
                    
                    System.Diagnostics.Debug.WriteLine(place.Name);
                }


            }
            catch (Exception ex)
            { }
        }

        public async void SearchByClient(string query)
        {
            Places.Clear();
            string url = "https://maps.googleapis.com/maps/api/place/textsearch/json?query=";
            Char delimiter = ' ';
            var substrings = query.Split(delimiter);
            
            for (int i = 0; i < substrings.Length; i++)
            {           
                if (i != substrings.Length)
                {
                    System.Diagnostics.Debug.WriteLine(substrings[i]);
                    url += substrings[i];
                }      
            }

            url += "&key=AIzaSyCK8SquzPry-orS9Lc80hXbPyVUhIyni_Y";
            System.Diagnostics.Debug.WriteLine(url);
            /* restaurants+in+Sydney";*/
            try
            {
                HttpClient client = new HttpClient();
                var response = await client.GetStringAsync(string.Format(url));
                System.Diagnostics.Debug.WriteLine(response);

                var result = JsonConvert.DeserializeObject<PlacesApiQueryResponse>(response.ToString());
                foreach (var item in result.results)
                {
                    PlaceViewModel place = new PlaceViewModel()
                    {
                        Name = item.name,
                        Rating = item.rating,
                        Icon = item.icon
                    };
                  
                    Places.Add(place);

                    System.Diagnostics.Debug.WriteLine(place.Name);
                }
            }
            catch (Exception ex)
            { }
        }

        
        private void MoveToTop(object phoneObj)
        {
            PlaceViewModel phone = phoneObj as PlaceViewModel;
            if (phone == null) return;
            int oldIndex = Places.IndexOf(phone);
            if (oldIndex > 0)
                Places.Move(oldIndex, oldIndex - 1);
        }
        private void MoveToBottom(object phoneObj)
        {
            PlaceViewModel phone = phoneObj as PlaceViewModel;
            if (phone == null) return;
            int oldIndex = Places.IndexOf(phone);
            if (oldIndex < Places.Count - 1)
                Places.Move(oldIndex, oldIndex + 1);
        }
        private void Remove(object phoneObj)
        {
            PlaceViewModel phone = phoneObj as PlaceViewModel;
            if (phone == null) return;

            Places.Remove(phone);
        }
    }
}
