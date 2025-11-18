using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public static class UFetch {
    // =====================================================================
    // OPTIONS
    // =====================================================================
    public class Options {
        public Dictionary<string, string> Headers = new();
        public byte[] BodyRaw = null;               // Raw JSON body
        public string JsonBody = null;              // For JSON encoding
        public WWWForm FormData = null;             // For multipart/form-data

        // Progress callbacks
        public Action<float> OnDownloadProgress = null;
        public Action<float> OnUploadProgress = null;

        public bool ThrowOnError = true;            // Throw exception if HTTP error
    }

    // =====================================================================
    // PUBLIC API � Shortcuts
    // =====================================================================

    public static Task<UFetchResponse> Get(string url, Options options = null)
        => Request("GET", url, options);

    public static Task<UFetchResponse> Delete(string url, Options options = null)
        => Request("DELETE", url, options);

    public static Task<UFetchResponse> Post(string url, string jsonBody, Options options = null) {
        options ??= new Options();
        options.JsonBody = jsonBody;
        return Request("POST", url, options);
    }

    public static Task<UFetchResponse> Put(string url, string jsonBody, Options options = null) {
        options ??= new Options();
        options.JsonBody = jsonBody;
        return Request("PUT", url, options);
    }

    // Multipart upload:
    public static Task<UFetchResponse> Upload(string url, WWWForm formData, Options options = null) {
        options ??= new Options();
        options.FormData = formData;
        return Request("POST", url, options);
    }

    // GENERIC TYPED REQUESTS
    public static async Task<T> Get<T>(string url, Options options = null) {
        var res = await Get(url, options);
        return JsonConvert.DeserializeObject<T>(res.Text);
    }

    public static async Task<T> Post<T>(string url, string jsonBody, Options options = null) {
        var res = await Post(url, jsonBody, options);
        return JsonConvert.DeserializeObject<T>(res.Text);
    }

    // =====================================================================
    // CORE REQUEST
    // =====================================================================

    public class UFetchResponse {
        public string Text;
        public byte[] RawData;
        public long StatusCode;
        public bool IsError;
        public string Error;
        public UnityWebRequest UnityRequest;
    }

    public static async Task<UFetchResponse> Request(string method, string url, Options options) {
        options ??= new Options();

        UnityWebRequest req;

        // Use Unity helper methods for GET/DELETE, manual construction for others
        if (method == "GET") {
            req = UnityWebRequest.Get(url);
        }
        else if (method == "DELETE") {
            req = UnityWebRequest.Delete(url);
        }
        else {
            req = new UnityWebRequest(url, method);
            req.downloadHandler = new DownloadHandlerBuffer();

            // Upload handler for POST/PUT/PATCH
            if (options.FormData != null) {
                req.uploadHandler = new UploadHandlerRaw(options.FormData.data);
            }
            else if (!string.IsNullOrEmpty(options.JsonBody)) {
                var bytes = Encoding.UTF8.GetBytes(options.JsonBody);
                req.uploadHandler = new UploadHandlerRaw(bytes);
                req.SetRequestHeader("Content-Type", "application/json");
            }
            else if (options.BodyRaw != null) {
                req.uploadHandler = new UploadHandlerRaw(options.BodyRaw);
            }
        }

        // Custom headers
        if (options.Headers != null) {
            foreach (var h in options.Headers)
                req.SetRequestHeader(h.Key, h.Value);
        }

        // Send async
        var operation = req.SendWebRequest();

        // Initialize progress to 0% at the start
        options.OnDownloadProgress?.Invoke(0f);
        options.OnUploadProgress?.Invoke(0f);

        // Progress monitoring loop
        while (!operation.isDone) {
            options.OnDownloadProgress?.Invoke(req.downloadProgress);
            options.OnUploadProgress?.Invoke(req.uploadProgress);
            await Task.Yield();
        }

        // Ensure final progress is set to 100% when complete
        options.OnDownloadProgress?.Invoke(1.0f);
        options.OnUploadProgress?.Invoke(1.0f);

        // Build response - extract data before disposing
        var response = new UFetchResponse {
            Text = req.downloadHandler?.text,
            RawData = req.downloadHandler?.data,
            StatusCode = req.responseCode,
            IsError = req.result != UnityWebRequest.Result.Success,
            Error = req.error,
            UnityRequest = req
        };

        // Error handling
        if (response.IsError && options.ThrowOnError) {
            req.Dispose();
            throw new Exception($"UFetch Error [{response.StatusCode}]: {response.Error}\nURL: {url}");
        }

        // Note: UnityWebRequest is NOT disposed here because response.UnityRequest holds a reference
        // Users should manually dispose if needed: response.UnityRequest.Dispose()
        // The data (Text, RawData) is already copied, so it's safe to use after disposal

        return response;
    }
}
