using UnityEngine;
using System.Collections;

public class LifePanel : MonoBehaviour 
{
	public GameObject[] icons;
	
	// 라이프에 따라 스프라이트를 나누어서 출력
	public void UpdateLife (int life)
	{
		for (int i = 0; i < icons.Length; i++)
		{
			if (i < life) icons[i].SetActive(true);
			else icons[i].SetActive(false);
		}
	}
}
