using UnityEngine;
using System.Collections;

public class Tutorial_Player1 : MonoBehaviour 
{

    public GameObject m_player;
    public SpriteRenderer m_render;
    public Sprite[] m_keyBoardimage;
    public Sprite[] m_controllerImage;

    private Vector3 player1_pos;
    private bool isController;
    private int step;
    private float t;

    private static bool m_isComplete = false;

    void Start()
    {
        Screen.SetResolution(1600, 900, true);

        m_isComplete = false;
        step = 0;
        t = 0;

        if (Input.GetJoystickNames().Length == 0)
            isController = false;
        else
            isController = true;
    }
	
	void Update () 
    {
        Tutorial();       
	}
    void Tutorial()
    {
        player1_pos = Camera.main.WorldToScreenPoint(m_player.transform.position);

        if (Input.GetAxis("Right_Horizontal_P1") != 0 || Input.GetAxis("Right_Vertical_P1") != 0)
            isController = true;
        else if (Input.GetJoystickNames().Length == 0 || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetMouseButtonDown(0))
            isController = false;

        switch (step)
        {
            case 0:
                if (isController)
                {
                    // 애니메이션 효과
                    t += Time.deltaTime;
                    if (Mathf.Sin(t * 10) > 0)
                        m_render.sprite = m_controllerImage[0];
                    else
                        m_render.sprite = m_controllerImage[0 + 1];

                    // 아이콘 위치 조정
                    transform.position = Camera.main.ScreenToWorldPoint(new Vector3(player1_pos.x, player1_pos.y + 150, 1));
                    
                    // 입력
                    if (Input.GetAxis("Left_Horizontal_P1") > 0)
                        step++;
                }
                else
                {
                    // 스프라이트 설정
                    m_render.sprite = m_keyBoardimage[step];

                    // 아이콘 위치 조정
                    transform.position = Camera.main.ScreenToWorldPoint(new Vector3(player1_pos.x + 80, player1_pos.y + 30, 1));

                    // 입력
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                        step++;
                }
                break;
            case 1:
                if (isController)
                {
                    // 애니메이션 효과
                    t += Time.deltaTime;
                    if (Mathf.Sin(t * 10) > 0)
                        m_render.sprite = m_controllerImage[2];
                    else
                        m_render.sprite = m_controllerImage[2 + 1];

                    // 아이콘 위치 조정
                    transform.position = Camera.main.ScreenToWorldPoint(new Vector3(player1_pos.x, player1_pos.y + 150, 1));

                    // 입력
                    if (Input.GetAxis("Left_Horizontal_P1") < 0)
                        step++;
                }
                else
                {
                    // 스프라이트 설정
                    m_render.sprite = m_keyBoardimage[step];

                    // 아이콘 위치 조정
                    transform.position = Camera.main.ScreenToWorldPoint(new Vector3(player1_pos.x - 80, player1_pos.y + 30, 1));

                    // 입력
                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                        step++;
                }
                break;
            case 2:
                if (isController)
                {
                    // 애니메이션 효과
                    t += Time.deltaTime;
                    if (Mathf.Sin(t * 3) > 0.5f)
                        m_render.sprite = m_controllerImage[4 + 1];
                    else if (Mathf.Sin(t * 3) > -0.5f)
                        m_render.sprite = m_controllerImage[4 + 2];     
                    else
                        m_render.sprite = m_controllerImage[4];  

                    // 아이콘 위치 조정
                    transform.position = Camera.main.ScreenToWorldPoint(new Vector3(player1_pos.x, player1_pos.y + 150, 1));

                    // 입력
                    if (Input.GetAxis("360_Trigger_P1") > 0)
                        step++;
                }
                else
                {
                    // 애니메이션 효과
                    t += Time.deltaTime;
                    if(Mathf.Sin(t * 10) > 0)
                        m_render.sprite = m_keyBoardimage[step];
                    else
                        m_render.sprite = m_keyBoardimage[step+1];

                    // 아이콘 위치 조정
                    transform.position = Camera.main.ScreenToWorldPoint(new Vector3(player1_pos.x, player1_pos.y + 150, 1));

                    // 입력
                    if (Input.GetMouseButtonDown(0))
                        step++;
                }
                break;
            default:
                Destroy(this.gameObject);
                m_isComplete = true;
                break;
        }
    }
}
