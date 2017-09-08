using UnityEngine;
using System.Collections;

public class SpawnGameObjects : MonoBehaviour {
    public GameObject spawnSurface;

    public GameObject spawnPrefab;

    public float minSecondsBetweenSpawning = 3.0f;
	public float maxSecondsBetweenSpawning = 6.0f;
	
	public Transform chaseTarget;
	
	private float savedTime;
	private float secondsBetweenSpawning;

	// Use this for initialization
	void Start () {
		savedTime = Time.time;
		secondsBetweenSpawning = Random.Range (minSecondsBetweenSpawning, maxSecondsBetweenSpawning);
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - savedTime >= secondsBetweenSpawning) // is it time to spawn again?
		{
			MakeThingToSpawn();
			savedTime = Time.time; // store for next spawn
			secondsBetweenSpawning = Random.Range (minSecondsBetweenSpawning, maxSecondsBetweenSpawning);
		}	
	}

	void MakeThingToSpawn()
	{

        // create a new gameObject
        GameObject clone = null;

        if (spawnSurface != null)
        {
            float xArea = spawnSurface.GetComponent<Renderer>().bounds.size.x;
            float zArea = spawnSurface.GetComponent<Renderer>().bounds.size.z;

            float xSpawn = Random.Range(spawnSurface.transform.position.x - xArea / 2, spawnSurface.transform.position.x + xArea / 2);
            float zSpawn = Random.Range(spawnSurface.transform.position.z - zArea / 2, spawnSurface.transform.position.z + zArea / 2);

            Vector3 newTransform = new Vector3(xSpawn, transform.position.y, zSpawn);

            clone = Instantiate(spawnPrefab, newTransform, transform.rotation) as GameObject;
        } else {
            clone = Instantiate(spawnPrefab, transform.position, transform.rotation) as GameObject;
        }

		// set chaseTarget if specified
		if ((chaseTarget != null) && (clone.gameObject.GetComponent<Chaser> () != null))
		{
			clone.gameObject.GetComponent<Chaser>().SetTarget(chaseTarget);
		}
	}
}
