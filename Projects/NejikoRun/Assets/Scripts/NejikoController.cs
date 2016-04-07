using UnityEngine;
using System.Collections;

public class NejikoController: MonoBehaviour 
{
	const int MinLane = -2;
	const int MaxLane = 2;
	const float LaneWidth = 1.0f;
	const int DefaultLife = 3;
	const float StunDuration = 0.5f;
	
	CharacterController controller;
	Animator animator;
	
	Vector3 moveDirection = Vector3.zero;
	int targetLane;
	int life = DefaultLife;
	float recoverTime = 0.0f;
	
	public float gravity;
	public float speedZ;
	public float speedX;
	public float speedJump;
	public float accelerationZ;	
	
	public int Life ()
	{
		return life;
	}
	
	public bool IsStan ()
	{
		return recoverTime > 0.0f || life <= 0;
	}
	
	void Start ()
	{
		// 필요한 컴포넌트를 자동 취득
		controller = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
	}
	
	void Update ()
	{
		// 디버그 용
		if (Input.GetKeyDown("left")) MoveToLeft();
		if (Input.GetKeyDown("right")) MoveToRight();
		if (Input.GetKeyDown("space")) Jump();
		
		if (IsStan())
		{
			// 움직임을 기절 상태에서 복귀 카운트를 진행한다
			moveDirection.x = 0.0f;
			moveDirection.z = 0.0f;
			recoverTime -= Time.deltaTime;
		}
		else
		{
			// 서서히 가속하여 Z 방향으로 계속 전진시킨다
			float acceleratedZ = moveDirection.z + (accelerationZ * Time.deltaTime); 
			moveDirection.z = Mathf.Clamp(acceleratedZ, 0, speedZ);
			
			// X방향은 목표의 포지션까지의 차등 비율로 속도를 계산
			float ratioX = (targetLane * LaneWidth - transform.position.x) / LaneWidth;
			moveDirection.x = ratioX * speedX;
		}
		
		// 중력만큼의 힘을 매 프레임 추가
		moveDirection.y -= gravity * Time.deltaTime;
		
		// 이동 실행
		Vector3 globalDirection = transform.TransformDirection(moveDirection);
		controller.Move(globalDirection * Time.deltaTime);
		
		// 이동 후 접지하고 있으면 Y 방향의 속도는 리셋한다
		if (controller.isGrounded) moveDirection.y = 0;
		
		// 속도가 0 이상이면 달리고 있는 플래그를 true로 한다
		animator.SetBool("run", moveDirection.z > 0.0f);
	}
	
	// 왼쪽 차선으로 이동을 시작
	public void MoveToLeft ()
	{
		if (IsStan()) return;
		if (controller.isGrounded && targetLane > MinLane) targetLane--;
	}
	
	// 오른쪽 차선으로 이동을 시작
	public void MoveToRight ()
	{
		if (IsStan()) return;
		if (controller.isGrounded && targetLane < MaxLane) targetLane++;
	}
	
	public void Jump ()
	{
		if (IsStan()) return;
		if (controller.isGrounded) 
		{
			moveDirection.y = speedJump;
			
			// 점프 트리거를 설정
			animator.SetTrigger("jump");
		}
	}
	
	// CharacterController에 충돌이 발생했을 때의 처리
	void OnControllerColliderHit (ControllerColliderHit hit) 
	{
		if(IsStan()) return;
		
		if(hit.gameObject.tag == "Robo")
		{
			// 라이프를 줄이고 기절 상태로 전환
			life--;
			recoverTime = StunDuration;
			
			// 데미지 트리거를 설정
			animator.SetTrigger("damage");
			
			// 히트한 오브젝트는 삭제
			Destroy(hit.gameObject);
		}
	}
}