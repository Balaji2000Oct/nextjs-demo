using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Records.Models
{
    public class BookerModel
    {
        private BookersDataContext db = new BookersDataContext();


        public IQueryable<BookedUser> FindAllUsers()
        {
            return db.BookedUsers;
        }
        public IQueryable<BookedUser> FindBookedDinnersDinners(string N)
        {
            return from dinner in db.BookedUsers
                   where dinner.Name==N
                   orderby dinner.BookId
                   select dinner;
        }

        //
        // Insert/Delete Methods

        public void Add(BookedUser dinner)
        {
            db.BookedUsers.InsertOnSubmit(dinner);
        }

        public void Delete(BookedUser dinner)
        {

            db.BookedUsers.DeleteOnSubmit(dinner);
        }

        //
        // Persistence 
        public BookedUser GetUser(int id)
        {
            return db.BookedUsers.SingleOrDefault(d =>d.BookId== id);
        }
        public void Save()
        {
            db.SubmitChanges();
        }
    }
}