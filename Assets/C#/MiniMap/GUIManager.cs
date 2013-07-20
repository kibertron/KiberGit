using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour
{
	public RenderTexture MiniMapTexture;
	public Material MiniMapMaterial;
	private float offset;

	void Awake ()
	{
		offset = 10;
	}

	void OnGUI ()
	{
		if (Event.current.type == EventType.Repaint)
			Graphics.DrawTexture (new Rect (Screen.width - 128 - offset, offset, 128, 128), MiniMapTexture, MiniMapMaterial);
	}
}