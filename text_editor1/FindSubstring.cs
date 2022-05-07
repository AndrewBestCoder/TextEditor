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

                string word = substring;

                TextRange text = new TextRange(richText.Document.ContentStart, richText.Document.ContentEnd);
                TextPointer current = text.Start.GetInsertionPosition(LogicalDirection.Forward);
                while (true)
                {
                    string textOnRun = current.GetTextInRun(LogicalDirection.Forward);


                    int index = textOnRun.IndexOf(word);
                    if (index >= 0)
                    {
                        TextPointer first = current.GetPositionAtOffset(index, LogicalDirection.Forward);
                        TextPointer last = current.GetPositionAtOffset(word.Length, LogicalDirection.Forward);
                        _wordsRanges.Add(new TextRange(first, last));

                    }
                    else
                    {
                        break;
                    }
                   
                }
            }
        }
        public void ShowNext(RichTextBox richText)
        {
            if (_wordsRanges.Count != 0 && _iterator != _wordsRanges.Count - 1)
            {
                _iterator += 1;
                richText.Selection.Select(_wordsRanges[_iterator].Start, _wordsRanges[_iterator].End);
            }
        }
        public void ShowPrev(RichTextBox richText)
        {
            if (_wordsRanges.Count != 0 && _iterator != 0)
            {
                _iterator -= 1;
                richText.Selection.Select(_wordsRanges[_iterator].Start, _wordsRanges[_iterator].End);
            }
        }
        
    }
}
