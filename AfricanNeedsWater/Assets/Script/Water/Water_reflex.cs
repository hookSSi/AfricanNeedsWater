using UnityEngine;
using System.Collections;

public class Water_reflex : Water
{
    private int rand = 0;
    private bool once = true;

    protected virtual void Move()
    {  
        rigid.velocity = new Vector3(m_dx,m_dy).normalized * m_speed;
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
        if (other.gameObject.tag == "Ground")
        {
            DestroyWater();
        }
        else if (once && other.gameObject.tag == "Wall")
        {
            m_dx = -m_dx;
            transform.rotation = new Quaternion(m_fireEulerAngle.x, m_fireEulerAngle.y, -m_fireEulerAngle.z, m_fireEulerAngle.w);
            once = false;
        }
        else if(!once && other.gameObject.tag == "Wall")
        {
            DestroyWater();
        }
    } 
    public override void Clear()
    {
      
        rand = CreateRandom(0, 5, 20);
        once = true;
        m_dx = Mathf.Cos(m_Angle * Mathf.Deg2Rad);
        m_dy = Mathf.Sin(m_Angle * Mathf.Deg2Rad);
    }
    protected virtual int CreateRandom(int p_min, int p_max, int p_many)
    {
        int result = 0;

        for(int i = 0; i < p_many; i++)
        {
            result += Random.Range(p_min, p_max);
        }

        if (30 < rand && rand < 45)  // 벽에 반사될 확률
            once = false;

        return result;
    }
}
