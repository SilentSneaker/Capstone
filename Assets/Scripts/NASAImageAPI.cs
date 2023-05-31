using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NASAImageAPI : MonoBehaviour
{

    public string apiEndpoint = "https://images-api.nasa.gov/search";
    //public string apiKey = "V8DUyMT4oOSlQ7f2GxAXPMQZNmhcRwT3rlJHvpB3";
    public string searchQuery = "main_sequence_star";
    private Texture2D retrievedImage;
    public RawImage rawImage;
    List<string> imageUrls = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
       StartCoroutine(FetchImageData());
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void SetSearchQuery(string obj)
    {
        searchQuery = obj;
    }

    public IEnumerator FetchImageData()
    {

        string requestUrl = $"https://images-api.nasa.gov/search?q={searchQuery}%&media_type=image&description=images";
        //string url = BaseURL + RoverEndpoint.Replace("{roverName}", RoverName) + APIKeyParam + APIKey;

        UnityWebRequest webRequest = UnityWebRequest.Get(requestUrl);

        yield return webRequest.SendWebRequest();


        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            // Handle the response and parse the image data
            //ParseImageData(webRequest.downloadHandler.text);

            // Access the response data
            string response = webRequest.downloadHandler.text;

            Debug.Log(searchQuery);

            // Parse JSON response into wrapper object
            ImageDataWrapper wrapper = JsonUtility.FromJson<ImageDataWrapper>(response);

            // Save images from response into an array that will be returned

            if (wrapper.collection.items.Count > 0)
            {
                //StartCoroutine(LoadImage(wrapper.collection.items[0].links[0].href));
                Debug.Log("Number of images retreived: " + wrapper.collection.items.Count);
                for (int i = 0; i < wrapper.collection.items.Count; i++)
                {
                    imageUrls.Add(wrapper.collection.items[i].links[0].href);
                }
            }
            else
            {
                Debug.Log("Did not get any images");
            }
            //Debug.Log("Number of links: " + wrapper.collection.items.Count);
            //StartCoroutine(LoadImage(imageUrl));
            
        }
        else
        {
            Debug.Log(searchQuery);
            Debug.LogError($"Failed to fetch image data. Error: {webRequest.error}");
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

    public List<string> GetImages()
    {
        return imageUrls;
    }

    public void DeleteImages()
    {
        imageUrls.Clear();
    }
    private void ParseImageData(string responseData)
    {
        // Parse the JSON response and extract relevant image information
        // Update the retrievedImage variable with the desired image data
    }

    [System.Serializable]
    public class ImageDataWrapper
    {
        public Collection collection;
    }

    [System.Serializable]
    public class Collection
    {
        public List<Item> items;
    }

    [System.Serializable]
    public class Item
    {
        public List<Data> data;
        public List<Link> links;
    }

    [System.Serializable]
    public class Data
    {
        public string media_type;
        public string nasa_id;
        public string title;
    }

    [System.Serializable]
    public class Link
    {
        public string href;
    }


}


