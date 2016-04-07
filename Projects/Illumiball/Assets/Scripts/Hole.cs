using UnityEngine;
using System.Collections;

public class Hole : MonoBehaviour 
{
	bool fallIn;
	
	// 어떤 공을 빨아 들일 지를 태그로 지정
	public string activeTag;
	
	// 공이 들어와 있는지를 반환
	public bool IsFallIn()
	{
		return fallIn;
	}
	
	void OnTriggerEnter (Collider other)
	{
		if(other.gameObject.tag == activeTag)
		{
			fallIn = true;
		}
	}
	
	void OnTriggerExit (Collider other)
	{
		if(other.gameObject.tag == activeTag)
		{
			fallIn = false;
		}
	}
	
	void OnTriggerStay (Collider other)
	{
		// 콜라이더에 접촉하고 있는 오브젝트의 Rigidbody컴포넌트를 취득 
		Rigidbody r = other.gameObject.GetComponent<Rigidbody>();
		
		// 공이 어느 방향에 있는지를 계산
		Vector3 direction = transform.position - other.gameObject.transform.position;
		direction.Normalize();
		
		// 태그에 따라 공에 힘을 더한다
		if (other.gameObject.tag == activeTag)
		{
			// 중심 지점에서 공을 멈추기 위해 속도를 감속시킨다 
			r.velocity *= 0.9f;
			
			r.AddForce(direction * r.mass * 20.0f);
		} else {
			r.AddForce(- direction * r.mass * 80.0f);
		}
	}
}