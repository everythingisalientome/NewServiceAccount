using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace ServiceAccountForm
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadDropdownData();
        }

        private void LoadDropdownData()
        {
            // 1. Select an Action
            var actions = new List<string>
            {
                "--Select One--",
                "Create a new Service account",
                "Modify an existing Service account",
                "Update ownership of an existing Service Account"
            };
            CmbAction.ItemsSource = actions;
            CmbAction.SelectedIndex = 0;

            // 2. Select Domain
            var domains = new List<string>
            {
                "--Select One--",
                "AD-ENT",
                "cnet.trzn.wachovia.net",
                "DEV-ENT",
                "QA-ENT",
                "xnet.trzn.wachovia.net",
                "xtch.xtrt.wachovia.net"
            };
            CmbDomain.ItemsSource = domains;
            CmbDomain.SelectedIndex = 0;

            // 3. Active Directory Groups
            var groups = new List<string>
            {
                "No",
                "1 group",
                "2 groups",
                "3 groups",
                "4 groups",
                "5 groups",
                "6 groups"
            };
            CmbADGroups.ItemsSource = groups;
            CmbADGroups.SelectedIndex = 0;

            // 4. Primary Use
            var primaryUses = new List<string>
            {
                "--Select One--",
                "Application ID",
                "Test ID",
                "File Transfer",
                "Other"
            };
            CmbPrimaryUse.ItemsSource = primaryUses;
            CmbPrimaryUse.SelectedIndex = 0;
        }

        private void txtAuNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtAuNumber.Text))
            {
                txtAuApproverName.Text = "OZA, GAUTAM";
                txtAuApproverName.Foreground = Brushes.Black;
                txtAuApproverName.FontStyle = FontStyles.Normal;
            }
            else
            {
                txtAuApproverName.Text = "(Auto-populated)";
                txtAuApproverName.Foreground = Brushes.Gray;
                txtAuApproverName.FontStyle = FontStyles.Italic;
            }
        }

        private async void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            // 1. Grey out and lock background
            MainFormGrid.IsEnabled = false;
            MainFormGrid.Opacity = 0.5;
            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                // 2. Sleep (Async wait)
                await Task.Delay(3000);

                // 3. Generate Random ART Number
                Random rnd = new Random();
                string randomDigits = rnd.Next(100000, 999999).ToString();
                string artNumber = $"ART{randomDigits}";

                // FIX #2: Reset Cursor BEFORE showing the dialog so the dialog has a normal pointer
                Mouse.OverrideCursor = null;

                // 4. Show the Custom Success Window
                SuccessDialog successPopup = new SuccessDialog(artNumber);
                successPopup.ShowDialog(); // Code waits here until user clicks OK
            }
            finally
            {
                // 5. Re-enable the UI
                MainFormGrid.IsEnabled = true;
                MainFormGrid.Opacity = 1.0;

                // Ensure cursor is normal (redundant safety check)
                Mouse.OverrideCursor = null;

                // FIX #3: Reset the form fields
                ResetForm();
            }
        }

        private void ResetForm()
        {
            // Reset TextBoxes
            txtLanID.Text = string.Empty;
            txtAuNumber.Text = string.Empty;
            txtPurpose.Text = string.Empty;
            txtImpact.Text = string.Empty;

            // Reset AU Approver Label
            txtAuApproverName.Text = "(Auto-populated)";
            txtAuApproverName.Foreground = Brushes.Gray;
            txtAuApproverName.FontStyle = FontStyles.Italic;

            // Reset Radio Buttons to defaults (mostly "No")
            rdNeedServiceAccountNo.IsChecked = true;
            rdAddElidNo.IsChecked = true;
            rdInteractiveNo.IsChecked = true;
            rdEPVCharacterLimitNo.IsChecked = true;
            rdLoadtestingNo.IsChecked = true;
            rdDatabaseUseNo.IsChecked = true;

            // Optional: Reset Radio Buttons that don't have a safe default "No"
            // (Like Production vs Non-Production). We can just clear them or set a default.
            rdUseNonProduction.IsChecked = false;
            rdUseProduction.IsChecked = false;

            // Reset Dropdowns to first index
            CmbAction.SelectedIndex = 0;
            CmbDomain.SelectedIndex = 0;
            CmbADGroups.SelectedIndex = 0;
            CmbPrimaryUse.SelectedIndex = 0;
        }
    }
}