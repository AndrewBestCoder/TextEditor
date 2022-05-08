using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace text_editor1
{
    internal class FindSubstring
    {
        private List<TextRange> _wordsRanges;
        private int _iterator = 0;
        public FindSubstring()
        {
            _wordsRanges = new List<TextRange>();
        }
        public void Find(RichTextBox richText, string substring)
        {
            _wordsRanges.Clear();
            if (substring != "")
            {
                int memoryIndex = 0;
                string word = substring;

                TextRange text = new TextRange(richText.Document.ContentStart, richText.Document.ContentEnd);

                TextPointer current = text.Start.GetInsertionPosition(LogicalDirection.Forward);
                TextPointer FullTextCurrent = current; 
                while (current != null)
                {
                    string textOnRun = current.GetTextInRun(LogicalDirection.Forward);


                    int index = textOnRun.IndexOf(word);
                    memoryIndex  += index + word.Length;
                    if (index >= 0)
                    {
                        TextPointer first = FullTextCurrent.GetPositionAtOffset(memoryIndex - word.Length, LogicalDirection.Backward);
                        TextPointer last = FullTextCurrent.GetPositionAtOffset(memoryIndex, LogicalDirection.Forward);
                        _wordsRanges.Add(new TextRange(first, last));
                    }
                    else
                    {
                        break;
                    }
                    current = current.GetPositionAtOffset(index + word.Length);
                }
            }
            if (_wordsRanges.Count > 0)
            {
                richText.Selection.Select(_wordsRanges[0].Start, _wordsRanges[0].End);
                richText.Focus();
            }
            else
            {
                MessageBox.Show("Совпадений не найдено");
            }
        }
        public void ShowNext(RichTextBox richText)
        {
            if (_wordsRanges.Count != 0 && _iterator != _wordsRanges.Count - 1)
            {
                _iterator += 1;
                richText.Selection.Select(_wordsRanges[_iterator].Start, _wordsRanges[_iterator].End);
                richText.Focus();
            }
        }
        public void ShowPrev(RichTextBox richText)
        {
            if (_wordsRanges.Count != 0 && _iterator != 0)
            {
                _iterator -= 1;
                richText.Selection.Select(_wordsRanges[_iterator].Start, _wordsRanges[_iterator].End);
                richText.Focus();
            }
        }
        
    }
}
