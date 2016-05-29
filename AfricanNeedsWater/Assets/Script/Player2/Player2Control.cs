using UnityEngine;
using System.Collections;

public class Player2Control : MonoBehaviour
{
	public Animator anim;
	public GameObject[] scoreCoin;
	public GameObject scoreBar;

	private int forwardValue = 1;
	private int count_ScoreCoin;
	private float speed = 200f;
	private float jumpHeight = 400f;
	private float animationSpeed = 1f;
	private bool isDash, isJumpping;

	// Use this for initialization
	void Start()
	{
		foreach (GameObject obj in scoreCoin) obj.SetActive(false);
		count_ScoreCoin = 0;
		anim.enabled = false;
		isJumpping = false;
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		Move();
		SetAnimationSpeed();
		AddCount_ScoreCoin();
		SetScoreCoin();
	}

	void Move()
	{
		if (Input.GetButton("Horizontal"))
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

		if (Input.GetButton("Dash"))
		{
			speed = 600;
			isDash = true;
		}

		if (Input.GetButton("Jump") && !isJumpping)
		{
			isJumpping = true;
			anim.SetTrigger("Jump");
			GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
		}

		if (forwardValue == 1) transform.eulerAngles = new Vector3(0, 0, 0);
		else transform.eulerAngles = new Vector3(0, 180, 0);

		scoreBar.transform.eulerAngles = new Vector3(0, 0, 0);
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
			anim.speed = 0;
			//anim.playbackTime = 0;
			//anim.StartPlayback();
			//anim.enabled = false;
			//anim.Play(anim.name, 1, anim.recorderStartTime);
			//Debug.Log(anim.name);

			//anim.enabled = false;
		}
	}

	void AddCount_ScoreCoin()
	{
		count_ScoreCoin++;
	}

	void SetScoreCoin()
	{
		if ((count_ScoreCoin / 5) % 5 == 0)	foreach (GameObject obj in scoreCoin) obj.SetActive(false);

		scoreCoin[(count_ScoreCoin / 5) % 5].SetActive(true);
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (isJumpping && col.gameObject.tag == "Ground")
		{
			isJumpping = false;
			anim.SetTrigger("Stand");
		}
	}
}
