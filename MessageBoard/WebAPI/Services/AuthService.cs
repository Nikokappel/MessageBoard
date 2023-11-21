using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Domain.Models;
using EfcDataAccess;


namespace WebAPI.Services;

public class AuthService: IAuthService
{
        private readonly IList<User> users;

       
        public AuthService()
        {
            users = LoadUsersFromDatabase();
        }

        private IList<User> LoadUsersFromDatabase()
        {
            try
            {
                using (MBoardContext context = new MBoardContext())
                {
                    List<User> loadedUsers = context.Users.ToList();

                    return loadedUsers;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error loading from database: {ex.Message}", ex);
            }
        }
        /*public AuthService()
        {
            users = 
          users = LoadUsersFromJsonFile("data.json");
        }

        private IList<User> LoadUsersFromJsonFile(string filePath)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string json = reader.ReadToEnd();
                    var jsonData = JsonSerializer.Deserialize<Json>(json);

                    if (jsonData != null && jsonData.Users != null)
                    {
                        return jsonData.Users;
                    }
                    else
                    {
                        throw new Exception("error in JSON format or 'Users' data.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error loading from JSON file: {ex.Message}", ex);
            }
        }
*/
        public Task<User> ValidateUser(string username, string password)
        {
            User? existingUser = users.FirstOrDefault(u =>
                u.UserName.Equals(username, StringComparison.OrdinalIgnoreCase));

            if (existingUser == null)
            {
                throw new Exception("User not found");
            }

            if (!existingUser.Password.Equals(password))
            {
                throw new Exception("Password mismatch");
            }
            return Task.FromResult(existingUser);
        }

        public Task RegisterUser(User user)
        {
            if (string.IsNullOrEmpty(user.UserName))
            {
                throw new ValidationException("Username cannot be null");
            }

            if (string.IsNullOrEmpty(user.Password))
            {
                throw new ValidationException("Password cannot be null");
            }

            users.Add(user);

            return Task.CompletedTask;
        }
}

