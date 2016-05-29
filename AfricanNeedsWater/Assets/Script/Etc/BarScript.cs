using UnityEngine;
using System.Collections;

public class BarScript : MonoBehaviour
{
    Rect box;

    Texture2D bg;
    Texture2D fg;

    float energy = 50;
    int maxEnergy = 100;


    public BarScript(float left, float top, float width, float height, Texture2D fgTexture, Texture2D bgTexture)
    {
        SetRect(left, top, width, height);
        bg = bgTexture;
        fg = fgTexture;
    }
    public BarScript(int i, Texture2D fgTexture, Texture2D bgTexture)
    {
        SetRect(i);
        bg = bgTexture;
        fg = fgTexture;
    }
    public BarScript(float left, float top, float width, float height, Texture2D fgTexture, Texture2D bgTexture, int energy)
    {
        this.energy = energy;
        SetRect(left, top, width, height);
        bg = bgTexture;
        fg = fgTexture;
    }

    public BarScript(int i, Texture2D fgTexture, Texture2D bgTexture, int energy)
    {
        this.energy = energy;
        SetRect(i);
        bg = bgTexture;
        fg = fgTexture;
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
            GUI.DrawTexture(new Rect(0, 0, box.width, box.height), bg, ScaleMode.StretchToFill);
            GUI.DrawTexture(new Rect(0, 0, box.width * energy / maxEnergy, box.height), fg, ScaleMode.StretchToFill);
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
    public void SetRect(float a, float b, float c, float d)
    {
        box = new Rect(a, b, c, d);
    }
    public void SetRect(int a)
    {
        switch (a)
        {
            case 0:
                box = new Rect(5, Screen.height - 10 - Screen.height * 20 / 480, Screen.width * 100 / 320, Screen.height * 20 / 480);
                break;
            case 1:
                box = new Rect(Screen.width / 2 - Screen.width * 100 / 320 / 2, Screen.height - 10 - Screen.height * 20 / 480, Screen.width * 100 / 320, Screen.height * 20 / 480);
                break;
            case 2:
                box = new Rect(Screen.width - (Screen.width * 100 / 320) - 5, Screen.height - 10 - Screen.height * 20 / 480, Screen.width * 100 / 320, Screen.height * 20 / 480);
                break;
        }
    }
}
