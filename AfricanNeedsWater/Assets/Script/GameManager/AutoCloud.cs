using UnityEngine;
using System.Collections;

public class AutoCloud : MonoBehaviour
{
	public float speed;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update()
	{
		if (this.transform.position.x < -900) Destroy(this.gameObject);
	}

	void FixedUpdate ()
	{
		GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
	}

	public void SetSpeed()
	{
		speed = Random.Range(-120, -20);
	}

	public void SetScale()
	{
		transform.localScale = new Vector3(Random.Range(50, 100), Random.Range(30, 100), 1);
	}
}
