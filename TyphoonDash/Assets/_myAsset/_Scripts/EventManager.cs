﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour {

	public void play(){
		SceneManager.LoadScene (1);
	}

	public void mainMenu(){
		SceneManager.LoadScene (0);
	}
}
