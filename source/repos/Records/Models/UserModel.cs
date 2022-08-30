using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Records.Models
{
    public class UserModel
    {
        private UserInformationDataContext db = new UserInformationDataContext();

        //
        // Query Methods

        public IQueryable<User> FindAllUsers()
        {
            return db.Users;
        }

        
        public User GetUser(int id)
        {
            return db.Users.SingleOrDefault(d => d.Id== id);
        }
        public User GetUserByName(string id)
        {
            return db.Users.SingleOrDefault(d => d.Name== id);
        }
        public string[] GetUserByNameForAuthentication(string id)
        {
            string[] str = (from values in db.Users
                                                     where values.Name==id
                                                     select values.Name.TrimEnd()).ToArray();
            return str;
        }

        public void Add(User user)
        {
            db.Users.InsertOnSubmit(user);
        }

        public void Delete(User user)
        {

            db.Users.DeleteOnSubmit(user);
        }

        //
        // Persistence 

        public void Save()
        {
            db.SubmitChanges();
        }
        public string Enc( string Pass)
        {
            string a="";
            Pass.ToCharArray();
            foreach (var item in Pass)
            {
                char c =Convert.ToChar( item + 3);
                a += c;
            }
            return a;
        }
        public string Dec(string Pass)
        {
            string a = "";
            Pass.ToCharArray();
            foreach (var item in Pass)
            {
                char c = Convert.ToChar(item - 3);
                a += c;
            }
            return a;
        }

    }
}