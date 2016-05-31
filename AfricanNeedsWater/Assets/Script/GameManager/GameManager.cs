using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public static bool isPlaying;
	public Texture2D texture;
	public Rect textureCrop;
	public Text object_Text;
	
	private int stage = 1;

	// Use this for initialization
	void Start()
	{
		textureCrop = new Rect(0f, 0f, 4f, 0f);
		object_Text.GetComponent<Text>().text = "Stage " + stage;

		isPlaying = true;
	}

	// Update is called once per frame
	void Update()
	{

	}

	void OnGUI()
	{
		GUI.BeginGroup(new Rect(10, 10, texture.width * textureCrop.width, texture.height * textureCrop.height));
		GUI.DrawTexture(new Rect(-texture.width * textureCrop.x + 580, -texture.height * textureCrop.y + 5, texture.width * 0.4f, texture.height * 0.4f), texture);
		GUI.EndGroup();
	}

	public void AddTextureCrop()
	{
		if (textureCrop.height <= 0.45f) textureCrop.height += 0.005f;
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
}
