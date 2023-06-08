using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BeltSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject asteroidPrefab0;
    public GameObject asteroidPrefab1;
    public GameObject asteroidPrefab2;
    public GameObject asteroidPrefab3;
    public int asteroidDensity;
    public int seed;
    public float innerbelt;
    public float outerbelt;
    public float height;
    public bool rotatingClockwise;
    private List<GameObject> asteroidArray;




    [Header("Asteroid Settings")]
    public float minSpeed;
    public float maxSpeed;
    public float minRotation;
    public float maxRotation;


    private Vector3 localPosition;
    private Vector3 worldOffset;
    private Vector3 worldPosition;
    private float randomRadius;
    private float randomRadian;
    private float x,y,z;


    // Start is called before the first frame update
    void Start()
    {
        
        asteroidArray = new List<GameObject>(){
            asteroidPrefab0,
            asteroidPrefab1,
            asteroidPrefab2,
            asteroidPrefab3,
        };
        asteroidArray.Add(asteroidPrefab2);
        asteroidArray.Add(asteroidPrefab3);
        Random.InitState(seed);
        for (int i = 0; i < asteroidDensity; i++)
        {
            do
            {
                randomRadius = Random.Range(innerbelt, outerbelt);
                randomRadian = Random.Range(0,(2*Mathf.PI));
                y = Random.Range(-(height/2), (height/2));
                x = randomRadius * Mathf.Cos(randomRadian);
                z = randomRadius * Mathf.Sin(randomRadian);
            } while (float.IsNaN(z)&& float.IsNaN(x));

            localPosition = new Vector3(x,y,z);
            worldOffset = transform.rotation * localPosition;
            worldPosition = transform.position + worldOffset;
            int prefab = Random.Range(0,4);
            GameObject asteriod = Instantiate(asteroidArray[prefab], worldPosition, Quaternion.Euler(Random.Range(0,360),Random.Range(0,360),Random.Range(0,360)));
            UnityEngine.Debug.Log(prefab);
            asteriod.AddComponent<BeltAsteroid>().SetupAsteriod(Random.Range(minSpeed, maxSpeed),Random.Range(minRotation,maxRotation), gameObject, rotatingClockwise);
            asteriod.transform.SetParent(transform);
        }
    }
}
