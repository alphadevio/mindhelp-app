using AlexPacientes.Controls;
using AlexPacientes.Styles;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AlexPacientes.Views.Appointments.Cells
{
    public class CreditCardCell : Frame
    {
        public bool IsSelected { get { return (bool)GetValue(IsSelectedProperty); } set { SetValue(IsSelectedProperty, value); } }
        public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create(
            nameof(IsSelected), typeof(bool), typeof(CreditCardCell), false,
            propertyChanged: (s, o, n) =>
            {
                var sender = s as CreditCardCell;
                if (sender == null) return;
                sender.BorderColor = (bool)n ? Colors.PrimaryColor : Colors.Divider;
            });

        public CreditCardCell()
        {
            LabelDarkHint AccountHolderNameTitle = new LabelDarkHint()
            {
                TextColor = Colors.PrimaryColor,
                Text = Labels.Labels.Name
            };
            LabelDarkPrimary AccountHolderName = new LabelDarkPrimary()
            {
                HorizontalOptions = LayoutOptions.StartAndExpand
            };

            LabelDarkHint CardBrandTypeTitle = new LabelDarkHint()
            {
                TextColor = Colors.PrimaryColor,
                Text = Labels.Labels.CreditCardBrand,
                Margin = new Thickness(0, Layouts.Margin, 0, 0)
            };
            LabelDarkPrimary CardBrandType = new LabelDarkPrimary()
            {
                HorizontalOptions = LayoutOptions.StartAndExpand
            };

            LabelDarkHint EnumerationTitle = new LabelDarkHint()
            {
                TextColor = Colors.PrimaryColor,
                Text = Labels.Labels.CreditCardBrand,
                Margin = new Thickness(0, Layouts.Margin, 0, 0)
            };
            LabelDarkPrimary Enumeration = new LabelDarkPrimary()
            {
                HorizontalOptions = LayoutOptions.StartAndExpand
            };

            MinimumWidthRequest = 250;
            HasShadow = false;
            CornerRadius = Layouts.CornerRadius;
            BorderColor = IsSelected ? Colors.PrimaryColor : Colors.Divider;
            IsClippedToBounds = true;
            Padding = new Thickness(Layouts.Margin, Layouts.Margin / 2);
            Content = new StackLayout()
            {
                Spacing = 0,
                Padding=0,
                VerticalOptions=LayoutOptions.CenterAndExpand,
                Children =
                {
                    AccountHolderNameTitle,
                    AccountHolderName,
                    CardBrandTypeTitle,
                    CardBrandType,
                    EnumerationTitle,
                    Enumeration
                }
            };

            AccountHolderName.SetBinding(LabelDarkPrimary.TextProperty, "Name");
            CardBrandType.SetBinding(LabelDarkPrimary.TextProperty, "Brand");
            Enumeration.SetBinding(LabelDarkPrimary.TextProperty, "Last4", stringFormat: "**** **** **** {0}");
            this.SetBinding(IsSelectedProperty, "IsSelected");
        }
    }
}
