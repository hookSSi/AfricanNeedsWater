using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Water : MonoBehaviour {

    public GameObject m_particle; // 물 파괴 효과
    public float m_speed;   // 속도
    public int distance;    // 스크린에서 어느 정도 멀어지면 파괴하는 가?

    private float m_Angle;  // 각도
    private Quaternion m_fireEulerAngle;    // 발사 오일러 각도
    private Rigidbody2D rigid; // Rigidbody2D
    public static int count = 0;
    private bool isOut;
    public static Queue<GameObject> WaterList = new Queue<GameObject>();   // 물리스트

    void Awake ()
    {
        count++;
        rigid = GetComponent<Rigidbody2D>();
        m_Angle = 0;
        isOut = false;
    }
	void Start()
    {
        Debug.Log(count);
    }
	void FixedUpdate ()
    {
        isOut = DestroyOutOfMap();

        if (!isOut)
        {
            Move();
        }   
        else
        {
            WaterList.Enqueue(this.gameObject);
            this.gameObject.SetActive(false);
        }

    }
    virtual protected void Move()
    {
        rigid.velocity = new Vector3(Mathf.Cos(m_Angle * Mathf.Deg2Rad), Mathf.Sin(m_Angle * Mathf.Deg2Rad)) * m_speed;
    }
    virtual protected bool DestroyOutOfMap()
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPosition.y > Screen.height + distance || screenPosition.y < -distance || screenPosition.x > Screen.width + distance || screenPosition.x < -distance)
        {   
            return true;
        }
        else
            return false;
    }
    virtual protected void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Player2")
        {
            Instantiate(m_particle, transform.position, transform.rotation);
            WaterList.Enqueue(this.gameObject);
            this.gameObject.SetActive(false);
        }
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
}
