using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour {

	public AudioClip correct, wrong;
	public AudioSource audioSrc;

	void play(string audio){
		if(audio=="correct"){
			audioSrc.PlayOneShot(correct);
		} else{
			audioSrc.PlayOneShot(wrong);
		}
	}
}
