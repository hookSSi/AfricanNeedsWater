using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public Texture2D texture;
	public Rect textureCrop;

	// Use this for initialization
	void Start()
	{
		textureCrop = new Rect(0f, 0f, 4f, 0f);
	}

	// Update is called once per frame
	void Update()
	{

	}

	void OnGUI()
	{
		GUI.BeginGroup(new Rect(10, 10, texture.width * textureCrop.width, texture.height * textureCrop.height));
		GUI.DrawTexture(new Rect(-texture.width * textureCrop.x + 550, -texture.height * textureCrop.y, texture.width * 0.5f, texture.height * 0.5f), texture);
		GUI.EndGroup();
	}

	public void AddTextureCrop()
	{
		if (textureCrop.height <= 0.5f) textureCrop.height += 0.005f;
	}
}
