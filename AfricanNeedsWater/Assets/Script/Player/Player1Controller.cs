using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class Player1Controller : MonoBehaviour {

    public GameObject m_water; // 물
    public Transform m_firePosition;    // 발사 위치
    public float m_speed;   // 속도
    public Animator m_playerAnimator; // 플레이어 애니메이션 콘트롤러

    private float m_fireAngle;  // 발사 각도
    private Quaternion m_fireEulerAngle;    // 발사 오일러 각도
    private Rigidbody2D rigid;  // RigidBody2D
    private Vector3 horizontal; // 수평 벡터
    private Water water; // 물 스폰용
    private Vector3 flip;

    void Start ()
    {
        rigid = GetComponent<Rigidbody2D>();
        horizontal = new Vector3(1, 0, 0);
        m_fireEulerAngle = Quaternion.identity;
        water = m_water.GetComponent<Water>();
        flip = transform.localScale;
    }
	
	void Update ()
    {
        /*   물뿌리개 회전   */
        if (Input.GetAxis("Right_Horizontal_P1") != 0 || Input.GetAxis("Right_Vertical_P1") != 0)
        {
            RotateFirePosition();
        }
        OutOfMap();
    }

    void FixedUpdate()
    {
        /*   플레이어 이동 처리   */
        if (Input.GetAxis("Left_Horizontal_P1") > 0) // 오른쪽
        {
            flip.x = Mathf.Abs(flip.x);
            transform.localScale = flip;
            rigid.velocity = horizontal * m_speed;
        }
        else if (Input.GetAxis("Left_Horizontal_P1") < 0) // 왼쪽
        {
            flip.x = -Mathf.Abs(flip.x);
            transform.localScale = flip;
            rigid.velocity = horizontal * -m_speed;
        }
        else
        {
            rigid.velocity = horizontal * 0;

            /*   랜덤으로 Stand 이벤트 발생   */
            int rand = 0;

            for (int i = 0; i < 100; i++)
            {
                rand += Random.Range(0, 10);
            }
            if (400 < rand  && rand  < 600)
            {
                m_playerAnimator.SetBool("IsStand", false);
            }
            else
            {
                m_playerAnimator.SetBool("IsStand", true);
            }
        }

        m_playerAnimator.SetFloat("Speed", Mathf.Abs(rigid.velocity.x));    // 걷기 애니메이션

        /*   발사 처리   */
        if (Input.GetAxis("360_Trigger_P1") > 0)
        {
            Fire();
        }
        else
        {
            GamePad.SetVibration(0, 0, 0);
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
        GamePad.SetVibration(0, 1, 1);
        if (Water.count > 100)
        {
            if(Water.WaterList.Count > 0)
            {
                Water forspawn;
                m_water = Water.WaterList.Dequeue();
                forspawn = m_water.GetComponent<Water>();

                m_water.transform.position = m_firePosition.position;
                m_water.transform.rotation = m_fireEulerAngle;
                forspawn.Angle = m_fireAngle;

                m_water.SetActive(true);
            }   
        }
        else
        {
            Water forspawn;
            forspawn = Instantiate(water, m_firePosition.position, m_fireEulerAngle) as Water;
            forspawn.Angle = m_fireAngle;
        }
    }
    virtual protected void OutOfMap()
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPosition.x > Screen.width + 10)
        {
            transform.position = new Vector3(-800, transform.position.y, 0);
        }
        else if(screenPosition.x < -10)
        {
            transform.position = new Vector3(800, transform.position.y, 0);
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
