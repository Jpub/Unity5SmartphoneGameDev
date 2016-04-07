using UnityEngine;
using System.Collections;

public class AutoDestroyEffect : MonoBehaviour 
{
	ParticleSystem particle;
	
	void Start ()
	{
		particle = GetComponent<ParticleSystem>();
	}
	
	void Update ()
	{
		// 파티클의 재생이 끝나면 GameObject를 삭제
		if (particle.isPlaying == false) Destroy(gameObject);
	}
}
