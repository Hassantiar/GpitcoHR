﻿using Blazored.LocalStorage;

namespace ClientLibrary.Helper
{
    public class LocalStorgService(ILocalStorageService localStorageService)
    {
        private const string storagekey = "authentication-token";
        public async Task<string> GetToken() => await localStorageService.GetItemAsStringAsync(storagekey);
        public async Task SetToken(string item) => await localStorageService.SetItemAsStringAsync(storagekey, item);
        public async Task RemoveToken()=> await localStorageService.RemoveItemAsync(storagekey);
    }
}
