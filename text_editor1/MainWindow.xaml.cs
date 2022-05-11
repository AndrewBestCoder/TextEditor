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
            cmbFontFamily.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            cmbFontSize.ItemsSource = new List<double>() {8,9,10,11,12,14,16,18,20,22,24,26,28,36,};

            MainText.AddHandler(RichTextBox.DragOverEvent, new DragEventHandler(RichTextBox_DragOver), true);
            MainText.AddHandler(RichTextBox.DropEvent, new DragEventHandler(RichTextBox_Drop), true);
        }
        private void cmbFontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbFontFamily.SelectedItem != null)
                MainText.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, cmbFontFamily.SelectedItem);
        }
        private void cmbFontSize_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(cmbFontSize.Text, out int result))
            {
                MainText.Selection.ApplyPropertyValue(Inline.FontSizeProperty, (double)result);
            }

        }
        private void rtbEditor_SelectionChanged(object sender, RoutedEventArgs e)
        {

            object temp = MainText.Selection.GetPropertyValue(Inline.FontWeightProperty);
            btnBold.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontWeights.Bold));
            temp = MainText.Selection.GetPropertyValue(Inline.FontStyleProperty);
            btnItalic.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontStyles.Italic));
            temp = MainText.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            btnUnderline.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(TextDecorations.Underline));

            temp = MainText.Selection.GetPropertyValue(Inline.FontFamilyProperty);
            cmbFontFamily.SelectedItem = temp;
            temp = MainText.Selection.GetPropertyValue(Inline.FontSizeProperty);
            cmbFontSize.Text = temp.ToString();
        }
        //Сохранение и открытие файлов
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
        private void RichTextBox_DragOver(object sender, DragEventArgs e)
        {
            
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.All;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
            e.Handled = false;
        }

        private void RichTextBox_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] docPath = (string[])e.Data.GetData(DataFormats.FileDrop);

                var dataFormat = DataFormats.Rtf;

                if (e.KeyStates == DragDropKeyStates.ShiftKey)
                {
                    dataFormat = DataFormats.Text;
                }

                System.Windows.Documents.TextRange range;
                System.IO.FileStream fStream;
                if (System.IO.File.Exists(docPath[0]))
                {
                    try
                    {
                        range = new System.Windows.Documents.TextRange(MainText.Document.ContentStart, MainText.Document.ContentEnd);
                        fStream = new System.IO.FileStream(docPath[0], System.IO.FileMode.OpenOrCreate);
                        range.Load(fStream, dataFormat);
                        fStream.Close();
                    }
                    catch (System.Exception)
                    {
                        MessageBox.Show("File could not be opened. Make sure the file is a text file.");
                    }
                }
            }
        }
        private void Find(object sender, RoutedEventArgs e)
        {
            SearchAndReplaceWindow sarw = new SearchAndReplaceWindow();
            sarw.ShowDialog();
        }

        private void Reference_button(object sender, RoutedEventArgs e)
        {
            ReferenceWindow rw = new ReferenceWindow();
            rw.ShowDialog();
        }

        private void Close_button(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BoldButtonClick(object sender, RoutedEventArgs e)
        {
        }

    }
}
