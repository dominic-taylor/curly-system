using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManger : MonoBehaviour {

	public static GameManger instance = null;
	private BoardManager boardScript;
	private int level = 3;

	// Use this for initialization
	void Awake () {
	
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);
		boardScript = GetComponent<BoardManager> ();
		InitGame ();
	}
	void InitGame()
	{
		boardScript.SetupScene (level);
	}
	// Update is called once per frame
	void Update () {
	
	}
}
