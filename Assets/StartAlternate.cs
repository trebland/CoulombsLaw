using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAlternate : MonoBehaviour {

	public void Awake ()
	{
		Time.timeScale = 0;
	}

	public void StartUp ()
	{
		if (!ForceScript.running) {
			ForceScript.running = true;
			Time.timeScale = 1;
		}
	}

}
