using UnityEngine;
using System.Collections;

public class GravityController : MonoBehaviour 
{
	// 중력 가속도
	const float Gravity = 9.81f;
	
	// 중력의 적용 상태
	public float gravityScale = 1.0f;
	
	void Update () 
	{
		
		Vector3 vector = new Vector3();
		
		// 유니티 에디터와 실제 단말에서의 처리를 분리
		if (Application.isEditor)
		{
			// 키 입력을 검출하는 벡터를 설정
			vector.x = Input.GetAxis ("Horizontal");
			vector.z = Input.GetAxis ("Vertical");
			
			// 높이 방향의 판정은 z키로 한다
			if (Input.GetKey ("z"))
			{
				vector.y = 1.0f;
			} else {
				vector.y = -1.0f;
			}
		} else {
			// 가속도 센서의 입력을 유니티공간의 축에 맵핑한다
			vector.x = Input.acceleration.x;
			vector.z = Input.acceleration.y;
			vector.y = Input.acceleration.z;
		}
		
		// 씬의 중력을 입력 벡터의 방향에 맞추어 변화시킨다
		Physics.gravity = Gravity * vector.normalized * gravityScale;
	}
}