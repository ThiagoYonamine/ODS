using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataBase{
	public Question[] questions;
}

[System.Serializable]
public class Question {
	public string pergunta;
	public Answers respostas;
	public string certa;
	
}

[System.Serializable]
public class Answers {
	public string a;
	public string b;
	public string c;
	public string d;
	
}