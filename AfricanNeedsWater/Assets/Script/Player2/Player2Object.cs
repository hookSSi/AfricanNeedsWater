using UnityEngine;
using System.Collections;

public class Player2Object : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
	
	}

	/*void Move()
	{
		if (Input.GetButton("Horizontal") && GameManager.isPlaying)
		{
			anim.enabled = true;
			forwardValue = Input.GetAxis("Horizontal") > 0 ? 1 : -1;
			if (isDash)
			{
				GetComponent<Rigidbody2D>().velocity = new Vector2(forwardValue * speed, GetComponent<Rigidbody2D>().velocity.y);
				speed = 300;
				isDash = false;
			}

			else GetComponent<Rigidbody2D>().velocity = new Vector2(forwardValue * speed, GetComponent<Rigidbody2D>().velocity.y);
		}

		if (Input.GetButton("Dash") && GameManager.isPlaying)
		{
			speed = 600;
			isDash = true;
		}

		if (Input.GetButton("Jump") && !isJumpping && GameManager.isPlaying)
		{
			isJumpping = true;
			anim.SetTrigger("Jump");
			GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
		}

		if (forwardValue == 1) transform.eulerAngles = new Vector3(0, 0, 0);
		else transform.eulerAngles = new Vector3(0, 180, 0);

		scoreBar.transform.eulerAngles = new Vector3(0, 0, 0);
	}*/
}
