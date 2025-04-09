using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PMU_APP.Utils
{
    public static class Validators
    {
        public static bool IsValidUsername(string username)
        {
            // Only letters, numbers, 3 to 20 characters
            return Regex.IsMatch(username, @"^[a-zA-Z0-9]{3,20}$");
        }

        public static bool IsValidEmail(string email)
        {
            // Simple email validation
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        public static bool IsValidPassword(string password)
        {
            // At least 8 characters, 1 uppercase, 1 lowercase, 1 number, 1 special character
            return Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$");
        }
    }
}
