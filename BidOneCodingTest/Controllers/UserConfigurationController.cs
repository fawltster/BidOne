using System.Threading.Tasks;
using BidOneCodingTest.Models;
using Microsoft.AspNetCore.Mvc;

namespace BidOneCodingTest.Controllers
{
    public class UserConfigurationController : Controller
    {
        private readonly IUserConfigurationStorage _UserConfigurationStorage;

        public UserConfigurationController(IUserConfigurationStorage userConfigurationStorage)
        {
            _UserConfigurationStorage = userConfigurationStorage;
        }

        public async Task<ActionResult> Index()
        {
            var model = await _UserConfigurationStorage.GetUserConfigAsync();                        
            return View(model);
        }
                  
        public async Task<ActionResult> Edit()
        {
            var model = await _UserConfigurationStorage.GetUserConfigAsync();
            return View(model);
        }
                
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UserConfiguration userConfig)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
               await _UserConfigurationStorage.SetUserConfigAsync(userConfig);
               return RedirectToAction(nameof(Index));    
            }           
        }
      

 
    }
}
