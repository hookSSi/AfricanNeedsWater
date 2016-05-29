using UnityEngine;
using System.Collections;

public class Player1Controller : MonoBehaviour {

    public GameObject m_water; // 물
    public Transform m_firePosition;    // 발사 위치
    public float m_speed;   // 속도

    private float m_fireAngle;  // 발사 각도
    private Quaternion m_fireEulerAngle;    // 발사 오일러 각도
    private Rigidbody2D rigid;  // RigidBody2D
    private Vector3 horizontal; // 수평 벡터
    private Water water; // 물 스폰용 

    void Start ()
    {
        rigid = GetComponent<Rigidbody2D>();
        horizontal = new Vector3(1, 0, 0);
        m_fireEulerAngle = Quaternion.identity;
        water = m_water.GetComponent<Water>();
    }
	
	void Update ()
    {
        /*   물뿌리개 회전   */
        if (Input.GetAxis("Right_Horizontal_P1") != 0 || Input.GetAxis("Right_Vertical_P1") != 0)
        {
            RotateFirePosition();
        }   
    }

    void FixedUpdate()
    {
        /*   플레이어 이동 처리   */
        if(Input.GetAxis("Left_Horizontal_P1") > 0)
        {
            rigid.velocity = horizontal * m_speed;
        }
        else if(Input.GetAxis("Left_Horizontal_P1") < 0)
        {
            rigid.velocity = horizontal * -m_speed;
        }
        else
        {
            rigid.velocity = horizontal * 0;
        }
        /*   발사 처리   */
        if(Input.GetAxis("360_Trigger_P1") > 0)
        {
            Fire();
        }
    }

    void RotateFirePosition()
    {
        m_fireAngle = GetAngle(Input.GetAxis("Right_Horizontal_P1"), Input.GetAxis("Right_Vertical_P1"));
        m_fireEulerAngle.eulerAngles = new Vector3(0, 0, m_fireAngle);
        m_firePosition.rotation = Quaternion.Slerp(m_firePosition.rotation, m_fireEulerAngle, 10 * Time.deltaTime);
    }
    void Fire()
    {
        if(Water.WaterList != null)
        {
            if(Water.WaterList.Count > 20)
            {
                GameObject forspawn = Water.WaterList.Dequeue();
                water = Water.WaterList.Dequeue().GetComponent<Water>();
                forspawn.transform.position = m_firePosition.position;
                forspawn.transform.rotation = m_fireEulerAngle;
                water.Angle = m_fireAngle;
                forspawn.SetActive(true);
                water = m_water.GetComponent<Water>();
            } 
        }
        else
        {
            Water forspawn;
            forspawn = Instantiate(water, m_firePosition.position, m_fireEulerAngle) as Water;
            forspawn.Angle = m_fireAngle;
        }
    }
    /*     Get,Set   */
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
    public float FireAngle
    {
        get
        {
            return m_fireAngle;
        }
        set
        {
            m_fireAngle = value;
        }
    }

    /*   기타 함수들   */
    float GetAngle(float x, float y)
    {
        float angle = 0;

        angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;

        return angle;
    }
}
