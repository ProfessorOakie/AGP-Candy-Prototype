using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : Singleton<WaveManager> {

    private int WaveNumber = 0;
    private int SpawnZonePriorityTotals = 0;

    [SerializeField]
    private GameObject NewWaveEffect;
    [SerializeField]
    private GameObject WaveDefeatedEffect;

    // A list of all the spawn zones that exist
    private SpawnZone[] SpawnZones;

    private void Start()
    {
        SpawnZones = FindObjectsOfType<SpawnZone>();
        foreach (var zone in SpawnZones)
            SpawnZonePriorityTotals += zone.GetSpawnPriority();

        NewWaveEffect.SetActive(false);
        WaveDefeatedEffect.SetActive(false);

        NewWave();
    }
    
    // The last enemy was just killed
    public void EndOfWave()
    {
        StartCoroutine(WaitForNextWave());
    }

    // Time inbetween waves
    IEnumerator WaitForNextWave()
    {
        Debug.LogWarning("TODO: Implemment hardcoded values");

        WaveDefeatedEffect.SetActive(true);
        yield return new WaitForSeconds(3);
        WaveDefeatedEffect.SetActive(false);
        yield return new WaitForSeconds(3);
        NewWaveEffect.SetActive(true);
        yield return new WaitForSeconds(3);
        NewWaveEffect.SetActive(false);
        NewWave();
    }

    // Spawns new enemies in the zones based on their priority.
    private void NewWave()
    {
        ++WaveNumber;
        Debug.LogWarning("TODO: make this number calculated as designer specifies");
        int numEnemiesToBeSpawned = WaveNumber * 3; 
        foreach(var zone in SpawnZones)
        {
            int numEnemiesToSpawnInZone = Mathf.FloorToInt(((float)zone.GetSpawnPriority() / (float)SpawnZonePriorityTotals * (float) numEnemiesToBeSpawned) + 0.5f);
            for (int i = 0; i < numEnemiesToSpawnInZone; ++i)
            {
                // Check enemy was successfully spawned
                if (zone.SpawnObject())
                    GameManager.Instance.NumLivingEnemiesIncrement();
            }
        }
    }
    

}
