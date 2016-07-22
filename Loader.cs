﻿using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour {

	public GameObject gameManager;
	public GameObject soundManager;

	// Use this for initialization
	void Awake () {
		if (GameManger.instance == null)
			Instantiate (gameManager);
//	
//		if (SoundManager.instance == null)
//			Instantiate (soundManager);
	}

}
