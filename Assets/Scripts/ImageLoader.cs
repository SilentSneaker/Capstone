using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class ImageLoader : MonoBehaviour
{
    public RawImage rawImage;


    //For the Mars Rover Photos
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

    //Image Library Load Images
    public void LoadLibraryImages(GameObject imageGallery)
    {
        StartCoroutine(LoadLibraryImagesCoroutine(imageGallery));
    }


    private IEnumerator LoadLibraryImagesCoroutine(GameObject imageGallery)
    {

        NASAImageAPI APIManager = GameObject.Find("ObjectInfoUI").GetComponent<NASAImageAPI>();

        yield return APIManager.FetchImageData();

        List<string> imageUrls = APIManager.GetImages();

        if (imageUrls.Count > 0)
        {

            Debug.Log("image array has something");

            for (int i = 0; i < imageUrls.Count; i++)
            {
                if (i > 4)
                {
                    break;
                }
                rawImage = imageGallery.transform.Find("Viewport/Content/Image " + (i + 1)).GetComponent<RawImage>();

                UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageUrls[i]);
                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.Success)
                {
                    Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                    rawImage.texture = texture;
                }
                else
                {
                    Debug.LogError("Failed to load image. Error: " + www.error);
                    Debug.Log(imageUrls[i]);
                }
            }
        }
        else
        {
            Debug.Log("image array is empty");
        }
    }

    public void ClearImages(GameObject imageGallery)
    {
        ClearImageGallery(imageGallery);
    }


    private void ClearImageGallery(GameObject imageGallery)
    {
        for(int i = 0; i < 5; i++)
        {
            rawImage = imageGallery.transform.Find("Viewport/Content/Image " + (i + 1)).GetComponent<RawImage>();
            rawImage.texture = null;
        }
    }
}
