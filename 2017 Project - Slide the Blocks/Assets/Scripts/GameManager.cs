
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour {

	bool gameHasEnded = false;

	public float restartDelay = 5f;

	public GameObject GameOverScreen;
	public UnityEvent OnGameOver;

	public void GameOver()
	{
		OnGameOver.Invoke ();
	}

	public void EndGame () 
	{
		if (gameHasEnded == false)
		{
			gameHasEnded = true;
			GameOver ();
			//Debug.Log ("GAME OVER");
			//Invoke("Restart", restartDelay);
		}
	}
	
	public void Restart()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}
}
