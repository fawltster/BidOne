using System;
using System.IO;
using System.Threading.Tasks;
using BidOneCodingTest.Models;
using Newtonsoft.Json;

namespace BidOneCodingTest
{
     /// <summary>
     /// Encapsulates storage of the user's first and last name in a JSON file.
     /// </summary>
    public class JSONUserConfigurationStorage : IUserConfigurationStorage
    {
        public string UserConfigurationFileFullPath { get; }
        private const string FileName = "UserConfiguration.json";
        public const string DefaultFirstName = "Matt";
        public const string DefaultLastName = "Williams";

        public JSONUserConfigurationStorage() 
        {
            UserConfigurationFileFullPath = Path.Combine(Path.GetTempPath(), FileName);                   
        }

        private void EnsureExists(string userConfigurationPath)
        {
            if (!File.Exists(UserConfigurationFileFullPath))
            {
                // set default values
                string jsonText = JsonConvert.SerializeObject(new UserConfiguration() { FirstName = DefaultFirstName, LastName = DefaultLastName });
                File.WriteAllText(UserConfigurationFileFullPath, jsonText);
            }
        }

        public async Task<UserConfiguration> GetUserConfigAsync() 
        {
            EnsureExists(UserConfigurationFileFullPath);    

            string jsonText = await File.ReadAllTextAsync(UserConfigurationFileFullPath);
            return JsonConvert.DeserializeObject<UserConfiguration>(jsonText);
        }

        public async Task SetUserConfigAsync(UserConfiguration config)
        {
            if (config == null) 
                throw new ArgumentNullException("User config was null");

            if (String.IsNullOrWhiteSpace(config.FirstName)) 
                throw new ArgumentNullException("First name was null or empty");

            if (String.IsNullOrWhiteSpace(config.LastName)) 
                throw new ArgumentNullException("Last Name was null or empty");

            string jsonText = JsonConvert.SerializeObject(config);
            
            EnsureExists(UserConfigurationFileFullPath);    

            await File.WriteAllTextAsync(UserConfigurationFileFullPath, jsonText);
        }


    }
}
