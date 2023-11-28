using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace library
{
    [Serializable]
    public class BookInfo : IComparable<BookInfo>
    {
        public BookInfo()
        {
        }
        private string _id;
        private string _title;
        private string _authors;
        private string _pubDate;
        private string _copies;
        private string _avlCopies;
        private string _keyword;

        public int CompareTo(BookInfo other)
        {
            if (other == null)
            {
                return 1;
            }
            if (!string.IsNullOrEmpty(_keyword))
            {
                string keywordLower = _keyword.ToLowerInvariant(); // Convert the keyword to lowercase

                bool keywordInTitle = !string.IsNullOrEmpty(_title) && _title.ToLowerInvariant().Contains(keywordLower);
                bool keywordInAuthors = !string.IsNullOrEmpty(_authors) && _authors.ToLowerInvariant().Contains(keywordLower);

                
                if (keywordInTitle && keywordInAuthors)
                {
                    // Decide how to prioritize the comparison when the keyword is present in both title and authors
                    // In this example, prioritize title
                    return string.Compare(_title, other._title, StringComparison.OrdinalIgnoreCase);
                }
                else if (keywordInTitle)
                {
                    return string.Compare(_title, other._title, StringComparison.OrdinalIgnoreCase);
                }
                else if (keywordInAuthors)
                {
                    return string.Compare(_authors, other._authors, StringComparison.OrdinalIgnoreCase);
                }
            }
            // If the keyword is not present in either title or authors, consider them equal
            return 0;
        }
        public string Keyword
        {
            get { return _keyword; }
            set { _keyword = value; }
        }

        public string GetBookID()
        {
            return _id;
        }
        public string GetTitle()
        {
            return _title;
        }
        public string GetAuthors()
        {
            return _authors;
        }
        public string GetPubDate()
        {
            return _pubDate;
        }
        public string GetCopies()
        {
            return _copies;
        }
        public string GetAvlCopies()
        {
            return _avlCopies;
        }
        public void SetBookID(string id)
        {
            _id = id;
        }
        public void SetTitle(string title)
        {
            _title = title;
        }
        public void SetAuthors(string authors)
        {
            _authors = authors;
        }
        public void SetPubDate(string pubDate)
        {
            _pubDate = pubDate;
        }
        public void SetCopies(string copies)
        {
            _copies = copies;
        }
        public void SetAvlCopies(string avlCopies)
        {
            _avlCopies = avlCopies;
        }
    }
}
