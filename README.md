# UFetch - Modern HTTP Client for Unity

A clean, modern, JavaScript-style fetch wrapper for Unity using async/await and UnityWebRequest.

## ✨ Features

- Simple API: `UFetch.Get()`, `Post()`, `Put()`, `Delete()`
- Generic typed requests: `UFetch.Get<T>()` for automatic JSON deserialization
- JSON serialization via Newtonsoft.Json
- Multipart uploads with progress tracking
- Custom headers support
- Flexible error handling (exceptions or result objects)
- Fully awaitable and works in Unity runtime

## 📦 Installation

### Via Unity Package Manager

1. Open Unity Package Manager (Window → Package Manager)
2. Click the **+** button → **Add package from git URL**
3. Enter: `https://github.com/herbou/UFetch.git?path=/Assets/UFetch`
4. Click **Add**

### Via manifest.json

Add this to your `Packages/manifest.json`:

```json
{
  "dependencies": {
    "com.herbou.ufetch": "https://github.com/herbou/UFetch.git?path=/Assets/UFetch"
  }
}
```

## 🚀 Quick Start

```csharp
using UnityEngine;

public class Example : MonoBehaviour {
    async void Start() {
        // Simple GET request
        var response = await UFetch.Get("https://api.example.com/data");
        Debug.Log(response.Text);

        // Typed GET request
        User user = await UFetch.Get<User>("https://api.example.com/user/123");
        Debug.Log(user.name);

        // POST with JSON
        var jsonBody = JsonConvert.SerializeObject(new { score = 100 });
        await UFetch.Post("https://api.example.com/submit", jsonBody);

        // Download with progress
        await UFetch.Get("https://example.com/file.zip", new UFetch.Options {
            OnDownloadProgress = p => Debug.Log($"Progress: {p * 100}%")
        });
    }
}

public class User {
    public string name;
    public int age;
}
```

## 📚 Documentation

Full documentation is available in the package:
- [README.md](Assets/UFetch/README.md) - Quick start guide
- [UFetch_Documentation.md](Assets/UFetch/UFetch_Documentation.md) - Complete API reference with examples

## 🔧 Requirements

- Unity 2021.3 or later (for async/await support)
- Newtonsoft.Json for Unity (automatically installed as dependency)
- .NET Standard 2.0+

## 📖 API Overview

### Main Methods

| Method | Description |
|--------|-------------|
| `Get(url, options)` | Performs a GET request |
| `Get<T>(url, options)` | GET request with typed JSON response |
| `Post(url, jsonBody, options)` | POST request with JSON body |
| `Post<T>(url, jsonBody, options)` | POST with typed response |
| `Put(url, jsonBody, options)` | PUT request with JSON body |
| `Delete(url, options)` | DELETE request |
| `Upload(url, formData, options)` | Multipart form upload |

### Options

```csharp
new UFetch.Options {
    Headers = new Dictionary<string, string> {
        { "Authorization", "Bearer TOKEN" }
    },
    OnDownloadProgress = progress => { /* 0.0 to 1.0 */ },
    OnUploadProgress = progress => { /* 0.0 to 1.0 */ },
    ThrowOnError = false  // Disable exceptions for manual error handling
}
```

### Response Object

```csharp
UFetchResponse {
    string Text;              // Response as string
    byte[] RawData;          // Response as bytes
    long StatusCode;         // HTTP status code
    bool IsError;            // True if request failed
    string Error;            // Error message if failed
}
```

## 🎯 Example Use Cases

### API Integration
```csharp
// Authentication
var loginJson = JsonConvert.SerializeObject(new { email, password });
var token = await UFetch.Post<AuthToken>("https://api.com/login", loginJson);

// Authenticated request
var profile = await UFetch.Get<UserProfile>("https://api.com/profile",
    new UFetch.Options {
        Headers = { { "Authorization", $"Bearer {token.access_token}" } }
    });
```

### Image Download
```csharp
var response = await UFetch.Get("https://example.com/image.png");
Texture2D texture = new Texture2D(2, 2);
texture.LoadImage(response.RawData);
myImage.texture = texture;
```

### File Upload
```csharp
byte[] fileData = File.ReadAllBytes("photo.jpg");
var form = new WWWForm();
form.AddField("title", "My Photo");
form.AddBinaryData("file", fileData, "photo.jpg", "image/jpeg");

await UFetch.Upload("https://api.com/upload", form, new UFetch.Options {
    OnUploadProgress = p => uploadBar.value = p
});
```

## 🛠️ Development

### Project Structure

```
Assets/UFetch/              # UPM Package
├── Runtime/               # Runtime code
│   ├── UFetch.cs         # Main implementation
│   └── UFetch.Runtime.asmdef
├── Samples~/             # Optional samples (excluded from package)
│   └── Demo/
│       └── Demo.cs
├── package.json          # Package manifest
├── README.md            # Quick start
├── LICENSE.md           # MIT License
└── UFetch_Documentation.md  # Full docs
```

### Building from Source

1. Clone the repository
2. Open the project in Unity 2021.3+
3. The UFetch package is located in `Assets/UFetch/`

## 📄 License

MIT License - Free to use in personal and commercial projects.

See [LICENSE.md](Assets/UFetch/LICENSE.md) for details.

## 🤝 Contributing

Contributions, issues, and feature requests are welcome!

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 📞 Support

- 📖 [Full Documentation](Assets/UFetch/UFetch_Documentation.md)
- 🐛 [Issue Tracker](https://github.com/herbou/UFetch/issues)
- 💬 [Discussions](https://github.com/herbou/UFetch/discussions)

## ⭐ Show Your Support

If you find UFetch helpful, please consider:
- Giving it a ⭐ on GitHub
- Sharing it with other Unity developers
- Contributing improvements or reporting issues

---

Made with ❤️ for the Unity community
