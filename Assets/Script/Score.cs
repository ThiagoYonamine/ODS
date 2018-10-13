using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
	public Text score;

	// Use this for initialization
	void Start () {

		if(PlayerPrefs.HasKey("bestScore") && PlayerPrefs.HasKey("score")){
			if(PlayerPrefs.GetInt("bestScore") < PlayerPrefs.GetInt("score")){
				PlayerPrefs.SetInt("bestScore", PlayerPrefs.GetInt("score"));
			}
		} else {
			PlayerPrefs.SetInt("bestScore", 0);
		}

		score.text = PlayerPrefs.GetInt("bestScore").ToString();

	}
}
