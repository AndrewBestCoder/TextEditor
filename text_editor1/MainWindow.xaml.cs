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


/*        private void FindButtonClick(object sender, RoutedEventArgs e)
        {
            _substringFinder.Find(MainText, FindText.Text);
            
        }

        private void RigthtStepButtonClick(object sender, RoutedEventArgs e)
        {
            _substringFinder.ShowNext(MainText);
        }

        private void LeftStepButtonClick(object sender, RoutedEventArgs e)
        {
            _substringFinder.ShowPrev(MainText);
        }*/
    }
}
