using UnityEngine;
using System.Collections;

public class ChildScript : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Water")
		{
			col.gameObject.GetComponent<Water>().HandlePlayer2Collision();
			GameObject.FindWithTag("Player2").GetComponent<Player2Control>().AddCount_ScoreCoin();
		}
	}
}
