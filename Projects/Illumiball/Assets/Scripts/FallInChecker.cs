using UnityEngine;
using System.Collections;

public class FallInChecker : MonoBehaviour 
{
	public Hole red;
	public Hole blue;
	public Hole green;
	
	void OnGUI()
	{
		string label = " ";
		
		// 모든 홀에 들어왔다면 레이블을 표시
		if (red.IsFallIn() && blue.IsFallIn() && green.IsFallIn())
		{
			label = "Fall in hole!";
		}
		
		GUI.Label (new Rect(0,0,100,30), label);
	}
}
