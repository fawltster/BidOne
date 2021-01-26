using System;
using System.IO;
using System.Threading.Tasks;
using BidOneCodingTest.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BidOneCodingTest.Tests
{
    [TestClass]
    public class UserConfigurationStorageTests
    {
        [TestMethod]
        public void ConfigFileFullPath_MustBe_Initialized()
        {
            var storage = new JSONUserConfigurationStorage();                        
            Assert.IsFalse(String.IsNullOrEmpty(storage.UserConfigurationFileFullPath));
        }

        [TestMethod]
        public void ConfigFile_Must_Exist()
        {
            var storage = new JSONUserConfigurationStorage();                        
            Assert.IsTrue(File.Exists(storage.UserConfigurationFileFullPath));
        }
                
        [TestMethod]
        public async Task ConfigFile_MustBe_CreatedFromDefaultsIfNonExistent()
        {
            JSONUserConfigurationStorage storage = new JSONUserConfigurationStorage();
            File.Delete(storage.UserConfigurationFileFullPath);

            var userConfig = await storage.GetUserConfigAsync();

            Assert.IsTrue(userConfig.FirstName == JSONUserConfigurationStorage.DefaultFirstName);
            Assert.IsTrue(userConfig.LastName == JSONUserConfigurationStorage.DefaultLastName);
            Assert.IsTrue(File.Exists(storage.UserConfigurationFileFullPath));            
        }

        [TestMethod]
        public async Task ConfigValues_Can_BeModified()
        {
            var storage = new JSONUserConfigurationStorage();
            var newfirstName = Guid.NewGuid().ToString();
            var newlastName =  Guid.NewGuid().ToString();

            var configToUpdate = new UserConfiguration()
            {
                FirstName = newfirstName,
                LastName = newlastName
            };

            await storage.SetUserConfigAsync(configToUpdate);
            var reloadedConfig = await storage.GetUserConfigAsync();

            Assert.IsTrue(reloadedConfig.FirstName == newfirstName);
            Assert.IsTrue(reloadedConfig.LastName ==  newlastName);

        }

        [TestMethod]
        public async Task ConfigValues_Cannot_BeSavedWithInvalidFirstName()
        {
            var storage = new JSONUserConfigurationStorage();
            var invalidfirstName = string.Empty;
            var lastName =  Guid.NewGuid().ToString();

            var configToUpdate = new UserConfiguration()
            {
                FirstName = invalidfirstName,
                LastName = lastName
            };

           Exception ex = await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => {
                 await storage.SetUserConfigAsync(configToUpdate);
            });

            Assert.IsTrue(ex.Message == "Value cannot be null. (Parameter 'First name was null or empty')");

        }

    }
}
