using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AlexPacientes.Controls
{
    public class LazyListView : ListView
    {

        //Example code implementation
        //var comando = new Command(()=> {
        //    //load more items code
        //});
        //LazyListView lista=new LazyListView();
        //lista.LoadMoreCommand = comando;
        public static readonly BindableProperty LoadMoreCommandProperty = BindableProperty.Create(
            nameof(LoadMoreCommand), 
            typeof(ICommand), 
            typeof(LazyListView), 
            default(ICommand),
            BindingMode.OneWay);
        public ICommand LoadMoreCommand
        {
            get { return (ICommand)GetValue(LoadMoreCommandProperty); }
            set { SetValue(LoadMoreCommandProperty, value); }
        }

        public LazyListView()
        {
            ItemAppearing += LazyListView_ItemAppearing;
        }

        public LazyListView(ListViewCachingStrategy cachingStrategy) : base(cachingStrategy)
        {
            ItemAppearing += LazyListView_ItemAppearing;
        }

        void LazyListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            var items = ItemsSource as IList;
            if (items != null && e.Item == items[items.Count - 1])
            {
                if (LoadMoreCommand != null && LoadMoreCommand.CanExecute(null))
                    LoadMoreCommand.Execute(null);
            }
        }
    }
}
