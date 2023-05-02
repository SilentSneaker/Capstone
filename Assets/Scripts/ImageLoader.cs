using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class ImageLoader : MonoBehaviour
{
    public RawImage rawImage;

    public void LoadImage(string imageUrl)
    {
        StartCoroutine(LoadImageCoroutine(imageUrl));
    }

    private IEnumerator LoadImageCoroutine(string imageUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageUrl);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            rawImage.texture = texture;
            rawImage.SetNativeSize();
        }
        else
        {
            Debug.LogError($"Failed to load image: {request.error}");
        }
    }
}
