using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player2Control : MonoBehaviour
{
	public Animator anim;
	public GameObject[] scoreCoin;
	public GameObject[] dashSprite;
	public GameObject dashJumpSprite;
	public GameObject scoreBar;
	public GameObject obj_Child;
	public Image gauge;

	private int forwardValue = 1;
	private int count_ScoreCoin;
	private float time;
	private float speed = 200f;
	private float jumpHeight = 400f;
	private float animationSpeed = 1f;
	private float gauge_MIN = 3.51f;
	private float gauge_MAX = 4f;
	private float value_Dash = 0.01f;
	private bool isDash, isJumpping;
	private bool overload;
	private bool fever;
	private string[] sprite = { "P2_1", "P2_2", "P2_3", "P2_4", "P2_5", "P2_6", "P2_7", "P2_8", "P2_9", "P2_10" };

	// Use this for initialization
	void Start()
	{
		foreach (GameObject obj in scoreCoin) obj.SetActive(false);
		time = 0;
		count_ScoreCoin = 0;
		anim.enabled = false;
		isJumpping = false;
		overload = false;
		fever = false;
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		Move();
		CheckGauge();

		if (overload) Overload();
		if (isDash) DashIllusion();

		SetAnimationSpeed();
		SetScoreCoin();

		//Debug.Log(anim.GetComponent<SpriteRenderer>().sprite.name);
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

		if (Input.GetButton("Dash") && GameManager.isPlaying && !overload)
		{
			speed = 600;
			isDash = true;
			fever = true;
		}

		if (Input.GetButtonUp("Dash"))
		{
			fever = false;
			isDash = false;
			speed = 300;
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
		//if (count_ScoreCoin == 25 && !isChanging) isChanging = true;
		if (count_ScoreCoin == 5)
		{
			GameObject.FindWithTag("GameManager").GetComponent<GameManager>().AddTextureCrop();
			count_ScoreCoin = 0;
		}

		/*if (isChanging)
		{
			//foreach (GameObject obj in scoreCoin) obj.SetActive(false);
			GameObject.FindWithTag("GameManager").GetComponent<GameManager>().AddTextureCrop();

			count_ScoreCoin = 0;
			isChanging = false;
		}*/

		//if (count_ScoreCoin > 0) scoreCoin[(count_ScoreCoin / 5) % 5].SetActive(true);
	}

	void CheckGauge()
	{
		if (isDash) gauge.fillAmount -= value_Dash;
		else gauge.fillAmount += value_Dash * 0.5f;

		if (gauge.fillAmount == 0)
		{
			gauge.color = new Color(1, 0, 0, 1);
			overload = true;
			fever = false;
		}

		gauge.transform.position = new Vector3(transform.position.x, transform.position.y + 75, transform.position.z);
	}

	void Overload()
	{
		time += Time.deltaTime;
		gauge.color += new Color(0, 1f / 255, 1f / 255, 0);

		if (time >= 3)
		{
			time = 0;
			gauge.color = new Color(1, 1, 1, 1);
			overload = false;
		}
	}

	void DashIllusion()
	{
		for (int i = 0; i < 10; i++)
		{
			if(anim.GetComponent<SpriteRenderer>().sprite.name == sprite[i])
				Instantiate(dashSprite[i], this.transform.position, this.transform.rotation);
		}

		if (isJumpping) Instantiate(dashJumpSprite, this.transform.position, this.transform.rotation);
	}

	protected void OnTriggerEnter2D(Collider2D col)
	{
		if (isJumpping && col.gameObject.tag == "Ground")
		{
			isJumpping = false;
			anim.SetTrigger("Stand");
		}

		if (col.gameObject.tag == "Water")
		{
			if (!fever)
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

			else
			{
				col.gameObject.GetComponent<Water>().HandlePlayer2Collision();
				AddCount_ScoreCoin();
			}
		}
	}
}
