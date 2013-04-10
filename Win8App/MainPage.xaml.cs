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

namespace GetStartedWithData
{
    public class Feedback1
    {
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public string Twitter { get; set; }
        public string Message { get; set; }

    }

    public sealed partial class MainPage : Page
    {
     //   private ObservableCollection<TodoItem> items = new ObservableCollection<TodoItem>();

        //// MobileServiceCollectionView implements ICollectionView (useful for databinding to lists) and 
        //// is integrated with your Mobile Service to make it easy to bind your data to the ListView
        //// TODO: Uncomment the following two lines of code to replace the following collection with todoTable, 
        //// a proxy for the table in SQL Database.
        private MobileServiceCollectionView<Feedback1> items;
        private IMobileServiceTable<Feedback1> todoTable = App.MobileService.GetTable<Feedback1>();

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void InsertTodoItem(Feedback1 todoItem)
        {
            // TODO: Delete or comment the following statement; Mobile Services auto-generates the ID.
            //todoItem.Id = items.Count == 0 ? 0 : items.Max(i => i.Id) + 1;

            //// This code inserts a new TodoItem into the database. When the operation completes
            //// and Mobile Services has assigned an Id, the item is added to the CollectionView
            //// TODO: Mark this method as "async" and uncomment the following statement.
            
            await todoTable.InsertAsync(todoItem);
             
            items.Add(todoItem);
        }

        private void RefreshTodoItems()
        {
            // filter out the latest results only
            items = todoTable
               .Where(todoItem => todoItem.Id >0)
               .OrderByDescending(x=>x.Id)
               .Take(7)
               .ToCollectionView();

             ListItems.ItemsSource = items; 
        }

        private async void UpdateCheckedTodoItem(Feedback1 item)
        {
            //// This code takes a freshly completed TodoItem and updates the database. When the MobileService 
            //// responds, the item is removed from the list.
            //// TODO: Mark this method as "async" and uncomment the following statement
             await todoTable.UpdateAsync(item);     
        }

        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshTodoItems();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
         //   var todoItem = new TodoItem { Text = TextInput.Text, Channel = App.CurrentChannel.Uri };
         //   InsertTodoItem(todoItem);
        }

        private void CheckBoxComplete_Checked(object sender, RoutedEventArgs e)
        {
            //CheckBox cb = (CheckBox)sender;
            //TodoItem item = cb.DataContext as TodoItem;
            //UpdateCheckedTodoItem(item);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            RefreshTodoItems();
        }
    }
}
