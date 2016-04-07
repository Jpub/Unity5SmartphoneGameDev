using UnityEngine;
using System.Collections;

public class CandyDestroyer : MonoBehaviour 
{
	public CandyHolder candyHolder;
	public int reward;
	public GameObject effectPrefab;
	public Vector3 effectRotation;
	
	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Candy")
		{
			// 지정 수만큼만 Candy의 던질 기회을 늘린다			
			candyHolder.AddCandy(reward);
			
			// 오브젝트를 삭제
			Destroy(other.gameObject);
			
			if (effectPrefab != null)
			{
				// Candy의 포지션에 효과를 생성	
				Instantiate(
					effectPrefab, 
					other.transform.position, 
					Quaternion.Euler(effectRotation)
					);
			}
		}
	}
}
