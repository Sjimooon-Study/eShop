using Blazored.LocalStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Blazor.Helpers.LocalStorage
{
    public class User
    {
        private static readonly string EDIT_MODE = "edit_mode";

        private readonly ILocalStorageService _localStorageService;

        public static async Task<User> Create(ILocalStorageService localStorageService)
        {
            User user = new(localStorageService);
            await user.SetEditModeAsync(false);

            return user;
        }

        private User(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public async Task<bool> IsInEditModeAsync()
        {
            if (await _localStorageService.ContainKeyAsync(EDIT_MODE) && await _localStorageService.GetItemAsync<bool>(EDIT_MODE))
            {
                return true;
            }

            return false;
        }

        public async Task SetEditModeAsync(bool state)
        {
            await _localStorageService.SetItemAsync(EDIT_MODE, state);
        }
    }
}
