using UnityEngine;
using System.Collections;

public class Water_reflex : Water
{
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
    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            Instantiate(m_particle, transform.position, transform.rotation);
            WaterList.Enqueue(this.gameObject);
            this.gameObject.SetActive(false);
        }
        else if(other.gameObject.tag == "Wall")
        {
            m_dx = -m_dx;
        }

        /*if (other.gameObject.tag == "Player2")
		{
			Instantiate(m_particle, transform.position, transform.rotation);
			WaterList.Enqueue(this.gameObject);
			other.gameObject.GetComponent<Player2Control>().AddCount_ScoreCoin();
			this.gameObject.SetActive(false);
		}*/
    }
}
