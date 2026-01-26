using System.Windows;

namespace ServiceAccountForm
{
    public partial class SuccessDialog : Window
    {
        // Constructor that accepts the ART Number
        public SuccessDialog(string artNumber)
        {
            InitializeComponent();

            // Set the text box value
            txtArtField.Text = artNumber;

            // Optional: Auto-select the text so the user can easily copy it
            txtArtField.SelectAll();
            txtArtField.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}