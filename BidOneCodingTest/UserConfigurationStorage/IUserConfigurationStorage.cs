using System.Threading.Tasks;
using BidOneCodingTest.Models;

namespace BidOneCodingTest
{
    // Encapsulates storage of a user's details for app config purposes
    public interface IUserConfigurationStorage
    {
        Task<UserConfiguration> GetUserConfigAsync();
        Task SetUserConfigAsync(UserConfiguration config);
    }
}