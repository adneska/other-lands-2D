using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnHandler : MonoBehaviour
{
    [SerializeField] GameObject enemyPF;
    private float spawnInterval = 3f;
    private int maxEnemies = 5;

    public static int currentEnemies = 0;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (currentEnemies < maxEnemies)
            {
                Transform randomChild = GetRandomChild();
                if (randomChild != null)
                {
                    GameObject newEnemy = Instantiate(enemyPF, randomChild.position, randomChild.rotation);
                    currentEnemies++;
                }
                spawnInterval *= 0.9f;
                yield return new WaitForSeconds(spawnInterval);
            }
            else
            {
                yield return new WaitForSeconds(1f);
            }
        }
    }

    public static void DecreaseEnemyCount()
    {
        currentEnemies--;
    }

    private Transform GetRandomChild()
    {
        Transform[] childObjects = GetComponentsInChildren<Transform>();

        List<Transform> validChildren = new List<Transform>();
        foreach (Transform child in childObjects)
        {
            if (child != transform)
            {
                validChildren.Add(child);
            }
        }

        if (validChildren.Count > 0)
        {
            return validChildren[Random.Range(0, validChildren.Count)];
        }

        return null;
    }
}
