	
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	[SerializeField] Transform player;
	private Text scoreText;

    private void Awake()
    {
        scoreText = GetComponent<Text>();
    }

    void Update () {

        scoreText.text = player.GetComponent<NewPlayerController>().PlayerScore.ToString();

	}
}
