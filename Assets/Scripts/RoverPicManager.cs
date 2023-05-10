using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine.UI;

public class RoverPicManager : MonoBehaviour
{
    private const string APIKey = "V8DUyMT4oOSlQ7f2GxAXPMQZNmhcRwT3rlJHvpB3";

    private const string BaseURL = "https://api.nasa.gov/mars-photos/api/v1";
    private const string RoverEndpoint = "/rovers/{roverName}/latest_photos";
    private const string APIKeyParam = "?api_key=";
    private const string APICamera = "&camera=navcam";

    public string RoverName = "curiosity"; // Replace with the rover name you want to access

    public RawImage rawImage;

    public ImageData[] images;

    public void Start()
    {
        StartCoroutine(FetchImages());
    }

    private IEnumerator<object> FetchImages()
    {
        string url = BaseURL + RoverEndpoint.Replace("{roverName}", RoverName) + APIKeyParam + APIKey;
        //string url = "https://api.nasa.gov/mars-photos/api/v1/rovers/curiosity/latest_photos?api_key=V8DUyMT4oOSlQ7f2GxAXPMQZNmhcRwT3rlJHvpB3";
        Debug.Log(url); 
        UnityWebRequest www = UnityWebRequest.Get(url);

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            // Access the response data
            string response = www.downloadHandler.text;
            Debug.Log("API response: " + response);
            // Parse the JSON response
            string json = www.downloadHandler.text;
            //Debug.Log(json);
            //MarsRoverData roverData = JsonUtility.FromJson<MarsRoverData>(json);
            //Debug.Log(roverData.photos);

            // Parse JSON response into wrapper object
            ImageDataWrapper wrapper = JsonUtility.FromJson<ImageDataWrapper>(json);

            // Extract image data from the wrapper
            images = wrapper.latest_photos;

            // Display the first image in the RawImage component
            if (images != null && images.Length > 0)
            {
                //string imageUrl = images[0].img_src;
                //StartCoroutine(LoadImage(imageUrl));
            }
            else
            {
                Debug.LogWarning("No images found in the Mars Rover API response.");
            }
            // Pass the MarsRoverData object to the display script
            //DisplayScript displayScript = GetComponent<DisplayScript>();
            //displayScript.DisplayImages(roverData);
        }
        else
        {
            Debug.LogError("Failed to fetch data from the Mars Rover API. Error: " + www.error);
        }
    }
    private IEnumerator<object> LoadImage(string url)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
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

    public void SetRover(string rover)
    {
        RoverName = rover;
    }
}

[System.Serializable]
public class ImageData
{
    public string id;
    public int sol;
    public CameraData camera;
    public string img_src;
    public string earth_date;
    public RoverData rover;
}

[System.Serializable]
public class CameraData
{
    public int id;
    public string name;
    public int rover_id;
    public string full_name;
}

[System.Serializable]
public class RoverData
{
    public int id;
    public string name;
    public string landing_date;
    public string launch_date;
    public string status;
}

[System.Serializable]
public class ImageDataWrapper
{
    public ImageData[] latest_photos;
}
