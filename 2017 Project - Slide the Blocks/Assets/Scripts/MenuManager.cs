﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

public void StartGame()
	{
	
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);

	}

}
