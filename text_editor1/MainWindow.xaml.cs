using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace text_editor1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _currentPath;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenButtonClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true) { 
                MainText.Text = File.ReadAllText(openFileDialog.FileName);
                _currentPath = openFileDialog.FileName;
            }
        }
        private void SaveAsButtonClick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                File.Create(saveFileDialog.FileName);
                File.WriteAllText(saveFileDialog.FileName, MainText.Text);
                _currentPath = saveFileDialog.FileName;
            }
                

            
        }

        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            if(File.Exists(_currentPath))
            {
                File.WriteAllText(_currentPath, MainText.Text);
            }
            else
            {
                SaveAsButtonClick(sender, new RoutedEventArgs());
            }
        }
    }
}
