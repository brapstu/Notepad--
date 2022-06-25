using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.IO;
using Microsoft.Win32;

namespace Notepad__
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public string OpenFile = "C:/Untitled.txt";
        public string CurrentFile = "Untitled";
        public bool FileChanged = false;


        private void NewClicked(object sender, RoutedEventArgs e)
        {
            if (TextBox.Text == "" || TextBox.Text == "Type here" || FileChanged == false) {
                TextBox.Text = String.Empty;
                CurrentFile = "Untitled";
                OpenFile = "";
                FileChanged = false;
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to create a new document?", "Confirmation", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.OK)
                {
                    TextBox.Text = String.Empty;
                    CurrentFile = "Untitled";
                    OpenFile = "";
                    FileChanged = false;
                }
            }
        }

        private void SaveClicked(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Document"; // Default file name
            dlg.DefaultExt = ".txt"; // Default file extension
            dlg.Filter = "All Files|*.*"; // Filter files by extension

            // Show save file dialog box
            bool? result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                TextWriter tw = new StreamWriter(dlg.FileName);
                tw.WriteLine(TextBox.Text);
                tw.Close();
                OpenFile = dlg.FileName;
                CurrentFile = System.IO.Path.GetFileName(OpenFile);
                MainWindow1.Title = CurrentFile + " : Notepad--";
                FileChanged = false;
            }

        }

        
        private void OpenClicked(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                TextBox.Text = File.ReadAllText(openFileDialog.FileName);
                OpenFile = openFileDialog.FileName;
                CurrentFile = System.IO.Path.GetFileName(OpenFile);
                MainWindow1.Title = CurrentFile + " : Notepad--";
                FileChanged = false;
        }

        private void SettingsClicked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This feature will be added soon.");
        }

        private void OnWindowClose(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (FileChanged == true)
            {
                MessageBoxResult result = MessageBox.Show("Do you want to save changes to " + CurrentFile + "?", "Unsaved Changes", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    TextWriter tw = new StreamWriter(OpenFile);
                    tw.WriteLine(TextBox.Text);
                    tw.Close();
                    FileChanged = false;
                    e.Cancel = true;
                }

                if (result == MessageBoxResult.No)
                {
                    // Do nothin :p
                }
            }
            else
            {
                // DO NOTHIN AGAIN!!!! caus u know
            }
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            FileChanged = true;
            MainWindow1.Title = "*" + CurrentFile + " : Notepad--";
        }

        private void SaveTClicked(object sender, RoutedEventArgs e) // Why did i call it saveT
        {
            if (CurrentFile == "Untitled")
            {
                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                dlg.FileName = "Document"; // Default file name
                dlg.DefaultExt = ".txt"; // Default file extension
                dlg.Filter = "All Files|*.*"; // Filter files by extension

                // Show save file dialog box
                bool? result = dlg.ShowDialog();

                // Process save file dialog box results
                if (result == true)
                {
                    // Save document
                    TextWriter tw = new StreamWriter(dlg.FileName);
                    tw.WriteLine(TextBox.Text);
                    tw.Close();
                    OpenFile = dlg.FileName;
                    CurrentFile = System.IO.Path.GetFileName(OpenFile);
                    MainWindow1.Title = CurrentFile + " : Notepad--";
                    FileChanged = false;
                }
            }
            else
            {
                TextWriter tw = new StreamWriter(OpenFile);
                tw.WriteLine(TextBox.Text);
                tw.Close();
                MainWindow1.Title = CurrentFile + " : Notepad--";
                FileChanged = false;
            }
        }
    }
}
