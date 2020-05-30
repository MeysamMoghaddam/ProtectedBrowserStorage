using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Blazored.LocalStorage;

namespace ProtectedLocalStore
{
    
    public class ProtectedLocalStore
    {
        private readonly EncryptionService _encryptionService;
        ILocalStorageService _localStorageService;

        public ProtectedLocalStore(EncryptionService encryptionService, ILocalStorageService localStorageService)
        {
            _encryptionService = encryptionService;
            _localStorageService = localStorageService;
        }

        public async Task SetAsync(string key, object entryObj)
        {
            if (await _localStorageService.ContainKeyAsync(key))
                await _localStorageService.RemoveItemAsync(key);

            string jsonSerialize = JsonSerializer.Serialize(entryObj);
            await _localStorageService.SetItemAsync(key, _encryptionService.Encrypt(jsonSerialize));
        }
        public void Set(string key, object entryObj)
        {
            //if (ContainKey(key))
            //    _localStorageService.RemoveItemAsync(key);

            string jsonSerialize = JsonSerializer.Serialize(entryObj);
            _localStorageService.SetItemAsync(key, _encryptionService.Encrypt(jsonSerialize));
        }
        public async Task<T> GetAsync<T>(string key)
        {

            string encryptData = await _localStorageService.GetItemAsync<string>(key);

            var data = JsonSerializer.Deserialize<T>(_encryptionService.Decrypt(encryptData));
            return data;

        }



    }
}
