# ProtectedBrowserStorage
Provides services for storing data in the browser's localStorage. The stored data is protected using AES encryption and decryption functions.
I'm use this in my Blazor WebAssembly client-side project.

# Install
```
Install-Package ProtectedBrowserStorage.NETStandard -Version x.x
```  
x.x is version of package for use last version see https://www.nuget.org/packages/ProtectedBrowserStorage.NETStandard

# How to use
Add this line to Startup.cs. Register into DI container


```
services.AddProtectedLocalStore(new EncryptionService(
                new KeyInfo("45BLO2yoJkvBwz99kBEMlNkxvL40vUSGaqr/WBu3+Vg=", "Ou3fn+I9SVicGWMLkFEgZQ==")));
```        

## For Use Blazor WebAssembly client-side Register into Program.cs

```
builder.Services.AddProtectedLocalStore(new EncryptionService(
                new KeyInfo("45BLO2yoJkvBwz99kBEMlNkxvL40vUSGaqr/WBu3+Vg=", "Ou3fn+I9SVicGWMLkFEgZQ==")));
```   

In _Imports.razor add this

```
@using ProtectedLocalStore
@inject ProtectedLocalStore _protectedLocalStore
``` 

Now we can use it in our Component.

### Use browser session storage

set Synchronous protected data
```
_protectedLocalStore.SetSession("key", data);
``` 

set Asynchronous protected data
```
_protectedLocalStore.SetSessionAsync("key", data);
``` 

get Synchronous data
```
_protectedLocalStore.GetSession<T>("key");
``` 

get Asynchronous data
```
_protectedLocalStore.GetSessionAsync<T>("key");
``` 

### Use browser local storage

set Synchronous protected data
```
_protectedLocalStore.SetLocal("key", data);
``` 

set Asynchronous protected data
```
_protectedLocalStore.SetLocalAsync("key", data);
``` 

get Synchronous data
```
_protectedLocalStore.GetLocal<T>("key");
``` 

get Asynchronous data
```
_protectedLocalStore.GetLocalAsync<T>("key");
``` 

*** I'm glad to see your comments ***
