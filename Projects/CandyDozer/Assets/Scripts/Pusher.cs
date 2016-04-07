using UnityEngine;
using System.Collections;

public class Pusher : MonoBehaviour
{
	Vector3 startPosition;
	
	public float amplitude;
	public float speed;
	
	void Start () 
	{
		startPosition = transform.localPosition;
	}
	
	void Update () 
	{
		// 변위를 계산
		float z = amplitude * Mathf.Sin(Time.time * speed);
		
		// z를 변위시킨 포지션으로 재설정
		transform.localPosition = startPosition + new Vector3(0, 0, z);
	}
}
