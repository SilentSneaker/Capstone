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
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageUrl);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            rawImage.texture = texture;
        }
        else
        {
            Debug.LogError("Failed to load image. Error: " + www.error);
        }
    }
}
