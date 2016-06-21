using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public static bool isPlaying;
	public Texture2D texture;
	public Rect textureCrop;
	public Text object_Text;
	
	private static int stage = 1;
    private float t;

	// Use this for initialization
	void Start()
	{
        t = 0;
        isPlaying = true;
        Screen.SetResolution(Screen.width, Screen.width * 16 / 9, true);
        textureCrop = new Rect(Screen.width - 100, 10, 4f, 0f);
		object_Text.GetComponent<Text>().text = "Stage " + stage;
	}
    void Update()
    {
        if(!isPlaying)
        {
            t += Time.deltaTime;
            if(t >= 3f)
                Application.LoadLevel("MainMenu");
        }   
    }

	void OnGUI()
	{
		//GUI.BeginGroup(new Rect(10, 10, texture.width * textureCrop.width, texture.height * textureCrop.height));
        GUI.DrawTexture(new Rect(Screen.width - 100, 10, texture.width * 0.4f, texture.height * 0.4f), texture);
		//GUI.EndGroup();
	}

	public void AddTextureCrop()
	{
		if (textureCrop.height <= 0.45f) textureCrop.height += 0.07f;
		else
		{
			SetStage();
			textureCrop.height = 0;
		}
	}

	public void SetStage()
	{
		stage++;
		object_Text.GetComponent<Text>().text = "Stage " + stage;
	}

    public static int GetStage() { return stage; }
}
