using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using Windows.UI.Popups;
using Microsoft.WindowsAzure.MobileServices;
using System.ComponentModel.DataAnnotations;
using AidansWindowsStoreApp.Model;

namespace AidansWindowsStoreApp
{


    public sealed partial class MainPage : Page
    {
     //   private ObservableCollection<TodoItem> items = new ObservableCollection<TodoItem>();

        //// MobileServiceCollectionView implements ICollectionView (useful for databinding to lists) and 
        //// is integrated with your Mobile Service to make it easy to bind your data to the ListView
        //// TODO: Uncomment the following two lines of code to replace the following collection with todoTable, 
        //// a proxy for the table in SQL Database.
        private MobileServiceCollectionView<Entry> items;
        private IMobileServiceTable<Entry> mobileServicesData = App.MobileService.GetTable<Entry>();

        public MainPage()
        {
            this.InitializeComponent();
        }


        private void RefreshTodoItems()
        {
            // filter out the latest results only
            items = mobileServicesData
               .Where(todoItem => todoItem.Id >0)
               .OrderByDescending(x=>x.Id)
            //  .Take(7)
               .ToCollectionView();

             ListItems.ItemsSource = items;
           
        }


        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshTodoItems();
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            RefreshTodoItems();
        }
    }
}
