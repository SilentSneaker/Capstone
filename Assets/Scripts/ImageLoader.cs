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

        MarsApiResponse roverAPIResponse = APIManager.GetImages();

        if(roverAPIResponse.photos.Length > 0) {

            Debug.Log("image array has something");

            for (int i = 0; i < roverAPIResponse.photos.Length; i++) {
                if(i > 4)
                {
                    break;
                }
                rawImage = imageGallery.transform.Find("Viewport/Content/Image " + (i + 1)).GetComponent<RawImage>();

                UnityWebRequest www = UnityWebRequestTexture.GetTexture(roverAPIResponse.photos[i].img_src);
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
            //roverAPIResponse.photos.
        }
        else
        {
            Debug.Log("image array is empty");
        }
    }
}
