using UnityEngine;
using UnityEngine.UI;


public class Post {
    public int userId;
    public int id;
    public string title;
    public bool completed;

    public override string ToString() {
        return $"id:{id} / userid:{userId} / title:{title} / completed:{completed}";
    }
}
public class Demo : MonoBehaviour {
    public RawImage uiRawImage;
    public Slider uiSlider;



    private async void Start() {
        // Option 1: Use try-catch to handle exceptions
        try {
            var res = await UFetch.Get("https://jsonplaceholder.typicode.com/todos/1");
            Debug.Log($"Response: {res.Text}");
        }
        catch (System.Exception ex) {
            Debug.LogError($"Request failed: {ex.Message}");
        }

        Debug.Log("------------------------------------------------------------------------");

        // Option 2: Disable exceptions and check IsError manually
        var res2 = await UFetch.Get("https://jsonplaceholder.typicode.com/todos/1", new UFetch.Options {
            ThrowOnError = false
        });

        if (!res2.IsError)
            Debug.Log(res2.Text);
        else
            Debug.LogError($"Error {res2.StatusCode}: {res2.Error}");

        Debug.Log("------------------------------------------------------------------------");

        // Option 3: Generic typed request with try-catch
        try {
            var post = await UFetch.Get<Post>("https://jsonplaceholder.typicode.com/todos/1");
            Debug.Log(post.ToString());
        }
        catch (System.Exception ex) {
            Debug.LogError($"Failed to fetch post: {ex.Message}");
        }

        Debug.Log("------------------------------------------------------------------------");

        // Download image with progress tracking
        string imageURL = "https://images.unsplash.com/photo-1648465553030-0d756c5fd672?q=80&w=1171&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D";

        try {
            var res3 = await UFetch.Get(imageURL, new UFetch.Options {
                OnDownloadProgress = p => {
                    uiSlider.value = p;
                }
            });

            // Access binary data
            byte[] imageData = res3.RawData;

            Debug.Log($"Image data length: {imageData?.Length ?? 0} bytes");
            Debug.Log($"Image data is null: {imageData == null}");

            if (imageData == null || imageData.Length == 0) {
                Debug.LogError("No image data received!");
                return;
            }

            // Check the first few bytes to see if it looks like image data
            string header = "";
            for (int i = 0; i < Mathf.Min(16, imageData.Length); i++) {
                header += imageData[i].ToString("X2") + " ";
            }
            Debug.Log($"Image header bytes: {header}");

            // Create texture from bytes
            // Note: The size (2,2) will be replaced by LoadImage with actual image dimensions
            Texture2D texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);

            Debug.Log($"About to call LoadImage with {imageData.Length} bytes");
            bool loadSuccess = texture.LoadImage(imageData);
            Debug.Log($"LoadImage returned: {loadSuccess}");

            if (loadSuccess) {
                // Verify RawImage exists
                if (uiRawImage == null) {
                    Debug.LogError("uiRawImage is not assigned in the Inspector!");
                    return;
                }

                uiRawImage.texture = texture;

                // Image will automatically scale to fit the RawImage's RectTransform size
                // If you want to maintain aspect ratio, you can use:
                // uiRawImage.SetNativeSize(); // This sets RawImage size to match texture size

                Debug.Log($"Image loaded successfully! Size: {texture.width}x{texture.height}");
                Debug.Log($"Downloaded {imageData.Length} bytes, Status: {res3.StatusCode}");
                Debug.Log($"RawImage assigned: {uiRawImage.name}, Active: {uiRawImage.gameObject.activeInHierarchy}");
            }
            else {
                Debug.LogError("Failed to load image data into texture");
            }
        }
        catch (System.Exception ex) {
            Debug.LogError($"Image download failed: {ex.Message}");
        }
    }
}
