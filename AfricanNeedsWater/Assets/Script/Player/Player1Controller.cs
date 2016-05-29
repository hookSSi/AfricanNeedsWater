using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class Player1Controller : MonoBehaviour {

    public GameObject m_water; // 물
    public GameObject m_playerSprite; // 플레이어 이미지
    public Transform m_firePosition;    // 발사 위치
    public float m_speed;   // 속도
    public Animator m_playerAnimator; // 플레이어 애니메이션 콘트롤러

    private float m_fireAngle;  // 발사 각도
    private Quaternion m_fireEulerAngle;    // 발사 오일러 각도
    private Rigidbody2D rigid;  // RigidBody2D
    private Vector3 horizontal; // 수평 벡터
    private Water water; // 물 스폰용
    private Vector3 flip; // 스프라이트 방향 전환

    public GameObject m_waterGaugeBar; // 물 게이지 바
    private Vector3 MaxGaugeScale;
    private Vector3 waterGaugeScale;// 물 게이지 조절용
    private float m_curWaterGauge; // 현재 물 게이지
    public float m_waterRate; // 채워지는 비율
    public float rechargeTime; // 재충전 시간
    private float m_maxWaterGauge; // 최대 물 게이지
    private bool isOverLoad; // 과부하 상태
    private float time; // 시간 흐름
    public Material m_baseMaterial; // 기본 메터리얼
    public Material m_effectMaterial; // 과부하 효과 메터리얼
    private SpriteRenderer m_spriteRenderer;

    void Start ()
    {
        time = 0;
        isOverLoad = false;
        m_curWaterGauge = 100;
        m_maxWaterGauge = 100;
        rigid = GetComponent<Rigidbody2D>();
        horizontal = new Vector3(1, 0, 0);
        m_fireEulerAngle = Quaternion.identity;
        water = m_water.GetComponent<Water>();
        flip = m_playerSprite.transform.localScale;
        waterGaugeScale = m_waterGaugeBar.transform.localScale;
        MaxGaugeScale = waterGaugeScale;
        m_spriteRenderer = m_playerSprite.GetComponent<SpriteRenderer>();
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
            m_playerSprite.transform.localScale = flip;
            rigid.velocity = horizontal * m_speed;
        }
        else if (Input.GetAxis("Left_Horizontal_P1") < 0) // 왼쪽
        {
            flip.x = -Mathf.Abs(flip.x);
            m_playerSprite.transform.localScale = flip;
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
        if (Input.GetAxis("360_Trigger_P1") > 0 && m_curWaterGauge > 0 && !isOverLoad)
        {
            m_waterGaugeBar.SetActive(true);
            Fire();
        }
        /*   물 게이지 관련 처리   */
        else if(!isOverLoad)
        {
            m_waterGaugeBar.SetActive(false);
            if (m_curWaterGauge < m_maxWaterGauge)
                m_curWaterGauge += Time.deltaTime * m_waterRate;
            GamePad.SetVibration(0, 0, 0);
        }
        else if(isOverLoad)
        {
            Flash();
            m_waterGaugeBar.SetActive(true);
            time += Time.deltaTime;
            if(time > rechargeTime)
            {
                m_curWaterGauge += Time.deltaTime * m_waterRate;
            }    
            if(m_curWaterGauge == m_maxWaterGauge)
            {
                m_spriteRenderer.material = m_baseMaterial;
                time = 0;
                isOverLoad = false;
            }
        }
        GUIupdate();
    }
    void RotateFirePosition()
    {
        m_fireAngle = GetAngle(Input.GetAxis("Right_Horizontal_P1"), Input.GetAxis("Right_Vertical_P1"));
        m_fireEulerAngle.eulerAngles = new Vector3(0, 0, m_fireAngle);
        m_firePosition.rotation = Quaternion.Slerp(m_firePosition.rotation, m_fireEulerAngle, 10 * Time.deltaTime);
    }
    void Fire()
    {
        GamePad.SetVibration(0, 1, 1);  // 컨트롤러 진동
        if(m_curWaterGauge > 0)
            m_curWaterGauge -= Time.deltaTime * m_waterRate;
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
        if (m_curWaterGauge < 0)
        {
            GamePad.SetVibration(0, 0, 0);
            isOverLoad = true;
        }
    }
    protected virtual void OutOfMap() // 맵 밖으로 나갈시 실행하는 함수
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
    protected virtual void GUIupdate() // 플레이어 1의 UI 업데이트
    {
        waterGaugeScale.x = (MaxGaugeScale.x / 100) * m_curWaterGauge;
        m_waterGaugeBar.transform.localScale = waterGaugeScale;
    }
    protected virtual void Flash() // 과부하 효과
    {
        if (Mathf.Sin(Mathf.PI * 8 * time) > 0)
            m_spriteRenderer.material = m_effectMaterial;
        else
            m_spriteRenderer.material = m_baseMaterial;
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
    public float WaterRate
    {
        get
        {
            return m_waterRate;
        }
        set
        {
            m_waterRate = value;
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
