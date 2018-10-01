using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FadeIn : MonoBehaviour {


	private float timeLeft;
	private Image fade;
	private Color alphaColor;

	private float TOTAL_TIME;
	// Use this for initialization
	void Start () {
		TOTAL_TIME = 1f;
		timeLeft = TOTAL_TIME;
		fade = GetComponent<Image>();
		alphaColor = fade.color;

		alphaColor.a = 1f;
		fade.color = alphaColor;
	}
	
	// Update is called once per frame
	void Update () {
		timeLeft -= Time.deltaTime;
		alphaColor.a = (timeLeft/TOTAL_TIME);
		fade.color = alphaColor;
		if(timeLeft <= 0 ) {
			this.gameObject.SetActive(false);
		}
	}
}
