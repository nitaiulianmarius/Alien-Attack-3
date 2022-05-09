using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;

public class GameController : MonoBehaviour
{
	public GameObject hazard;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;

	private StreamReader theReader;
	private StreamWriter theWriter;
	private bool gameOver;
	private bool restart;
	private int score;
	private string highScore;
	
	void Start ()
	{
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		score = 0;
		if(File.Exists("AlienAttackV3_Data/HighScore_AAV3.txt")) {
			theReader = new StreamReader ("AlienAttackV3_Data/HighScore_AAV3.txt", Encoding.Default);
			highScore = theReader.ReadLine ();
			theReader.Close ();
		} else {
			highScore = "0";
			File.CreateText("AlienAttackV3_Data/HighScore_AAV3.txt");
		}
		UpdateScore ();
		StartCoroutine (SpawnWaves ());
	}

	void Update()
	{
		if (restart)
		{
			if(Input.GetKeyDown(KeyCode.R))
			{
				Application.LoadLevel(Application.loadedLevel);
			}
		}
	}

	void OnGUI()
	{
		if (restart) {
			if (GUI.Button (new Rect (Screen.width/2-100, Screen.height/2, 200, 30), "Restart")) {
				Application.LoadLevel (Application.loadedLevel);
			}
		
			if (GUI.Button (new Rect (Screen.width/2-100, Screen.height/2+40, 200, 30), "Return to main menu")) {
				Application.LoadLevel ("MainMenu");
			}
		}
	}

	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		while (true)
		{
			for (int i = 0; i < hazardCount; i++)
			{
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);

			if(gameOver)
			{
				restartText.text = "Press 'R' for restart";
				restart = true;
				break;
			}
		}
	}

	public void AddScore(int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}

	void UpdateScore()
	{
		if (score > int.Parse (highScore)) {
			highScore = score.ToString ();
			theWriter = new StreamWriter ("AlienAttackV3_Data/HighScore_AAV3.txt");
			theWriter.Write(highScore);
			theWriter.Close();
		}

		scoreText.text = "Score: " + score + " / " + highScore;
	}

	public void GameOver()
	{
		gameOverText.text = "Game Over !";
		gameOver = true;
	}
}