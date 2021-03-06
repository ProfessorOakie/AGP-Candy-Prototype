﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : Singleton<WaveManager> {

    private int WaveNumber = 1;
    private int SpawnZonePriorityTotals = 0;

    [SerializeField]
    private GameObject NewWaveText;
    [SerializeField]
    private GameObject WaveDefeatedText;

    // A list of all the spawn zones that exist
    private SpawnZone[] SpawnZones;

    private void Start()
    {
        SpawnZones = FindObjectsOfType<SpawnZone>();
        foreach (var zone in SpawnZones)
            SpawnZonePriorityTotals += zone.GetSpawnPriority();

        NewWaveText.SetActive(false);
        WaveDefeatedText.SetActive(false);
    }

	// Used by Game Manager when the game has started
	public void GameStart()
	{
		StartCoroutine(NewWaveEffect());
	}
    
    // The last enemy was just killed
    public void EndOfWave()
    {
		++WaveNumber;
        StartCoroutine(WaveDefeatedEffect());
    }

	IEnumerator NewWaveEffect()
	{
		StartCoroutine(InitializeSpawn());

        NewWaveText.GetComponent<SimpleHelvetica>().Text = "WAVE " + WaveNumber;
		NewWaveText.GetComponent<SimpleHelvetica>().GenerateText();

		NewWaveText.SetActive(true);

        var fx = NewWaveText.GetComponentInParent<WaveDefeatedEffects>();
        if (fx) fx.PlayEffects();

        yield return new WaitForSeconds(3);
		NewWaveText.SetActive(false);
        
    }

    IEnumerator WaveDefeatedEffect()
	{
		WaveDefeatedText.SetActive(true);

        var fx = WaveDefeatedText.GetComponentInParent<WaveDefeatedEffects>();
        if (fx) fx.PlayEffects();

		yield return new WaitForSeconds(3);
		WaveDefeatedText.SetActive(false);

        yield return new WaitForSeconds(2);
        StartCoroutine(NewWaveEffect());
	}

    // Spawns new enemies in the zones based on their priority.
	IEnumerator InitializeSpawn()
    {
        Debug.LogWarning("TODO: make this number calculated as designer specifies");
        int numEnemiesToBeSpawned = WaveNumber * 3; 
        foreach(var zone in SpawnZones)
        {
            int numEnemiesToSpawnInZone = Mathf.FloorToInt(((float)zone.GetSpawnPriority() / (float)SpawnZonePriorityTotals * (float) numEnemiesToBeSpawned) + 0.5f);
            for (int i = 0; i < numEnemiesToSpawnInZone; ++i)
            {
                zone.SpawnObject();
				yield return new WaitForSeconds(1);
            }
        }
    }
    

}
