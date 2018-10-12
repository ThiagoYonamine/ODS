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
	public string[] respostas;
	public string certa;
	
}