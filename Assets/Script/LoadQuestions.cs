using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LoadQuestions : MonoBehaviour {

	public Text txt_question;
	public Button btn_a;
	public Button btn_b;
	public Button btn_c;
	public Button btn_d;
	public Button btn_next;
	private Text btn_txt_a;
	private Text btn_txt_b;
	private Text btn_txt_c;
	private Text btn_txt_d;
	private DataBase db;
	private int index;
	private bool[] isAnswered;

	private Color incorrect;
	private Color correct;
	private Color normal;

	// Use this for initialization
	void Start () {
		incorrect = new Color(1f, 0.55f, 0.55f, 1f);
		correct = new Color(0.75f, 1f, 0.55f, 1);
		normal = new Color(1f, 1f, 1f, 1f);
		string path = "Assets/Questions/questions.json";
		StreamReader reader = new StreamReader(path);
		string json = reader.ReadToEnd();
		db = JsonUtility.FromJson<DataBase>(json);
		isAnswered = new bool[db.questions.Length];
		for(int i=0;i<isAnswered.Length;i++){ isAnswered[i]= false; }

		btn_txt_a = btn_a.GetComponentInChildren<Text>();
		btn_txt_b = btn_b.GetComponentInChildren<Text>();
		btn_txt_c = btn_c.GetComponentInChildren<Text>();
		btn_txt_d = btn_d.GetComponentInChildren<Text>();
		btn_next.gameObject.SetActive(false);
		resetButtonColor();
		loadRandomQuestion();
	}

	public void loadRandomQuestion() {
		resetButtonColor();
		if(db.questions.Length > 0) {
			index = Random.Range(0, db.questions.Length);
			int maxSearch = db.questions.Length*2;
			while(isAnswered[index]) {
				index = Random.Range(0, db.questions.Length);
				maxSearch--;
				if(maxSearch<=0) break;
			}
			isAnswered[index] = true;
			txt_question.text = db.questions[index].pergunta;
			btn_txt_a.text = db.questions[index].respostas.a;
			btn_txt_b.text = db.questions[index].respostas.b;
			btn_txt_c.text = db.questions[index].respostas.c;
			btn_txt_d.text = db.questions[index].respostas.d;
		}
	}

	public void sendResponse(string response) {
		showCorrectAnswer();
	}

	private void resetButtonColor() {
		btn_a.image.color = normal;
		btn_b.image.color = normal;
		btn_c.image.color = normal;
		btn_d.image.color = normal;
		btn_next.gameObject.SetActive(false);

	}

	private void showCorrectAnswer( ) {
		btn_a.image.color = db.questions[index].certa == "a" ? correct : incorrect;
		btn_b.image.color = db.questions[index].certa == "b" ? correct : incorrect;
		btn_c.image.color = db.questions[index].certa == "c" ? correct : incorrect;
		btn_d.image.color = db.questions[index].certa == "d" ? correct : incorrect;
		btn_next.gameObject.SetActive(true);
	}

}
