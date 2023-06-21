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
    private const string RoverEndpoint = "/rovers/{roverName}/photos";
    private const string APIKeyParam = "?api_key=";
    private const string APICamera = "&camera={camera}";
    public string APICamQuery;

    string APISolQuery = "&sol=";
    int APISolDate = 1878;
    int minSolDate = 0;
    int maxSolDate = 3970;

    public string RoverName = "curiosity"; // Replace with the rover name you want to access

    public RawImage rawImage;

    MarsApiResponse roverPhotos;

    public ImageData[] images;

    public void Start()
    {
        APICamQuery = "navcam";
        //StartCoroutine(FetchImages());
    }

    public IEnumerator<object> FetchImages()
    {
        //Set random sol date within parameters
        APISolDate = Random.Range(minSolDate, maxSolDate);

        string url = BaseURL + RoverEndpoint.Replace("{roverName}", RoverName) + APIKeyParam + APIKey + APISolQuery + APISolDate.ToString();// + APICamera.Replace("{camera}", APICamQuery);
        //string url = "https://api.nasa.gov/mars-photos/api/v1/rovers/curiosity/photos?api_key=V8DUyMT4oOSlQ7f2GxAXPMQZNmhcRwT3rlJHvpB3&sol=1878";
        Debug.Log(url); 
        UnityWebRequest www = UnityWebRequest.Get(url);

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            // Access the response data
            string response = www.downloadHandler.text;
            Debug.Log("API response: " + response);

            // Deserialize the JSON response
            roverPhotos = JsonUtility.FromJson<MarsApiResponse>(response);

            // Access the image data
            ImageData[] images = roverPhotos.photos;

            // Display the first image in the RawImage component
            if (images != null && images.Length > 0)
            {
                Debug.Log("Image array contains: " + images.Length);
            }
            else
            {
                Debug.LogWarning("No images found in the Mars Rover API response.");
                yield return StartCoroutine(FetchImages());
            }
            
        }
        else
        {
            Debug.LogError("Failed to fetch data from the Mars Rover API. Error: " + www.error);
        }
    }

    //Used to test if pictures are received from API
    //private IEnumerator<object> LoadImage(string url)
    //{
    //    UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
    //    yield return www.SendWebRequest();

    //    if (www.result == UnityWebRequest.Result.Success)
    //    {
    //        Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
    //        rawImage.texture = texture;
    //    }
    //    else
    //    {
    //        Debug.LogError("Failed to load image. Error: " + www.error);
    //    }
    //}

    public void SetRover(string rover)
    {
        RoverName = rover;
    }

    public void SetCamera(string camera)
    {
        APICamQuery = camera;
    }

    public MarsApiResponse GetImages()
    {
        return roverPhotos;
    }

    public void SetMaxSolDate(int date)
    {
        maxSolDate = date;
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
public class MarsApiResponse
{
    public ImageData[] photos;
}
