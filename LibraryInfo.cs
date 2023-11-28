using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    [Serializable]
    public class LibraryInfo : IComparable<LibraryInfo> 
    {
        public LibraryInfo() { }

        public string _userID { get; set; }
        public string _name { get; set; }
        public string _bookID { get; set; }
        public string _bookTitle { get; set; }
        public string _borrowedDate { get; set; }
        public string _dueDate { get; set; }
        public string _bookStatus { get; set; }
        public string _returnDate { get; set; }
        public string _fine { get; set; }

        public int CompareTo(LibraryInfo other)
        {
            if (other == null)
            {
                return 1;
            }
            if (_userID.CompareTo(other._userID) != 0)
            {
                return _userID.CompareTo(other._userID);
            }
            // If UserID is the same, compare BookID.
            if (_bookID.CompareTo(other._bookID) != 0)
            {
                return _bookID.CompareTo(other._bookID);
            }
            if (_bookTitle.CompareTo(other._bookStatus) != 0)
            {
                return _bookTitle.CompareTo(other._bookStatus);
            }
            // If no other comparison is needed, consider the objects equal.
            return 0;
        }
    }
}
