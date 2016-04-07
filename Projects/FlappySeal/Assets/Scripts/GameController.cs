using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement; //5.3

public class GameController : MonoBehaviour 
{
	// 게임 스테이트
	enum State
	{
		Ready,
		Play,
		GameOver
	} 
	
	State state;
	int score;
	
	public SealController seal;
	public GameObject blocks;
	public Text scoreLabel;
	public Text stateLabel;
	
	void Start ()
	{
		// 시작과 동시에 Ready스테이트로 전환
		Ready();
	}
	
	void LateUpdate ()
	{
		// 게임의 스테이트마다 이벤트를 감시
		switch (state)
		{
		case State.Ready:
			// 탭하면 게임 시작
			if (Input.GetButtonDown("Fire1")) GameStart();
			break;
		case State.Play:
			// 캐릭터가 사망하면 게임 오버
			if (seal.IsDead()) GameOver();
			break;
		case State.GameOver:
			// 탭하면 씬을 리로드
			if (Input.GetButtonDown("Fire1")) Reload();
			break;
		}
	}
	
	void Ready ()
	{
		state = State.Ready;
		
		// 각 오브젝트를 무효 상태로 한다
		seal.SetSteerActive(false);
		blocks.SetActive(false);
		
		// 레이블을 업데이트
		scoreLabel.text = "Score : " + 0;
		
		stateLabel.gameObject.SetActive(true);
		stateLabel.text = "Ready";
	}
	
	void GameStart ()
	{
		state = State.Play;
		
		// 각 오브젝트를 유효로 한다
		seal.SetSteerActive(true);
		blocks.SetActive(true);
		
		// 처음의 입력만 게임 컨트롤러로부터 건넨다
		seal.Flap();
		
		// 레이블을 업데이트
		stateLabel.gameObject.SetActive(false);
		stateLabel.text = "";
	}
	
	void GameOver ()
	{
		state = State.GameOver;
		
		// 씬 안의 모든 ScrollObject컴포넌트를 찾아낸다
		ScrollObject[] scrollObjects = GameObject.FindObjectsOfType<ScrollObject>();
		
		// 모든 ScrollObject의 스크롤 처리를 무효로 한다
		foreach (ScrollObject so in scrollObjects) so.enabled = false;
		
		// 레이블을 업데이트
		stateLabel.gameObject.SetActive(true);
		stateLabel.text = "GameOver";
	}
	
	void Reload ()
	{
		// 현재 로딩하고 있는 씬을 리로딩
		//Application.LoadLevel(Application.loadedLevel);  //5.2
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);   //5.3
	}
	
	public void IncreaseScore () 
	{
		score++;
		scoreLabel.text = "Score : " + score;
	}
}