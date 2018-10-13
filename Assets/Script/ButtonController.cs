﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonController : MonoBehaviour {

	public void LoadScene(string scene){
		SceneManager.LoadScene(scene);
	}

	public void quit(){
		Application.Quit();
	}

	public void destroy(GameObject obj){
		obj.SetActive(false);
	}
}
