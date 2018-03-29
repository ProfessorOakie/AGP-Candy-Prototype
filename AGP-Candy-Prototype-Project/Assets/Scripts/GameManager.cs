using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

    private int numberLivingEnemies = 0;
	private bool gameStarted = false;

	[SerializeField]
	private GameObject pregameObject;

	void Update()
	{
		if (Input.GetKey (KeyCode.Space) && !gameStarted) {
			StartGame ();
		}
	}

	public void StartGame()
	{
		gameStarted = true;
		Destroy(pregameObject);
		WaveManager.Instance.GameStart();
	}

    public void NumLivingEnemiesDecrement()
    {
        --numberLivingEnemies;
        if(numberLivingEnemies == 0)
        {
            WaveManager.Instance.EndOfWave();
        }
    }

    public void NumLivingEnemiesIncrement()
    {
        ++numberLivingEnemies;
    }

}
