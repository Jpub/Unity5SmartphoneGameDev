using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  //5.3

public class TitleController : MonoBehaviour 
{
	public Text highScoreLabel;
	
	public void Start ()
	{
		// 하이 스코어를 표시
		highScoreLabel.text = "High Score : " + PlayerPrefs.GetInt("HighScore") + "m";
	}
	
	public void OnStartButtonClicked ()
	{
		//Application.LoadLevel("Main");  //5.2
		SceneManager.LoadScene("Main");   //5.3
	}
}
