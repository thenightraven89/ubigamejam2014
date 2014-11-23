using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    private float minWaitBetweenMobs = 0.25f;
    private float maxWaitBetweenMobs = 0.75f;

    private int currentWaveId;

    private MonsterBatch[] batches;

    public Transform[] spawners;

    private int spawnerCursor;

    private int waveCount = 2;

    private float waveCooldownTime = 5f;

    // Use this for initialization
    void Start()
    {
        batches = GetComponents<MonsterBatch>();

        currentWaveId = 0;

        spawnerCursor = 0;
        
        StartCoroutine("Spawn");
    }

    private IEnumerator Spawn()
    {
        while (currentWaveId < waveCount)
        {
            yield return new WaitForSeconds(waveCooldownTime);

            while (!WaveIsFinished(currentWaveId))
            {
                SpawnFromWave(currentWaveId);
                yield return new WaitForSeconds(Random.Range(minWaitBetweenMobs, maxWaitBetweenMobs));
            }

            Debug.Log("wave ended");
            
            currentWaveId++;
        }

        Debug.Log("THE END");
    }

    private void SpawnFromWave(int currentWaveId)
    {
        for (int i = 0; i < batches.Length; i++)
        {
            if (batches[i].waveId == currentWaveId && batches[i].IsAvailable())
            {
                spawnerCursor = (spawnerCursor + 1) % spawners.Length;
                GameObject newMob = GameObject.Instantiate(batches[i].sourceObject, spawners[spawnerCursor].position, Quaternion.identity) as GameObject;
                newMob.transform.parent = transform;
                batches[i].UseMonster();
            }
        }
    }

    private bool WaveIsFinished(int currentWaveId)
    {
        for (int i = 0; i < batches.Length; i++)
        {
            if (batches[i].waveId == currentWaveId && batches[i].IsAvailable())
            {
                return false;
            }
        }

        return true;
    }
}