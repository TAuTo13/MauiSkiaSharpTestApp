using System.Linq;
using Microsoft.Maui.Controls;

namespace MauiApp2.Behavior
{
    internal class NumericValidationBehavior :Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        private static void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {

            if (sender is Entry entry) {
                if (!string.IsNullOrWhiteSpace(args.NewTextValue))
                {
                    bool isValid = args.NewTextValue.ToCharArray().All(x => char.IsDigit(x));
                    ((Entry)sender).Text = isValid ? args.NewTextValue : args.NewTextValue.Remove(args.NewTextValue.Length - 1);
                } else if (args.NewTextValue != null && args.NewTextValue.Equals(string.Empty)){
                    entry.Text = null;
                }
            }
        }
    }
}
