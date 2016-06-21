using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public GameObject m_progressLabel;
    public Text m_progressText;
    public GameObject m_mainMenu;

    void Start()
    {
        Screen.SetResolution(Screen.width, Screen.width * 16 / 9, true);
    }

    public void PlayStart()
    {
        m_mainMenu.SetActive(false);
        m_progressLabel.SetActive(true);
        StartCoroutine("Load","Stage1");
    }
    public void HowToPlay()
    {
        m_mainMenu.SetActive(false);
        m_progressLabel.SetActive(true);
        StartCoroutine("Load", "Tutorial");
    }
    public void Exit()
    {
        Application.Quit();
    }
    IEnumerator Load(string level)
    {
        AsyncOperation async = Application.LoadLevelAsync(level);

        while(!async.isDone)
        {
            float progress = async.progress * 100.0f;
            int pRounded = Mathf.RoundToInt(progress);
            m_progressText.text = "Loading… " + pRounded.ToString() + "%";

            yield return true;
        }
    }
}
