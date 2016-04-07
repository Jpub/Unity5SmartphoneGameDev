using UnityEngine;
using System.Collections;

class FlashEffect : MonoBehaviour
{
	private Texture2D texture;
	private float alpha;

	public static void Play ()
	{
		GameObject go = new GameObject("Flash");
		go.AddComponent<FlashEffect>();
	}

	void Awake ()
	{
		texture = new Texture2D(4, 4, TextureFormat.RGB24, false);

		for (int y = 0; y < texture.height; ++y) 
		{
			for (int x = 0; x < texture.width; ++x) 
			{
				texture.SetPixel(x, y, Color.white);
			}
		}

		texture.Apply();	
		alpha = 1.0f;
	}

	void OnGUI ()
	{
		float dim = Mathf.Clamp01(Time.deltaTime * 2.0f);
		alpha = Mathf.Clamp01(alpha - dim);

		GUI.color = new Color(1.0f, 1.0f, 1.0f, alpha);
		GUI.DrawTexture( new Rect(0, 0, Screen.width, Screen.height ), texture );

		if (alpha == 0.0f)
		{
			Destroy(gameObject);
		}
	}

	void OnDestroy ()
	{
		Destroy(texture);
	}
}