using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Water : MonoBehaviour {

    public GameObject m_particle; // 물 파괴 효과
    public float m_speed;   // 속도
    public int distance;    // 스크린에서 어느 정도 멀어지면 파괴하는 가?

    protected float m_Angle;  // 각도
    protected float m_dx;
    protected float m_dy;
    protected Quaternion m_fireEulerAngle;    // 발사 오일러 각도
    protected Rigidbody2D rigid; // Rigidbody2D
    public static int count = 0;
    protected bool isOut;
    public static Queue<GameObject> WaterList = new Queue<GameObject>();   // 물리스트

    void Awake ()
    {
        count++;
        rigid = GetComponent<Rigidbody2D>();
        m_Angle = 0;
        m_dx = 0;
        m_dy = 0;
        isOut = false;
    }
    void Start()
    {
        Clear();
    }
    void FixedUpdate ()
    {
        isOut = DestroyOutOfMap();

        if (!isOut)
        {
            Move();
        }   
        if(isOut)
        {
            WaterList.Enqueue(this.gameObject);
            this.gameObject.SetActive(false);
        }

    }
    protected virtual void Move()
    {
        rigid.velocity = new Vector3(m_dx, m_dy) * m_speed;
    }
    protected virtual bool DestroyOutOfMap()
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPosition.y > Screen.height + distance || screenPosition.y < -distance || screenPosition.x > Screen.width + distance || screenPosition.x < -distance)
        {   
            return true;
        }
        else
            return false;
    }
	protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Wall")
        {
            DestroyWater();
        }

		/*if (other.gameObject.tag == "Player2")
		{
			Instantiate(m_particle, transform.position, transform.rotation);
			WaterList.Enqueue(this.gameObject);
			other.gameObject.GetComponent<Player2Control>().AddCount_ScoreCoin();
			this.gameObject.SetActive(false);
		}*/
	}
    protected virtual void DestroyWater()
    {
        Instantiate(m_particle, transform.position, transform.rotation);
        WaterList.Enqueue(this.gameObject);
        this.gameObject.SetActive(false);
    }
    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }
	public void HandlePlayer2Collision()
	{
		Instantiate(m_particle, transform.position, transform.rotation);
		WaterList.Enqueue(this.gameObject);
		this.gameObject.SetActive(false);
	}
    public virtual void Clear()
    {
        m_dx = Mathf.Cos(m_Angle * Mathf.Deg2Rad);
        m_dy = Mathf.Sin(m_Angle * Mathf.Deg2Rad);
    }
    /*   Get,Set   */
    public float Speed
    {
        get
        {
            return m_speed;
        }
        set
        {
            m_speed = value;
        }
    }
    public float Angle
    {
        get
        {
            return m_Angle;
        }
        set
        {
            m_Angle = value;
        }
    }
    public Quaternion EulerAngle
    {
        get
        {
            return EulerAngle;
        }
        set
        {
            m_fireEulerAngle = value;
        }
    }
}
