using AlexPacientes.Controls;
using AlexPacientes.Styles;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AlexPacientes.Views.Settings.Payments.Cell
{
    public class PaymentCell:ViewCell
    {
        public PaymentCell()
        {
            LabelDarkPrimary defaultLabel = new LabelDarkPrimary()
            {
                HorizontalOptions = LayoutOptions.End,
                TextColor = Colors.PrimaryColor,
                Text = Labels.Labels.Default,
                Margin = new Thickness(0, 0, 0, 0)
            };
            LabelDarkHint AccountHolderNameTitle = new LabelDarkHint()
            {
                TextColor = Colors.PrimaryColor,
                Text = Labels.Labels.Name,
                Margin = new Thickness(0, Layouts.Margin, 0, 0)
            };
            LabelDarkPrimary AccountHolderName = new LabelDarkPrimary()
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Margin=new Thickness(0,0,Layouts.Margin,0)
            };

            LabelDarkHint CardBrandTypeTitle = new LabelDarkHint()
            {
                TextColor = Colors.PrimaryColor,
                Text = Labels.Labels.CreditCardBrand,
                Margin = new Thickness(0, Layouts.Margin, 0, 0)
            };
            LabelDarkPrimary CardBrandType = new LabelDarkPrimary()
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Margin = new Thickness(0, 0, Layouts.Margin, 0)
            };

            LabelDarkHint EnumerationTitle = new LabelDarkHint()
            {
                TextColor = Colors.PrimaryColor,
                Text = Labels.Labels.CreditCardBrand,
                Margin = new Thickness(0, Layouts.Margin, 0, 0)
            };
            LabelDarkPrimary Enumeration = new LabelDarkPrimary()
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Margin = new Thickness(0, 0, Layouts.Margin*2, 0)
            };

            LabelDarkPrimary CloseButton = new LabelDarkPrimary()
            {
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.End,
                FontAttributes=FontAttributes.Bold,
                Text = "+",
                FontSize = 32,
                TextColor = Colors.PrimaryColor,
                Rotation = 45
            };
            var tgrDelete = new TapGestureRecognizer();
            CloseButton.GestureRecognizers.Add(tgrDelete);

            View = new Frame()
            {
                HasShadow = false,
                CornerRadius = Layouts.CornerRadius,
                BorderColor= Colors.PrimaryColor,
                IsClippedToBounds = true,
                Margin=Layouts.Margin,
                Padding=new Thickness(Layouts.Margin, Layouts.Margin/2),                
                Content = new Grid()
                {
                    HorizontalOptions=LayoutOptions.Fill,
                    VerticalOptions=LayoutOptions.Center,
                    Children =
                    {
                        new StackLayout()
                        {
                            Spacing = 0,
                            Children =
                            {
                                defaultLabel,
                                AccountHolderNameTitle,
                                AccountHolderName,
                                CardBrandTypeTitle,
                                CardBrandType,
                                EnumerationTitle,
                                Enumeration
                            }
                        },
                        CloseButton
                    }
                }
            };

            defaultLabel.SetBinding(Label.IsVisibleProperty, "Default");
            AccountHolderName.SetBinding(LabelDarkPrimary.TextProperty, "Name");
            CardBrandType.SetBinding(LabelDarkPrimary.TextProperty, "Brand");
            Enumeration.SetBinding(LabelDarkPrimary.TextProperty, "Last4", stringFormat: "**** **** **** {0}");
            tgrDelete.SetBinding(TapGestureRecognizer.CommandProperty, "DeleteCardCommand");
            tgrDelete.SetBinding(TapGestureRecognizer.CommandParameterProperty, ".");
        }
    }
}
