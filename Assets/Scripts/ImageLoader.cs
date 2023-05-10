using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class ImageLoader : MonoBehaviour
{
    public RawImage rawImage;

    public void LoadImage(GameObject imageGallery)
    {
        StartCoroutine(LoadImageCoroutine(imageGallery));
    }

    private IEnumerator LoadImageCoroutine(GameObject imageGallery)
    {

        RoverPicManager APIManager = GameObject.Find("ObjectInfoUI").GetComponent<RoverPicManager>();

        yield return APIManager.FetchImages();

        ImageData[] imageUrl = APIManager.GetImages();

        if(imageUrl.Length > 0) {

            Debug.Log("image array has something");

            for (int i = 0; i < imageUrl.Length; i++) {
                if(i > 4)
                {
                    break;
                }
                rawImage = imageGallery.transform.Find("Viewport/Content/Image " + (i + 1)).GetComponent<RawImage>();

                Debug.Log(rawImage);

                UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageUrl[i].img_src);
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
        else
        {
            Debug.Log("image array is empty");
        }
    }
}
