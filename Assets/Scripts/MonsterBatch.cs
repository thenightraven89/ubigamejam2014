using UnityEngine;
using System.Collections;

public class MonsterBatch : MonoBehaviour
{
    // id of wave that the batch participates in
    public int waveId;

    // source monster to clone
    public GameObject sourceObject;

    // amount of monsters of this type participating in this wave
    public int monsterCount;

    // this will update over time (decrease)
    private int remainingCount;

    public void Awake()
    {
        remainingCount = monsterCount;
    }

    public bool IsAvailable()
    {
        return remainingCount > 0;
    }

    public void UseMonster()
    {
        remainingCount--;
    }
}
