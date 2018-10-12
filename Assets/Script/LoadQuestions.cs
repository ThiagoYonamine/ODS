using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class LoadQuestions : MonoBehaviour {

	public Image timer;
	public Text txt_question;
	public Button btn_a;
	public Button btn_b;
	public Button btn_c;
	public Button btn_d;
	public Button btn_next;
	public string nivel;

	public GameObject[] lifesUI;

	private Text btn_txt_a;
	private Text btn_txt_b;
	private Text btn_txt_c;
	private Text btn_txt_d;
	private DataBase db;
	private int index;
	private bool[] isAnswered;
	private Dictionary<char,char> questionsMap;

	private Color incorrect;
	private Color correct;
	private Color normal;

	private int acertos;
	private bool activeTimer;

	private int correctsToNextLevel = 5;
	private int lifes;

	// Use this for initialization
	void Start () {
		incorrect = new Color(1f, 0.55f, 0.55f, 1f);
		correct = new Color(0.75f, 1f, 0.55f, 1);
		normal = new Color(1f, 1f, 1f, 1f);
		questionsMap = new Dictionary<char, char>();
		if(nivel=="facil"){
			PlayerPrefs.SetInt("lifes", 3);
		}
		lifes = PlayerPrefs.GetInt("lifes");

		string file = nivel + ".json";

		string path = "Resources/" + file;
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

		acertos=0;
		reset();
		loadRandomQuestion();
	}

	public void loadRandomQuestion() {
		if(acertos == correctsToNextLevel) {
			goToNextLevel(nivel);
			return;
		}

		if(lifes <= 0) {
			SceneManager.LoadScene("GameOver");
			return;
		}
		reset();

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
			int randAns = Random.Range(0, 4);
			if(db.questions[index].respostas[3].Contains("Todas")) {
				randAns = 2;
			}
			for(int i =0;i<4;i++ ){
				questionsMap[(char)('a'+i)] = (char)('a'+randAns); 
				randAns += 3;
				randAns %= 4;
			}

			btn_txt_a.text = db.questions[index].respostas[(int)questionsMap['a']-'a'];
			btn_txt_b.text = db.questions[index].respostas[(int)questionsMap['b']-'a'];
			btn_txt_c.text = db.questions[index].respostas[(int)questionsMap['c']-'a'];
			btn_txt_d.text = db.questions[index].respostas[(int)questionsMap['d']-'a'];
		}
	}

	public void sendResponse(string response) {
		if (activeTimer) {
			char certa = db.questions[index].certa[0];
			bool isCorrect;
			if (response[0] == questionsMap[certa]) {
				acertos++;
				isCorrect = true;
			} else {
				isCorrect = false;
			}
			showCorrectAnswer(certa, isCorrect);
		}
	}

	private void reset() {
		for(int i=0;i<4;i++){
			lifesUI[i].SetActive(false);
		}

		btn_a.image.color = normal;
		btn_b.image.color = normal;
		btn_c.image.color = normal;
		btn_d.image.color = normal;
		timer.fillAmount = 0;
		activeTimer = true;
		btn_next.gameObject.SetActive(false);
		 
	}

	private void showLifes(bool isCorrect) {
		for(int i=0;i<=lifes;i++) {
			lifesUI[i].SetActive(true);
		}
		if(!isCorrect) {
			Animator m_Animator;
			m_Animator = lifesUI[lifes].GetComponent<Animator>();
			m_Animator.Play("error");
			lifes--;
		}
	}

	private void showCorrectAnswer(char certa, bool isCorrect) {
		showLifes(isCorrect);
		btn_a.image.color = certa == questionsMap['a'] ? correct : incorrect;
		btn_b.image.color = certa == questionsMap['b'] ? correct : incorrect;
		btn_c.image.color = certa == questionsMap['c'] ? correct : incorrect;
		btn_d.image.color = certa == questionsMap['d'] ? correct : incorrect;
		btn_next.gameObject.SetActive(true);
		activeTimer = false;
	}

 	void Update(){
		if(activeTimer) {
			//Reduce timer amount over 60 seconds
			timer.fillAmount += 1.0f / 60 * Time.deltaTime;
			if(timer.fillAmount >= 1.0f){
				sendResponse("x");
			}
		}
    }

	void goToNextLevel(string current){
		string nextLevel = "Fase";
		switch (current)
		{
			case "facil":
				nextLevel = nextLevel+"2";
				break;
			case "medio":
				nextLevel = nextLevel+"3";
				break;
			case "dificil":
				nextLevel = "Victory";
				break;
			default:
				break;
		}
		PlayerPrefs.SetInt("lifes", lifes);
		SceneManager.LoadScene(nextLevel);
	}
}