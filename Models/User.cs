using SQLite;
using System.ComponentModel.DataAnnotations;

namespace PMU_APP.Models
{
    public class User
    {
        private int _id;
        private string _username;
        private string _email;
        private string _password;

        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get => _id;
            set => _id = value;
        }

        [Unique, NotNull]
        public string Username
        {
            get => _username;
            set => _username = value;
        }

        [Unique, NotNull, EmailAddress]
        public string Email
        {
            get => _email;
            set => _email = value;
        }

        [NotNull]
        public string Password
        {
            get => _password;
            set => _password = value;
        }
    }
}
