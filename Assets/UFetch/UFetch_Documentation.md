# 📘 UFetch — Modern Async/Await Web Requests for Unity

A clean, modern, JavaScript-style fetch wrapper for Unity using async/await and UnityWebRequest.

## Features

✔️ `UFetch.Get / Post / Put / Delete`
✔️ Generic typed requests `UFetch.Get<T>()` and `Post<T>()`
✔️ JSON serialization via Newtonsoft.Json
✔️ Multipart upload via `Upload()` with WWWForm
✔️ Upload & Download progress events
✔️ Custom headers support
✔️ Error handling via exceptions or result objects
✔️ Access to raw response data (bytes) and text
✔️ Access to underlying UnityWebRequest for advanced use
✔️ Fully awaitable and works in Unity runtime

---

## 🚀 Installation

### 1. Install Newtonsoft.Json (Required)

Choose one of these options:

**Option A: Unity's Official Package (Recommended)**
1. Open Package Manager (Window → Package Manager)
2. Click **+** → **Add package by name**
3. Enter: `com.unity.nuget.newtonsoft-json`
4. Click **Add**

**Option B: jillejr's Package**
1. Package Manager → **+** → **Add package from git URL**
2. Enter: `https://github.com/jilleJr/Newtonsoft.Json-for-Unity.git#upm`

### 2. Install UFetch via Unity Package Manager

1. Open Package Manager (Window → Package Manager)
2. Click **+** → **Add package from git URL**
3. Enter: `https://github.com/herbou/UFetch.git?path=/Assets/UFetch`
4. Click **Add**

Done!

---

## 🔥 Quick Start

### Simple GET

```csharp
var response = await UFetch.Get("https://api.example.com/data");
Debug.Log(response.Text);
```

### Simple POST (JSON)

```csharp
var jsonBody = JsonConvert.SerializeObject(new { username = "Hamza", score = 10 });
var response = await UFetch.Post("https://api.example.com/user", jsonBody);
Debug.Log(response.Text);
```

---

## 📦 Typed Requests (`Get<T>`, `Post<T>`)

### GET with Type

```csharp
public class User {
    public string name;
    public int age;
}

User user = await UFetch.Get<User>("https://api.example.com/user/23");
Debug.Log(user.name);
```

### POST with Type

```csharp
var jsonBody = JsonConvert.SerializeObject(new { username = "hamza" });
var created = await UFetch.Post<User>("https://api.example.com/create", jsonBody);
```

---

## 📝 Custom Headers

```csharp
var response = await UFetch.Get("https://api.example.com/me", new UFetch.Options {
    Headers = {
        { "Authorization", "Bearer TOKEN" }
    }
});
```

---

## 📄 Multipart Upload (File, Image, Bytes)

```csharp
byte[] imageBytes = File.ReadAllBytes("image.png");

var form = new WWWForm();
form.AddField("userId", "123");
form.AddBinaryData("file", imageBytes, "image.png", "image/png");

var result = await UFetch.Upload("https://example.com/upload", form);
```

---

## 📤 Upload Progress

```csharp
var form = new WWWForm();
form.AddBinaryData("file", bytes, "photo.jpg", "image/jpeg");

await UFetch.Upload("https://example.com/upload", form, new UFetch.Options {
    OnUploadProgress = p => Debug.Log($"UPLOAD: {p * 100}%")
});
```

---

## 📥 Download Progress

```csharp
await UFetch.Get("https://largefile.com/video.mp4", new UFetch.Options {
    OnDownloadProgress = p => Debug.Log($"DOWNLOAD: {p * 100}%")
});
```

---

## 🛑 Error Handling

### Exception Style (Default)

```csharp
try {
    var response = await UFetch.Get("https://bad-url.com");
} catch (Exception ex) {
    Debug.LogError($"Request failed: {ex.Message}");
}
```

### Result Style (Disable Exceptions)

```csharp
var res = await UFetch.Get("https://bad-url.com", new UFetch.Options {
    ThrowOnError = false
});

if (res.IsError) {
    Debug.LogError($"Error [{res.StatusCode}]: {res.Error}");
}
```

---

## ➕ Advanced Usage

### Custom Method

```csharp
var jsonBody = JsonConvert.SerializeObject(new { nickname = "hamza" });
var options = new UFetch.Options { JsonBody = jsonBody };
await UFetch.Request("PATCH", "https://api.example.com/user/2", options);
```

### Raw Body

```csharp
var options = new UFetch.Options {
    BodyRaw = Encoding.UTF8.GetBytes("{ \"json\": true }")
};
await UFetch.Request("POST", "https://api.com/raw", options);
```

### Access UnityWebRequest

```csharp
var response = await UFetch.Get("https://api.com/data");
var contentType = response.UnityRequest.GetResponseHeader("Content-Type");
Debug.Log($"Content-Type: {contentType}");
```

---

## 🧠 Full API

### 📌 Static Methods

| Method | Signature | Description |
|--------|-----------|-------------|
| `Get` | `Task<UFetchResponse> Get(string url, Options options = null)` | GET request |
| `Get<T>` | `Task<T> Get<T>(string url, Options options = null)` | GET returning typed JSON |
| `Post` | `Task<UFetchResponse> Post(string url, string jsonBody, Options options = null)` | POST JSON string |
| `Post<T>` | `Task<T> Post<T>(string url, string jsonBody, Options options = null)` | POST JSON + typed response |
| `Put` | `Task<UFetchResponse> Put(string url, string jsonBody, Options options = null)` | PUT JSON string |
| `Delete` | `Task<UFetchResponse> Delete(string url, Options options = null)` | DELETE request |
| `Upload` | `Task<UFetchResponse> Upload(string url, WWWForm formData, Options options = null)` | Multipart form upload |
| `Request` | `Task<UFetchResponse> Request(string method, string url, Options options)` | Custom HTTP method |

### 📌 Options Class

```csharp
public class Options {
    public Dictionary<string, string> Headers = new();
    public byte[] BodyRaw = null;                // Raw byte body
    public string JsonBody = null;               // For JSON encoding
    public WWWForm FormData = null;              // For multipart/form-data
    public Action<float> OnDownloadProgress = null;
    public Action<float> OnUploadProgress = null;
    public bool ThrowOnError = true;             // Throw exception if HTTP error
}
```

### 📌 UFetchResponse Class

```csharp
public class UFetchResponse {
    public string Text;                  // Response body as string
    public byte[] RawData;              // Response body as bytes
    public long StatusCode;             // HTTP status code
    public bool IsError;                // True if request failed
    public string Error;                // Error message if failed
    public UnityWebRequest UnityRequest; // Access to underlying UnityWebRequest
}
```

---

## 📚 Usage Examples

### Example 1: Authentication Flow

```csharp
// Login
var loginData = new { email = "user@example.com", password = "pass123" };
var loginJson = JsonConvert.SerializeObject(loginData);
var authResponse = await UFetch.Post<AuthToken>("https://api.example.com/login", loginJson);

// Use token in subsequent requests
var userData = await UFetch.Get<UserProfile>("https://api.example.com/profile", new UFetch.Options {
    Headers = {
        { "Authorization", $"Bearer {authResponse.token}" }
    }
});
```

### Example 2: File Upload with Progress

```csharp
byte[] fileData = await File.ReadAllBytesAsync("document.pdf");

var form = new WWWForm();
form.AddField("title", "My Document");
form.AddField("category", "reports");
form.AddBinaryData("file", fileData, "document.pdf", "application/pdf");

await UFetch.Upload("https://api.example.com/documents", form, new UFetch.Options {
    OnUploadProgress = progress => {
        uploadProgressBar.value = progress;
        Debug.Log($"Uploading: {(progress * 100):F1}%");
    }
});
```

### Example 3: Error Handling with Retry

```csharp
int maxRetries = 3;
int retryCount = 0;

while (retryCount < maxRetries) {
    try {
        var data = await UFetch.Get<ApiData>("https://api.example.com/data");
        return data;
    } catch (Exception ex) {
        retryCount++;
        if (retryCount >= maxRetries) {
            Debug.LogError($"Failed after {maxRetries} attempts: {ex.Message}");
            throw;
        }
        await Task.Delay(1000 * retryCount); // Exponential backoff
    }
}
```

### Example 4: Using Raw Response Data (Download Image)

```csharp
var response = await UFetch.Get("https://api.example.com/image.png");

if (!response.IsError) {
    // Access binary data
    byte[] imageData = response.RawData;

    // Create texture from bytes (size will be replaced by LoadImage)
    Texture2D texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);

    if (texture.LoadImage(imageData)) {
        // Apply texture to a UI RawImage or material
        uiRawImage.texture = texture;
        Debug.Log($"Image loaded! Size: {texture.width}x{texture.height}");
    }
    else {
        Debug.LogError("Failed to load image");
    }
}
```

---

## 💡 Tips & Best Practices

1. **Always handle errors** — Network requests can fail for many reasons
2. **Use typed requests** when working with known API structures
3. **Pre-serialize JSON** — `Post()` and `Put()` accept JSON strings, not objects. Use `JsonConvert.SerializeObject()` first
4. **Use WWWForm for uploads** — Create a `WWWForm` object and pass it to `Upload()` for multipart uploads
5. **Use progress callbacks** for better user experience with large uploads/downloads
6. **Cache tokens** and reuse them across requests when possible
7. **Access RawData for binary content** — Use `response.RawData` for images, files, or other binary data
8. **ThrowOnError defaults to true** — Set `ThrowOnError = false` in Options if you want to handle errors manually

---

## 🔧 Requirements

- Unity 2021.3 or later (for async/await support)
- Newtonsoft.Json for Unity (install manually - see Installation section)
- .NET 4.x or .NET Standard 2.0+

---

## 📄 License

MIT License — Free to use in personal and commercial projects.

---

## 🤝 Contributing

Contributions, issues, and feature requests are welcome!

---

## ⭐ Support

If you find UFetch helpful, consider giving it a star and sharing it with other Unity developers!
