# UFetch — Modern Async/Await HTTP Client for Unity

A clean, modern, JavaScript-style fetch wrapper for Unity using async/await and UnityWebRequest.

## ✨ Features

- ✅ Simple API: `UFetch.Get()`, `Post()`, `Put()`, `Delete()`
- ✅ Generic typed requests: `UFetch.Get<T>()` and `Post<T>()`
- ✅ JSON serialization via Newtonsoft.Json
- ✅ Multipart upload with `Upload()` and WWWForm
- ✅ Upload & Download progress callbacks
- ✅ Custom headers support
- ✅ Flexible error handling (exceptions or result objects)
- ✅ Access to raw response data (bytes and text)
- ✅ Fully awaitable and works in Unity runtime

## 📦 Installation

### Via Unity Package Manager (GitHub)

1. Open Unity Package Manager (Window → Package Manager)
2. Click the **+** button → **Add package from git URL**
3. Enter: `https://github.com/herbou/UFetch.git?path=/Assets/UFetch`
4. Click **Add**

The Newtonsoft.Json dependency will be automatically installed.

### Via manifest.json

Add this to your `Packages/manifest.json`:

```json
{
  "dependencies": {
    "com.yourname.ufetch": "https://github.com/herbou/UFetch.git?path=/Assets/UFetch"
  }
}
```

## 🚀 Quick Start

### Simple GET Request

```csharp
var response = await UFetch.Get("https://api.example.com/data");
Debug.Log(response.Text);
```

### POST JSON Data

```csharp
var jsonBody = JsonConvert.SerializeObject(new { username = "Player1", score = 100 });
var response = await UFetch.Post("https://api.example.com/submit", jsonBody);
Debug.Log(response.Text);
```

### Typed GET Request

```csharp
public class User {
    public string name;
    public int age;
}

User user = await UFetch.Get<User>("https://api.example.com/user/123");
Debug.Log(user.name);
```

### Download with Progress

```csharp
await UFetch.Get("https://example.com/largefile.zip", new UFetch.Options {
    OnDownloadProgress = progress => {
        Debug.Log($"Download: {progress * 100:F1}%");
    }
});
```

### Custom Headers

```csharp
var response = await UFetch.Get("https://api.example.com/protected", new UFetch.Options {
    Headers = {
        { "Authorization", "Bearer YOUR_TOKEN" }
    }
});
```

### Error Handling

```csharp
// Exception style (default)
try {
    var response = await UFetch.Get("https://api.example.com/data");
} catch (Exception ex) {
    Debug.LogError($"Request failed: {ex.Message}");
}

// Manual style
var res = await UFetch.Get("https://api.example.com/data", new UFetch.Options {
    ThrowOnError = false
});

if (res.IsError) {
    Debug.LogError($"Error [{res.StatusCode}]: {res.Error}");
}
```

## 📚 API Reference

### Static Methods

| Method | Description |
|--------|-------------|
| `Get(url, options)` | Performs a GET request |
| `Get<T>(url, options)` | GET request with typed JSON response |
| `Post(url, jsonBody, options)` | POST request with JSON body |
| `Post<T>(url, jsonBody, options)` | POST with typed JSON response |
| `Put(url, jsonBody, options)` | PUT request with JSON body |
| `Delete(url, options)` | DELETE request |
| `Upload(url, formData, options)` | Multipart form upload |
| `Request(method, url, options)` | Custom HTTP method |

### Options Class

```csharp
public class Options {
    public Dictionary<string, string> Headers;          // Custom headers
    public byte[] BodyRaw;                             // Raw binary body
    public string JsonBody;                            // JSON string body
    public WWWForm FormData;                           // Multipart form data
    public Action<float> OnDownloadProgress;           // Download progress (0-1)
    public Action<float> OnUploadProgress;             // Upload progress (0-1)
    public bool ThrowOnError = true;                   // Throw on HTTP errors
}
```

### UFetchResponse Class

```csharp
public class UFetchResponse {
    public string Text;                    // Response as string
    public byte[] RawData;                // Response as bytes
    public long StatusCode;               // HTTP status code
    public bool IsError;                  // True if request failed
    public string Error;                  // Error message
    public UnityWebRequest UnityRequest;  // Underlying request object
}
```

## 📖 Full Documentation

For complete documentation with more examples, see [UFetch_Documentation.md](./UFetch_Documentation.md)

## 🔧 Requirements

- Unity 2021.3 or later (for async/await support)
- Newtonsoft.Json for Unity (automatically installed)
- .NET Standard 2.0+

## 📄 License

MIT License — Free to use in personal and commercial projects.

## 🤝 Contributing

Contributions, issues, and feature requests are welcome!

## ⭐ Support

If you find UFetch helpful, consider giving it a star on GitHub!
