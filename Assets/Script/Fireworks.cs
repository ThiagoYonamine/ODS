using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireworks : MonoBehaviour {
	public GameObject[] fireWorks;

	 public float secondsBetweenSpawn;
     public float elapsedTime;
	  public float elapsedTime2;
	  public float elapsedTime3;
	 private ParticleSystem ps;
	 private int index;
	 private int lastIndex;
	void Start(){
		secondsBetweenSpawn = 1.3f;
		elapsedTime = 0.0f;
		elapsedTime2 = 0.0f;
		elapsedTime3 = 0.0f;
		lastIndex = -1;
		for(int i=0;i<6;i++){
			ps = fireWorks[i].GetComponent<ParticleSystem>();
			ps.Simulate(0.0f, true, true);
			ps.Stop();
		}

		fireWorks[4].GetComponent<ParticleSystem>().Simulate(0.0f, true, true);
		fireWorks[4].GetComponent<ParticleSystem>().Play();
		fireWorks[5].GetComponent<ParticleSystem>().Simulate(0.4f, true, true);
		fireWorks[5].GetComponent<ParticleSystem>().Play();
	}
	void FixedUpdate () {
		elapsedTime += Time.deltaTime;
		elapsedTime2 += Time.deltaTime;
		if (elapsedTime > secondsBetweenSpawn) {
			index = Random.Range(0, 4);
			while(index == lastIndex){
				index = Random.Range(0, 4);
			}
			lastIndex = index;
			float x = Random.Range(-2.5f, 2.5f);
			float y = Random.Range(-1, 5);
			fireWorks[index].transform.position = new Vector3(x, y, 0);
			fireWorks[index].GetComponent<ParticleSystem>().Simulate(0.0f, true, true);
			fireWorks[index].GetComponent<ParticleSystem>().Play();
			elapsedTime = 0;
		}

		if (elapsedTime >= 1 && fireWorks[index].GetComponent<ParticleSystem>().isPlaying) {
			fireWorks[index].GetComponent<ParticleSystem>().Stop();
		}

		if (elapsedTime2 >= 1.5) {
			float x = Random.Range(-2.5f, 2.5f);
			float y = Random.Range(-1, 5);
			fireWorks[4].transform.position = new Vector3(x, y, 0);
			fireWorks[4].GetComponent<ParticleSystem>().Simulate(0.0f, true, true);
			fireWorks[4].GetComponent<ParticleSystem>().Play();

			x = Random.Range(-2.5f, 2.5f);
			y = Random.Range(-1, 5);
			fireWorks[5].transform.position = new Vector3(x, y, 0);
			fireWorks[5].GetComponent<ParticleSystem>().Simulate(0.3f, true, true);
			fireWorks[5].GetComponent<ParticleSystem>().Play();
			elapsedTime2 = 0;
		}


	}
}
