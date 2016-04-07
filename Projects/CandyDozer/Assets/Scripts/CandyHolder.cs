using UnityEngine;
using System.Collections;

public class CandyHolder : MonoBehaviour 
{
	const int DefaultCandyAmount = 30;
	const int RecoverySeconds = 10;
	
	// 현재의 사탕 투척 기회 수
	int candy = DefaultCandyAmount;
	
	// 투척할 수 있도록 회복될 때까지의 남은 시간(초)
	int counter;
	
	public void ConsumeCandy ()
	{
		if (candy > 0) candy--;
	}
	
	public int GetCandyAmount ()
	{
		return candy;
	}
	
	public void AddCandy (int amount)
	{
		candy += amount;
	}
	
	void OnGUI ()
	{
		GUI.color = Color.black;
		
		// 사탕의 투척 기회 수를 표시
		string label = "Candy : " + candy;
		
		// 회복 카운트를 하고 있을 때만 초 수를 표시
		if (counter > 0) label = label + " (" + counter + "s)";
		
		GUI.Label(new Rect(0, 0, 100, 30), label);
	}
	
	void Update ()
	{
		// 사탕의 투척 기회가 디폴트보다 적고
		// 회복 카운트를 하고 있지 않을 때에 카운트를 스타트시킨다
		if (candy < DefaultCandyAmount && counter <= 0)
		{
			StartCoroutine(RecoverCandy());
		} 
	}
	
	IEnumerator RecoverCandy ()
	{
		counter = RecoverySeconds;
		
		// 1초씩 카운트를 진행한다
		while (counter > 0)
		{
			yield return new WaitForSeconds(1.0f);
			counter--;
		}
		
		candy++;
	}
}
