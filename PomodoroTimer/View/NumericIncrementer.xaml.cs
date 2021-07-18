using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace PomodoroTimer.View
{
    /// <summary>
    /// Logika interakcji dla klasy NumericIncrementer.xaml
    /// </summary>
    public partial class NumericIncrementer : UserControl
    {
        public NumericIncrementer()
        {
            InitializeComponent();
        }

        [Description("Max range of text in control"), Category("Data")]
        public int MaxRange { get; set; }

        [Description("Min range of text in control"), Category("Data")]
        public int MinRange { get; set; }


        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                "Text",
                typeof(string),
                typeof(NumericIncrementer),
                new FrameworkPropertyMetadata(null)
            );

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set
            {
                if (IsInRange(value))
                {
                    SetValue(TextProperty, value);
                }
            }
        }

        private static readonly Regex _regex = new Regex("^[0-9]{1,2}$");

        private bool IsTextNotAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

        private bool IsInRange(string text)
        {
            if (int.TryParse(text, out int num))
            {
                return num <= MaxRange && num >= MinRange;
            }
            else
                return false;
        }
        private void Button_Click_Up(object sender, RoutedEventArgs e)
        {
            int numText = int.Parse(Text);
            numText++;
            Text = numText.ToString();
        }

        private void Button_Click_Down(object sender, RoutedEventArgs e)
        {
            int numText = int.Parse(Text);
            numText--;
            Text = numText.ToString();
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (IsTextNotAllowed(e.Text))
            {
                e.Handled = Block();
            }
            else
            {
                e.Handled = Pass();
            }
        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(Text, out int num))
            {
                Text = num == 0 ? MinRange.ToString() : num.ToString();
            }
            else
                Text = MinRange.ToString();
        }

        private void textBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = Block();
            }
        }

        private void textBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string text = (string)e.DataObject.GetData(typeof(string));
                if (IsTextNotAllowed(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(Text, out int num))
            {
                if (num > MaxRange)
                {
                    int careteIdx = ((TextBox)sender).CaretIndex;
                    Text = MaxRange.ToString();
                    PlaceCareteAtSamePlace((TextBox)sender, careteIdx);
                }
            }
        }

        private bool Block() => true;
        private bool Pass() => false;
        private void PlaceCareteAtSamePlace(TextBox txtBox, int idx)
        {
            txtBox.CaretIndex = idx;
        }
    }
}
