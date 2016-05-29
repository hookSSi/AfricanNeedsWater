using UnityEngine;
using System.Collections;

public class CloudSpawn : MonoBehaviour
{
	public GameObject[] arr_Cloud;
	
	private float randomPosition;
	private float time = 0;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		time += 1 * Time.deltaTime;
		if (time > 2)
		{
			SpawnCloud();
			time = 0;
		}
	}

	void SpawnCloud()
	{
		float tmp = Random.Range(0f, 1f);

		int tmp_int = Random.Range(0, 8);
		if (tmp_int < 3)
		{
			GameObject obj = arr_Cloud[Random.Range(0, 2)];

			obj.GetComponent<AutoCloud>().SetSpeed();
			obj.GetComponent<AutoCloud>().SetScale();
			Instantiate(obj, new Vector3(969, Random.Range(420, 310), 5), Quaternion.identity);
		}
	}
}
