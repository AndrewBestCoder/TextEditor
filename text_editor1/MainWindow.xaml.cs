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
  
    public partial class MainWindow : Window
    {
        public static SearchAndReplaceWindow find_word;
        public static ReferenceWindow reference;
        private FindSubstring _substringFinder;
        public MainWindow()
        {
            InitializeComponent();
            _substringFinder = new FindSubstring();
        }

        private void OpenButtonClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "txt files (*.txt)|*.txt|rtf files (*.rtf)|*.rtf";

            if (openFileDialog.ShowDialog() == true)
            {

                TextRange doc = new TextRange(MainText.Document.ContentStart, MainText.Document.ContentEnd);

                using (FileStream fileStream = new FileStream(openFileDialog.FileName, FileMode.Open))
                {
                    if (System.IO.Path.GetExtension(openFileDialog.FileName).ToLower() == ".rtf")
                    {
                        doc.Load(fileStream, DataFormats.Rtf);
                    }
                    else if (System.IO.Path.GetExtension(openFileDialog.FileName).ToLower() == ".txt")
                    {
                        doc.Load(fileStream, DataFormats.Text);
                    }
                    else
                    {
                        doc.Load(fileStream, DataFormats.Xaml);
                    }
                }           
            }
        }
        private void SaveAsButtonClick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "txt files (*.txt)|*.txt|rtf files (*.rtf)|*.rtf";

            if(saveFileDialog.ShowDialog() == true)
            {
                TextRange doc = new TextRange(MainText.Document.ContentStart, MainText.Document.ContentEnd);
                using (FileStream fileStream = File.Create(saveFileDialog.FileName))
                {
                    if (System.IO.Path.GetExtension(saveFileDialog.FileName).ToLower() == ".rtf")
                    {
                        doc.Save(fileStream, DataFormats.Rtf);
                    }
                    else if (System.IO.Path.GetExtension(saveFileDialog.FileName).ToLower() == ".txt")
                    {
                        doc.Save(fileStream, DataFormats.Text);
                    }
                    else
                    {
                        doc.Save(fileStream, DataFormats.Xaml);
                    }
                }

            }
        }
        private void Find(object sender, RoutedEventArgs e)
        {
            if (find_word == null)
            {
                find_word = new SearchAndReplaceWindow();
                find_word.Show();
            }
            else find_word.Activate();
        }

        private void Reference_button(object sender, RoutedEventArgs e)
        {
            if (reference == null)
            {
                reference = new ReferenceWindow();
                reference.Show();
            }
            else reference.Activate();
        }

        private void Close_button(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
