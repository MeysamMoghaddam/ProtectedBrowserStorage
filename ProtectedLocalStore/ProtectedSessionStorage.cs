using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Blazored.SessionStorage;
using Microsoft.JSInterop;

namespace ProtectedLocalStore
{

    public class ProtectedSessionStorage
    {
        private readonly EncryptionService _encryptionService;
        ISessionStorageService _sessionStorageService;
        ISyncSessionStorageService _syncSessionStorageService;

        public ProtectedSessionStorage(EncryptionService encryptionService, ISessionStorageService sessionStorageService, ISyncSessionStorageService syncSessionStorageService)
        {
            _encryptionService = encryptionService;
            _sessionStorageService = sessionStorageService;
            _syncSessionStorageService = syncSessionStorageService;
        }

        public void SetSession(string key, object entryObj)
        {
            string jsonSerialize = JsonSerializer.Serialize(entryObj);
            _syncSessionStorageService.SetItem(key, _encryptionService.Encrypt(jsonSerialize));
        }

        public T GetSession<T>(string key)
        {
            string encryptData = _syncSessionStorageService.GetItem<string>(key);
            var data = JsonSerializer.Deserialize<T>(_encryptionService.Decrypt(encryptData));
            return data;
        }

        public async Task SetSessionAsync(string key, object entryObj)
        {
            string jsonSerialize = JsonSerializer.Serialize(entryObj);
            await _sessionStorageService.SetItemAsync(key, _encryptionService.Encrypt(jsonSerialize));
        }

        public async Task<T> GetSessionAsync<T>(string key)
        {
            return await Task.Run<T>(() => GetSession<T>(key));
        }
        public async Task<bool> IsExistkeyAsync(string key) => await _sessionStorageService.ContainKeyAsync(key);
        public bool IsExistkey(string key)
        {
            return _syncSessionStorageService.ContainKey(key);
        }
    }
}
