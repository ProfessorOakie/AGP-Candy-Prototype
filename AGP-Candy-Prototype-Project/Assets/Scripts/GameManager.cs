using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {



    private int numberLivingEnemies = 0;

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
