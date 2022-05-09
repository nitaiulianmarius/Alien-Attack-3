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
		if(File.Exists(Application.persistentDataPath + "HighScore_AAV3.txt")) {
			theReader = new StreamReader (Application.persistentDataPath + "HighScore_AAV3.txt", Encoding.Default);
			highScore = theReader.ReadLine ();
			theReader.Close ();
		} else {
			highScore = "0";
			File.CreateText(Application.persistentDataPath + "HighScore_AAV3.txt");
		}
		UpdateScore ();
		StartCoroutine (SpawnWaves ());
	}

	void OnGUI()
	{
		if (restart) {
			//while(true)
			//{
				if (GUI.Button (new Rect (Screen.width/2-100, Screen.height/2, 200, 30), "Restart")) {
					Application.LoadLevel (Application.loadedLevel);
				}
				
				if (GUI.Button (new Rect (Screen.width/2-100, Screen.height/2+40, 200, 30), "Return to main menu")) {
						Application.LoadLevel ("MainMenu");
				}
			//}
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
			theWriter = new StreamWriter (Application.persistentDataPath + "HighScore_AAV3.txt");
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