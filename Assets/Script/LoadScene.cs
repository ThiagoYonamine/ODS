using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

	float timeLeft;
	public string scene;
	// Use this for initialization
	void Start () {
		timeLeft = 2f; // 2 sec
	}
	
	// Update is called once per frame
	void Update () {
	 	timeLeft -= Time.deltaTime;
		if ( timeLeft < 0 ) {
			SceneManager.LoadScene(scene);
		}
	}
}
