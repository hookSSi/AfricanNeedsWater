using UnityEngine;
using System.Collections;

public class BarScript : MonoBehaviour
{
    Rect box;

    Texture2D m_backGround;
    Texture2D m_frontGround;

    float energy = 50;
    int maxEnergy = 100;


    public BarScript(float left, float top, float width, float height, Texture2D p_frontGround, Texture2D p_backGround)
    {
        SetRect(left, top, width, height);
        m_backGround = p_frontGround;
        m_frontGround = p_frontGround;
    }
    public BarScript(int i, Texture2D p_frontGround, Texture2D p_backGround)
    {
        SetRect(i);
        m_backGround = p_backGround;
        m_frontGround = p_frontGround;
    }
    public BarScript(float left, float top, float width, float height, Texture2D p_frontGround, Texture2D p_backGround, int energy)
    {
        this.energy = energy;
        SetRect(left, top, width, height);
        m_backGround = p_backGround;
        m_frontGround = p_frontGround;
    }

    public BarScript(int i, Texture2D p_frontGround, Texture2D p_backGround, int energy)
    {
        this.energy = energy;
        SetRect(i);
        m_backGround = p_backGround;
        m_frontGround = p_frontGround;
    }
    public BarScript(float p_width,float p_height,Texture2D p_frontGround, Texture2D p_backGround,int energy)
    {
        
    }

    public void EnergyUpdate()
    { // Update()에서 호출
        energy += Input.GetAxisRaw("Horizontal");
        if (energy < 0) energy = 0;
        if (energy > maxEnergy) energy = maxEnergy;
    }
    public void DrawGUI()
    {      //OnGUI에서 호출
        GUI.BeginGroup(box);
        {
            GUI.DrawTexture(new Rect(0, 0, box.width, box.height), m_backGround, ScaleMode.StretchToFill);
            GUI.DrawTexture(new Rect(0, 0, box.width * energy / maxEnergy, box.height), m_frontGround, ScaleMode.StretchToFill);
        }
        GUI.EndGroup();
    }

    public void Damage(float i)
    {    //데미지주기
        this.energy -= i;
    }
    public void Heal(float i)
    {
        this.energy += i;
    }

    public bool ZeroPoint()
    {        //0인지 받아오기
        if (energy <= 0)
        {
            return true;
        }
        else {
            return false;
        }
    }
    public void SetRect(float x, float y, float width, float height)
    {
        box = new Rect(x, y, width, height);
    }
    public void SetRect(int a)
    {
        switch (a)
        {
            case 0:
                box = new Rect(5, Screen.height - 10 - Screen.height * 20 / 800, Screen.width * 100 / 1600, Screen.height * 20 / 800);
                break;
            case 1:
                box = new Rect(Screen.width / 2 - Screen.width * 100 / 1600 / 2, Screen.height - 10 - Screen.height * 20 / 800, Screen.width * 100 / 1600, Screen.height * 20 / 800);
                break;
            case 2:
                box = new Rect(Screen.width - (Screen.width * 100 / 1600) - 5, Screen.height - 10 - Screen.height * 20 / 480, Screen.width * 100 / 1600, Screen.height * 20 / 800);
                break;
        }
    }
}
