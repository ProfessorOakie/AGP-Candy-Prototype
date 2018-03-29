using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{

    [SerializeField]
    private GameObject enemyPrefab;
    private float generationExclusionRadius;

    [SerializeField] [Range(0.10f, 0.90f)]
    private float minGenerationRadiusPercentage;
    private float minRadius;
    private float maxGenerationRadiusPercentage = 0.95f;
    private float maxRadius;
    [SerializeField] [Range(0.01f, 10)]
    private float generationInterval;
    private int enemyCounter;
    private Coroutine enemyGeneratorCoroutine;


    // Use this for initialization
    void Start () {
        Vector3 generationSurface = gameObject.GetComponent<Collider>().bounds.extents;
        minRadius = minGenerationRadiusPercentage * Mathf.Min(generationSurface.x, generationSurface.z);
        maxRadius = maxGenerationRadiusPercentage * Mathf.Min(generationSurface.x, generationSurface.z);
        enemyCounter = 0;
        enemyGeneratorCoroutine = StartCoroutine(EnemyGeneration());

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator EnemyGeneration() {
        if (enemyPrefab)
        {
            GenerateEnemy();
            yield return new WaitForSeconds(generationInterval);
            enemyGeneratorCoroutine = StartCoroutine(EnemyGeneration());
        }
        else 
        {
            Debug.LogError("enemy Prefab null");  
        }
    }

    private void GenerateEnemy()
    {
        GameObject enemyObj = Instantiate(enemyPrefab);
        // Turns out the code below might be better done with Random.insideUnitCircle()
        float presetY = enemyPrefab.transform.position.y;
        float randomRadius = Random.Range(minRadius, maxRadius);
        float randomAngle = Random.Range(Mathf.Epsilon, 2 * Mathf.PI);
        float randomX = randomRadius * Mathf.Cos(randomAngle);
        float randomZ = randomRadius * Mathf.Sin(randomAngle);
        enemyObj.transform.position = new Vector3(randomX, presetY, randomZ);
        enemyCounter++;
    }
}
