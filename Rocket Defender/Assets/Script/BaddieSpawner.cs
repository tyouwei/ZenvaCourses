using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaddieSpawner : MonoBehaviour
{
    [SerializeField]
    private Baddie baddieToSpawn;
    [SerializeField]
    private float spawnRegionWidth;
    [SerializeField]
    private Bases bases;

    public IEnumerator StartSpawning()
    {
        SpawnBaddie();
        yield return new WaitForSeconds(Random.Range(1f, 3f));
        StartCoroutine(StartSpawning());
    }
    public void Stop()
    {
        StopAllCoroutines();
    }
    private Vector3 GetRandomPosition()
    {
        float randomXPosition = Random.Range(-spawnRegionWidth, spawnRegionWidth);
        return new Vector3(randomXPosition, transform.position.y, transform.position.z);
    }
    void SpawnBaddie()
    {
        Baddie baddieInstance = Instantiate(baddieToSpawn, GetRandomPosition(), Quaternion.identity);
        baddieInstance.AssignTarget(bases.GetRandomBase(), Random.Range(1f, 3f));
    }
    private void Start()
    {
        StartCoroutine(StartSpawning());
    }
}
