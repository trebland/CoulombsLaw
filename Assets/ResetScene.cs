using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScene : MonoBehaviour {

	public void ReloadScene ()
	{
		Time.timeScale = 0;
		SceneManager.LoadScene ("ChargeLocationScene");
	}
}
