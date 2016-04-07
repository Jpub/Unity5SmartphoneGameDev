using UnityEngine;
using System.Collections;

public class ClashCamera : MonoBehaviour
{
	public void Clash ()
	{
		FlashEffect.Play();
		GetComponent<Animator>().SetTrigger("shake");
	}
}
