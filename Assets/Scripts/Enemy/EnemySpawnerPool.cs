using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerPool : MonoBehaviour
{
    // Singleton instance
    public static EnemySpawnerPool instance;

    // Inspector fields
    [SerializeField] private Transform spawnTransform;
    [SerializeField] private List<GameObject> _enemies;

    // Public fields
    public int maxEnemyCount;
    public float spawnInterval;
    public int currentEnemyCount;

    // Private fields
    private float timeCounter;

    // Props
    public List<GameObject> Enemies => _enemies;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        StartCoroutine(nameof(InstantiateEnemy));
    }

    private void Update()
    {
        timeCounter += Time.deltaTime;

        if(timeCounter >= spawnInterval)
        {
            StartCoroutine(nameof(InstantiateEnemy));
            timeCounter = 0f;
        }
    }

    private IEnumerator InstantiateEnemy()
    {
        for (int i = 0; i < maxEnemyCount; i++)
        {
            if (currentEnemyCount >= maxEnemyCount) break;

            int randomIndex = Random.Range(0, Enemies.Count);
            var newGarbage = Instantiate(Enemies[randomIndex], transform);
            newGarbage.transform.position = GetRandomSpawnPosition();
            yield return new WaitForEndOfFrame();
            currentEnemyCount++;
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float newX = Random.Range(-spawnTransform.position.x, spawnTransform.position.x);
        return new Vector3(newX, spawnTransform.position.y, spawnTransform.position.z);
    }
}
