using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;
namespace Membership
{
    public class Authentication
    {
        private static int salt = 12;
        public static string Hash(string value)
        {
            if (value == null)
            {
                return "";
            }
            return BC.HashPassword(value, salt);
        }

        public static async Task<bool> validateAsync(string username, string password)
        {
            User user = await HandleUser.findOneUserByFieldAsync("username", username);
            if(user == null)
            {
                return false;
            }
            return BC.Verify(password, user.Password);
        }

    }
}
