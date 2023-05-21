using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnHandler : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;  // Префаб врага для спавна
    private float spawnInterval = 3f;  // Промежуток времени между спаунами
    private int maxEnemies = 5;  // Максимальное количество врагов на сцене    

    private static int currentEnemies = 0;  // Текущее количество врагов на сцене

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            // Проверяем, достигнуто ли максимальное количество врагов на сцене
            if (currentEnemies < maxEnemies)
            {
                // Выбираем случайный дочерний объект
                Transform randomChild = GetRandomChild();
                if (randomChild != null)
                {
                    // Создаем нового врага на позиции выбранного дочернего объекта
                    GameObject newEnemy = Instantiate(enemyPrefab, randomChild.position, randomChild.rotation);
                    currentEnemies++;
                }

                // Увеличиваем сложность со временем, уменьшая промежуток между спаунами
                spawnInterval *= 0.9f;

                // Ожидаем заданный промежуток времени перед следующим спауном
                yield return new WaitForSeconds(spawnInterval);
            }
            else
            {
                // Если достигнуто максимальное количество врагов на сцене, ждем некоторое время
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
        // Получаем все дочерние объекты
        Transform[] childObjects = GetComponentsInChildren<Transform>();

        // Исключаем родительский объект из выборки
        List<Transform> validChildren = new List<Transform>();
        foreach (Transform child in childObjects)
        {
            if (child != transform)
            {
                validChildren.Add(child);
            }
        }

        // Выбираем случайный дочерний объект
        if (validChildren.Count > 0)
        {
            return validChildren[Random.Range(0, validChildren.Count)];
        }

        return null;
    }
}
