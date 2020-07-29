using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour {

	public void LoadGameScene()
	{
		//DontDestroyOnLoad (GameObject.FindGameObjectWithTag ("Light"));
		SceneManager.LoadSceneAsync ("ChargeLocationScene");
	}
}
