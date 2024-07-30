using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AlexPacientes.Controls
{

    /// <summary>
    /// This class does not implement the cached elements (all the items are on memory)
    /// </summary>
    public class CustomListView : Grid
    {
        private ICommand _innerSelectedCommand;
        private readonly ScrollView _scrollView;
        private readonly StackLayout _itemsStackLayout;

        public event EventHandler SelectedItemChanged;
        public StackOrientation ListOrientation { get; set; }
        public double Spacing { get; set; }
        

        public ICommand SelectedCommand{ get { return (ICommand)GetValue(SelectedCommandProperty); } set { SetValue(SelectedCommandProperty, value); }}
        public static readonly BindableProperty SelectedCommandProperty = BindableProperty.Create(
            nameof(SelectedCommand), 
            typeof(ICommand), 
            typeof(CustomListView),
            null);


        public IEnumerable ItemsSource {get { return (IEnumerable)GetValue(ItemsSourceProperty); } set { SetValue(ItemsSourceProperty, value); }}
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
            nameof(ItemsSource), 
            typeof(IEnumerable), 
            typeof(CustomListView), 
            default(IEnumerable<object>), 
            BindingMode.TwoWay, //we should be able to get the items
            propertyChanged: (s,o,n)=>
            {
                if(s!=null)
                    ((CustomListView)s).SetItems();
            });


        public object SelectedItem { get { return (object)GetValue(SelectedItemProperty); } set { SetValue(SelectedItemProperty, value); }}
        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(
            nameof(SelectedItem), 
            typeof(object), 
            typeof(CustomListView), 
            null, 
            BindingMode.TwoWay, 
            propertyChanged: (s,o,n)=>
            {
                var itemsView = (CustomListView)s;
                if (n == o || n == null || itemsView.SelectedItem == null) return;
                itemsView.SelectedItemChanged?.Invoke(itemsView, new EventArgs() );
                if (itemsView.SelectedCommand?.CanExecute(n) ?? false) itemsView.SelectedCommand?.Execute(n);
            });

        /// <summary>
        /// Must be a View, not a ViewCell!
        /// (if you get a nullptr setting the source/template is because your data template is not a view)
        /// </summary>
        public DataTemplate ItemTemplate { get { return (DataTemplate)GetValue(ItemTemplateProperty); } set { SetValue(ItemTemplateProperty, value); }}
        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(
            nameof(ItemTemplate),
            typeof(DataTemplate), 
            typeof(CustomListView), 
            default(DataTemplate));

        

        public CustomListView()
        {
            _scrollView = new ScrollView();

            _itemsStackLayout = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = BackgroundColor,
                Padding = Padding,
                Spacing = Spacing,
            };

            _scrollView.BackgroundColor = BackgroundColor;
            _scrollView.Content = _itemsStackLayout;
            Children.Add(_scrollView);
        }

        protected virtual void SetItems()
        {
            _itemsStackLayout.Children.Clear();
            _itemsStackLayout.Spacing = Spacing;

            _innerSelectedCommand = new Command<View>(view =>
            {
                SelectedItem = view.BindingContext;
            });

            _itemsStackLayout.Orientation = ListOrientation;
            _scrollView.Orientation = ListOrientation == StackOrientation.Horizontal
                ? ScrollOrientation.Horizontal
                : ScrollOrientation.Vertical;

            if (ItemsSource == null)
                return;

            foreach (var item in ItemsSource)
            {
                _itemsStackLayout.Children.Add(GetItemView(item));
            }

            _itemsStackLayout.BackgroundColor = BackgroundColor;
            SelectedItem = null;
        }

        protected virtual View GetItemView(object item)
        {
            var content = ItemTemplate.CreateContent();
            var view = content as View;

            if (view == null)
                return null;

            view.BindingContext = item;

            var gesture = new TapGestureRecognizer
            {
                Command = _innerSelectedCommand,
                CommandParameter = view
            };

            AddGesture(view, gesture);

            return view;
        }

        private void AddGesture(View view, TapGestureRecognizer gesture)
        {
            view.GestureRecognizers.Add(gesture);
            var layout = view as Layout<View>;

            if (layout == null)
                return;

            foreach (var child in layout.Children)
                AddGesture(child, gesture);
        }
    }
}
