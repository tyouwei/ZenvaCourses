using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public ObjectPool obstaclePool;
    public float minSpawnY;
    public float maxSpawnY;
    public float leftSpawnX;
    public float rightSpawnX;
    public float spawnRate;
    private float lastSpawnTime;

    private void Start()
    {
        Camera cam = Camera.main;
        float camWidth = (2.0f * cam.orthographicSize * cam.aspect);
        leftSpawnX = -camWidth / 2;
        rightSpawnX = camWidth / 2;
    }
    void Update()
    {
        if (Time.time - spawnRate >= lastSpawnTime)
        {
            lastSpawnTime = Time.time;
            SpawnObstacle();
        }
    }
    void SpawnObstacle()
    {
        GameObject obstacle = obstaclePool.GetPooledObject();
        obstacle.transform.position = GetSpawnPosition();
        obstacle.GetComponent<Obstacle>().moveDir = new Vector3(obstacle.transform.position.x > 0 ? -1 : 1, 0, 0);
    }
    Vector3 GetSpawnPosition()
    {
        float x = Random.Range(0, 2) == 1 ? leftSpawnX : rightSpawnX;
        float y = Random.Range(minSpawnY, maxSpawnY);
        return new Vector3(x, y, 0);
    }
}
