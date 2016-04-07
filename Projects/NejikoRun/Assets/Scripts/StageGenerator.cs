using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageGenerator : MonoBehaviour 
{
	const int StageTipSize = 30;
	
	int currentTipIndex;
	
	public Transform character;
	public GameObject[] stageTips;
	public int startTipIndex;
	public int preInstantiate;
	public List<GameObject> generatedStageList = new List<GameObject>();
	
	void Start ()
	{
		currentTipIndex = startTipIndex - 1;
		UpdateStage(preInstantiate);
	}
	
	void Update ()
	{
		// 캐릭터의 위치에서 현재 스테이지 팁의 인덱스를 계산
		int charaPositionIndex = (int)(character.position.z / StageTipSize);
		
		// 다음의 스테이지 팁에 들어가면 스테이지의 업데이트 처리를 실시
		if (charaPositionIndex + preInstantiate > currentTipIndex) 
		{
			UpdateStage(charaPositionIndex + preInstantiate);	
		}
	}
	
	// 지정 Index까지의 스테이지 팁을 생성하여 관리해 둔다
	void UpdateStage (int toTipIndex)
	{
		if(toTipIndex <= currentTipIndex) return;
		
		// 지정한 스테이지 팁까지를 작성 
		for (int i = currentTipIndex + 1; i <= toTipIndex; i++)
		{
			GameObject stageObject = GenerateStage(i);
			
			// 생성한 스테이지를 관리 리스트에 추가
			generatedStageList.Add(stageObject);
		}
		
		// 스테이지 보관 상한이 될 때까지 예전 스테이지를 삭제
		while (generatedStageList.Count > preInstantiate + 2) DestroyOldestStage();
		
		currentTipIndex = toTipIndex;
	}
	
	// 지정 인덱스 위치에 Stage 오브젝트르 임의로 작성
	GameObject GenerateStage (int tipIndex)
	{
		int nextStageTip = Random.Range(0, stageTips.Length);
		
		GameObject stageObject = (GameObject)Instantiate(
			stageTips[nextStageTip],
			new Vector3(0, 0, tipIndex * StageTipSize),
			Quaternion.identity
			);
		
		return stageObject;
	}
	
	// 가장 오래된 스테이지를 삭제
	void DestroyOldestStage ()
	{
		GameObject oldStage = generatedStageList[0];
		generatedStageList.RemoveAt(0);
		Destroy(oldStage);
	}
}