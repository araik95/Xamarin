using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace App2
{
    public partial class StartPage : ContentPage
    {
        bool isLoading;

        public StartPage()
        {
            InitializeComponent();
            PlacesListViewModel lans = new PlacesListViewModel();

            ListView listView = new ListView
            {
                ItemsSource = lans.Places,

                HeightRequest = 700,

                ItemTemplate = new DataTemplate(() =>
                {
                    Image icon = new Image();
                    icon.SetBinding(Image.SourceProperty, "Icon");
                    
                    

                    Label nameLabel = new Label();
                    nameLabel.SetBinding(Label.TextProperty, "Name");
                    nameLabel.TextColor = Color.FromHex("#f35e20");

                    Label ratingLabel = new Label();
                    ratingLabel.SetBinding(Label.TextProperty, "Rating");

                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            HeightRequest = 400,
                            Padding = new Thickness(0, 5),
                            Orientation = StackOrientation.Horizontal,
                            Children = { icon,nameLabel, ratingLabel }
                        }
                    };
                })
            };

            listView.ItemAppearing += (sender, e) =>
            {
                if (isLoading || lans.Places.Count == 0)
                {
                    return;
                }

                if (e.Item == lans.Places[lans.Places.Count - 1])
                {
                    isLoading = true;
                    lans.LoadMore();
                    isLoading = false;
                }
            };

            SearchBar searcBar = new SearchBar();
            searcBar.Placeholder = "Search";

            searcBar.TextChanged += (object sender, TextChangedEventArgs e) =>
             {
                 
                 System.Diagnostics.Debug.WriteLine("SEARCH BAR TEXT ==> {0}",searcBar.Text);
                 lans.SearchByClient(searcBar.Text);
                // listView.ItemsSource = lans.PlacesSearhByClient;
             };

            this.Content = new StackLayout { Children = { searcBar,listView } };
        }

    }
}
