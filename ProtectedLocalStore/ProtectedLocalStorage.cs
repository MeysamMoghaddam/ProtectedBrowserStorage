using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.JSInterop;

namespace ProtectedLocalStore
{

    public class ProtectedLocalStorage
    {
        private readonly EncryptionService _encryptionService;
        ILocalStorageService _localStorageService;
        ISyncLocalStorageService _syncLocalStorageService;

        public ProtectedLocalStorage(IJSRuntime jSRuntime, EncryptionService encryptionService, ILocalStorageService localStorageService, ISyncLocalStorageService syncLocalStorageService)
        {
            _encryptionService = encryptionService;
            _localStorageService = localStorageService;
            _syncLocalStorageService = syncLocalStorageService;
        }

        public void SetLocal(string key, object entryObj)
        {
            string jsonSerialize = JsonSerializer.Serialize(entryObj);
            _syncLocalStorageService.SetItem(key, _encryptionService.Encrypt(jsonSerialize));
        }

        public T GetLocal<T>(string key)
        {
            string encryptData = _syncLocalStorageService.GetItem<string>(key);
            var data = JsonSerializer.Deserialize<T>(_encryptionService.Decrypt(encryptData));
            return data;
        }

        public async Task SetLocalAsync(string key, object entryObj)
        {
            string jsonSerialize = JsonSerializer.Serialize(entryObj);
            await _localStorageService.SetItemAsync(key, _encryptionService.Encrypt(jsonSerialize));
        }

        public async Task<T> GetLocalAsync<T>(string key)
        {
            return await Task.Run<T>(() => GetLocal<T>(key));
        }

        public async Task<bool> IsExistkeyAsync(string key) => await _localStorageService.ContainKeyAsync(key);
        public bool IsExistkey(string key)
        {
            return _syncLocalStorageService.ContainKey(key);
        }

    }
}
