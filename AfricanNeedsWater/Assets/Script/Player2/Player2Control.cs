using UnityEngine;
using System.Collections;

public class Player2Control : MonoBehaviour
{
	public Animator anim;
	public GameObject[] scoreCoin;
	public GameObject scoreBar;
	public GameObject obj_Child;

	private int forwardValue = 1;
	private int count_ScoreCoin;
	private float speed = 200f;
	private float jumpHeight = 400f;
	private float animationSpeed = 1f;
	private bool isDash, isJumpping;
	private bool isChanging;

	// Use this for initialization
	void Start()
	{
		foreach (GameObject obj in scoreCoin) obj.SetActive(false);
		count_ScoreCoin = 0;
		anim.enabled = false;
		isJumpping = false;
		isChanging = false;
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		Move();
		SetAnimationSpeed();
		SetScoreCoin();
	}

	void Move()
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

			//////////////////////////////////////////////////////////////////////////////////////////////////////////////
			//this.GetComponent<BoxCollider2D>().enabled = false;
			//obj_Child.GetComponent<BoxCollider2D>().enabled = false;
			//transform.position = new Vector3(transform.position.x, transform.position.y, -5);
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

	public void AddCount_ScoreCoin()
	{
		count_ScoreCoin++;
	}

	void SetScoreCoin()
	{
		if (count_ScoreCoin == 25 && !isChanging) isChanging = true;

		if (isChanging)
		{
			foreach (GameObject obj in scoreCoin) obj.SetActive(false);
			GameObject.FindWithTag("GameManager").GetComponent<GameManager>().AddTextureCrop();

			count_ScoreCoin = 0;
			isChanging = false;
		}

		if (count_ScoreCoin > 0) scoreCoin[(count_ScoreCoin / 5) % 5].SetActive(true);
	}
	
	protected void OnCollisionEnter2D(Collision2D col)
	{
		if (isJumpping && col.gameObject.tag == "Ground")
		{
			isJumpping = false;
			anim.SetTrigger("Stand");
		}

		if (col.gameObject.tag == "Water")
		{
			col.gameObject.GetComponent<Water>().HandlePlayer2Collision();
			GameManager.isPlaying = false;
			anim.SetTrigger("Die");

			transform.eulerAngles = new Vector3(0, 180, 20);
			transform.position = new Vector3(transform.position.x, transform.position.y, -5);
			GetComponent<Rigidbody2D>().velocity = new Vector2(-forwardValue * speed, jumpHeight);
			this.GetComponent<BoxCollider2D>().enabled = false;
			obj_Child.GetComponent<BoxCollider2D>().enabled = false;
		}
	}
}
