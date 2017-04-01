/* 
  HSController.cs
  
Autor: André Luiz Micheletti - 2017

Tipo: Script de behaviour - Unity3D

Objetivo: Uma classe que controla o sistema de Highscore online. Usando arquivos PHP eu consigo manipular um Banco de Dados Remoto para
guardar e buscar as pontuações mais altas dos jogadores, e mostra-las em elementos de UI.

*/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class HSController : MonoBehaviour {
	
	private string secretKey ="devAndre@!";

	public Text uiText;
	[HideInInspector] public string addScoreUrl ="http://www.andregamedeveloper.com.br/games/addscore.php?";
	[HideInInspector] public string highscoreUrl ="http://www.andregamedeveloper.com.br/games/display.php"; 

	public Button postButton;
	public Text postText;
	public GameObject txtText;
	public GameObject sucessText;
	public Text bigScoreText;

	private GameController gameController;
	private bool sent = false;

	private Dictionary<string, string> headers;

	// Use this for initialization
	void Awake () {
		
		// Reset
		postButton.gameObject.SetActive (true);
		txtText.SetActive (true);
		postText.text = "";
		sucessText.SetActive (false);

		bigScoreText.text = "" + GameController.instance.getScore ();

		// Create Readers
		WWWForm form = new WWWForm();
		headers = form.headers;
		headers ["Access-Control-Allow-Credentials"] = "true";
		headers ["Access-Control-Allow-Headers"] = "Accept, X-Access-Token, X-Application-Name, X-Request-Sent-Time";
		headers ["Access-Control-Allow-Methods"] = "GET, POST, OPTIONS";
		headers ["Access-Control-Allow-Origin"] = "*";

        	StartCoroutine(GetScores());
		sent = false;
	}

	public void postScore() {
		if (sent == true)
			return;

		Debug.Log ("postScores!");
		StartCoroutine(PostScores(postText.text, gameController.getScore()));
	}

    IEnumerator PostScores(string name, int score) {
	string hash = secretKey; 

	string post_url = addScoreUrl + "name=" + WWW.EscapeURL(name) + "&score=" + score + "&hash=" + hash;
	Debug.Log ("URL: " + post_url);

	WWW hs_post = new WWW(post_url, null, headers);
        yield return hs_post;
 
	if (hs_post.error != null) {
		print ("There was an error posting the high score: " + hs_post.error);
	} else {
		sent = true;
		Debug.Log ("Score Sent!");

		postButton.gameObject.SetActive (false);
		txtText.SetActive (false);
		sucessText.SetActive (true);

		StartCoroutine(GetScores());
	}
    }
 
    IEnumerator GetScores() {
	uiText.text = "Loading Scores";

	WWW hs_get = new WWW(highscoreUrl, null, headers);
	foreach(KeyValuePair<string, string> obj in headers) {
		Debug.Log (obj.Key + " : " + obj.Value);
	}
	   
        yield return hs_get;
 
        if (hs_get.error != null) {
            print("There was an error getting the high score: " + hs_get.error);
        } else {
	    uiText.text = hs_get.text;
        }
    }
	
	// Generate MD5 Hash
	public  string Md5Sum(string strToEncrypt)  {
		System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
		byte[] bytes = ue.GetBytes(strToEncrypt);

		// encrypt bytes
		System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
		byte[] hashBytes = md5.ComputeHash(bytes);

		// Convert the encrypted bytes back to a string (base 16)
		string hashString = "";

		for (int i = 0; i < hashBytes.Length; i++)
		{
		hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
		}

		return hashString.PadLeft(32, '0');
	}
}
