using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Blazored.SessionStorage;

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

            string encryptData = await _sessionStorageService.GetItemAsync<string>(key);
            var data = JsonSerializer.Deserialize<T>(_encryptionService.Decrypt(encryptData));
            return data;

        }

    }
}
