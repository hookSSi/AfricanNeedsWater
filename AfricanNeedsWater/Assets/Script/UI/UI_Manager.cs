using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    void Start()
    {
        Screen.SetResolution(Screen.width, Screen.width * 16 / 9, true);
    }

    public void PlayStart()
    {
        Application.LoadLevel("Stage1");
    }
    public void HowToPlay()
    {
        Application.LoadLevel("HowToPlay");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
