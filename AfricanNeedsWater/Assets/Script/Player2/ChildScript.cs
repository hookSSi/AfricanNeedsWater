using UnityEngine;
using System.Collections;

public class ChildScript : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		transform.localPosition = new Vector3(0.744f, -0.1339998f, 0);
		transform.localEulerAngles = new Vector3(0, 0, 0);
	}

	protected void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Water")
		{
			col.gameObject.GetComponent<Water>().HandlePlayer2Collision();
			GameObject.FindWithTag("Player2").GetComponent<Player2Control>().AddCount_ScoreCoin();
		}
	}
}
