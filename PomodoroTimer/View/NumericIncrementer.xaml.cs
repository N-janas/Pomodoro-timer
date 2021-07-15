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
                Debug.WriteLine(value);
                if (IsInRange(value)) // IsTextAllowed(value) && 
                {
                    SetValue(TextProperty, value);

                }
            }
        }

        private static readonly Regex _regex = new Regex("^[0-9]$"); // ^([1 - 9]|[1-5] [0-9]|60)$

        private bool IsTextNotAllowed(string text)
        {
            Debug.WriteLine(text);
            Debug.WriteLine("IsTextAllowed : " + _regex.IsMatch(text).ToString());
            return !_regex.IsMatch(text);
        }

        private bool IsInRange(string text)
        {
            if (int.TryParse(text, out int num))
            { 
                Debug.WriteLine("IS in range :" + (num <= MaxRange && num >= MinRange).ToString());
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
            if (IsTextNotAllowed(Text) || IsTextNotAllowed(e.Text))
            {
                e.Handled = Block();
            }
            else
            {
                e.Handled = Pass();
            }
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Text))
            {
                Text = "1";
            }
        }

        private void textBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = Block();
            }
        }

        private bool Block() => true;
        private bool Pass() => false;
    }
}
