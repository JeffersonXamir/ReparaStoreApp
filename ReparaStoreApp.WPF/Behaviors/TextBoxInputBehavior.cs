using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ReparaStoreApp.WPF.Behaviors
{
    public static class TextBoxInputBehavior
    {
        private static readonly Regex _onlyNumbersRegex = new Regex("^[0-9]+$");

        public static readonly DependencyProperty OnlyNumbersProperty =
            DependencyProperty.RegisterAttached(
                "OnlyNumbers",
                typeof(bool),
                typeof(TextBoxInputBehavior),
                new UIPropertyMetadata(false, OnOnlyNumbersChanged));

        public static bool GetOnlyNumbers(DependencyObject obj) => (bool)obj.GetValue(OnlyNumbersProperty);
        public static void SetOnlyNumbers(DependencyObject obj, bool value) => obj.SetValue(OnlyNumbersProperty, value);

        private static void OnOnlyNumbersChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBox textBox)
            {
                if ((bool)e.NewValue)
                {
                    textBox.PreviewTextInput += PreviewTextInputHandler;
                    DataObject.AddPastingHandler(textBox, OnPaste);
                    textBox.PreviewKeyDown += OnPreviewKeyDown;
                }
                else
                {
                    textBox.PreviewTextInput -= PreviewTextInputHandler;
                    DataObject.RemovePastingHandler(textBox, OnPaste);
                    textBox.PreviewKeyDown -= OnPreviewKeyDown;
                }
            }
        }

        private static void PreviewTextInputHandler(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !_onlyNumbersRegex.IsMatch(e.Text);
        }

        private static void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Bloquea espacio
            if (e.Key == Key.Space)
                e.Handled = true;
        }

        private static void OnPaste(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(DataFormats.Text))
            {
                var text = e.DataObject.GetData(DataFormats.Text) as string;
                if (!_onlyNumbersRegex.IsMatch(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }
    }
}
