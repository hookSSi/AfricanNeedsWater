using UnityEngine;
using System.Collections;

public class Player2Control : MonoBehaviour
{
	public Animator anim;

	private int forwardValue = 1;
	private float speed = 200f;
	private float animationSpeed = 1f;

	// Use this for initialization
	void Start ()
	{
		anim.enabled = false;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		Move();
		SetAnimationSpeed();
	}

	void Move()
	{
		if(Input.GetButton("Horizontal"))
		{
			anim.enabled = true;
			forwardValue = Input.GetAxis("Horizontal") > 0 ? 1 : -1;
			GetComponent<Rigidbody2D>().velocity = new Vector2(forwardValue * speed, GetComponent<Rigidbody2D>().velocity.y);
		}

		if(forwardValue == 1) transform.eulerAngles = new Vector3(0, 0, 0);
		else transform.eulerAngles = new Vector3(0, 180, 0);
	}

	void SetAnimationSpeed()
	{
		if (300 - Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) < 299.9)
		{
			animationSpeed = Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) / speed;
			anim.speed = animationSpeed;
		}

		else
		{
			//anim.speed = 0;
			//anim.playbackTime = 0;
			//anim.StartPlayback();
			//anim.enabled = false;
			//anim.Play(anim.name, 1, anim.recorderStartTime);
			//Debug.Log(anim.name);
			
			anim.enabled = false;
		}

		
	}
}
