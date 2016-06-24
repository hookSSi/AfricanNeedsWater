using UnityEngine;
using System.Collections;

public class Tutorial_Player2 : MonoBehaviour
{
    public GameObject m_player;
    public SpriteRenderer m_render;
    public Sprite[] m_keyBoardimage;
    public Sprite[] m_controllerImage;

    private Vector3 player2_pos;
    private bool isController;
    private int step;
    private float t;

    private static bool m_isComplete = false;

    void Start()
    {
        GameManager.isPlaying = true;
        m_isComplete = false;
        step = 0;
        t = 0;
    }
    void Update()
    {
        Tutorial();
    }
    void Tutorial()
    {
        player2_pos = Camera.main.WorldToScreenPoint(m_player.transform.position);

        switch (step)
        {
                // 오른쪽
            case 0:
                // 스프라이트 설정
                m_render.sprite = m_keyBoardimage[step];

                // 아이콘 위치 조정
                transform.position = Camera.main.ScreenToWorldPoint(new Vector3(player2_pos.x, player2_pos.y + 150, 1));

                // 입력
                if (Input.GetKeyDown(KeyCode.D))
                    step++;
                break;
                // 왼쪽
            case 1:
                // 스프라이트 설정
                m_render.sprite = m_keyBoardimage[step];

                // 아이콘 위치 조정
                transform.position = Camera.main.ScreenToWorldPoint(new Vector3(player2_pos.x, player2_pos.y + 150, 1));

                // 입력
                if (Input.GetKeyDown(KeyCode.A))
                    step++;
                break;
                // 점프
            case 2:
                // 스프라이트 설정
                m_render.sprite = m_keyBoardimage[step];

                // 아이콘 위치 조정
                transform.position = Camera.main.ScreenToWorldPoint(new Vector3(player2_pos.x, player2_pos.y + 150, 1));

                // 입력
                if (Input.GetKeyDown(KeyCode.Space))
                    step++;
                break;
                // 대쉬
            case 3:
                // 스프라이트 설정
                m_render.sprite = m_keyBoardimage[step];

                // 아이콘 위치 조정
                transform.position = Camera.main.ScreenToWorldPoint(new Vector3(player2_pos.x, player2_pos.y + 150, 1));

                // 입력
                if ((Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift)) && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A)))
                    step++;
                break;
            default:
                Destroy(this.gameObject);
                m_isComplete = true;
                break;
        }
    }
}
