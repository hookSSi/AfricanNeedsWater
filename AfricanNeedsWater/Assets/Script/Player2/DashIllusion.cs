using UnityEngine;
using System.Collections;

public class DashIllusion : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		this.GetComponent<Renderer>().material.color -= new Color(0, 0, 0, 0.03f);
		if (this.GetComponent<Renderer>().material.color.a <= 0) Destroy(this.gameObject);
	}
}
