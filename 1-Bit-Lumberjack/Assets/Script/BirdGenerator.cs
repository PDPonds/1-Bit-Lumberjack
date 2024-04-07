using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdGenerator : Singleton<BirdGenerator>
{
    [SerializeField] GameObject birdPrefab;

    [Header("===== Transform =====")]
    public Transform moveLeftPos;
    public Transform moveRightPos;
    public Transform spawnAndExitLeftPos;
    public Transform spawnAndExitRightPos;

    [HideInInspector] public Bird curBird;

    private void Start()
    {
        SpawnBird();
    }

    public void SpawnBird()
    {
        Transform spawnPos = RandomSpawnPos();
        GameObject birdObj = Instantiate(birdPrefab, spawnPos.position, Quaternion.identity);
        Bird bird = birdObj.GetComponent<Bird>();
        bird.OnSpawn();

        if (spawnPos == spawnAndExitLeftPos)
        {
            bird.SwitchBehavior(BirdBehavior.MoveRight);
        }
        else if (spawnPos == spawnAndExitRightPos)
        {
            bird.SwitchBehavior(BirdBehavior.MoveLeft);
        }

    }

    Transform RandomSpawnPos()
    {
        int ran = Random.Range(0, 11);
        if (ran > 5)
        {
            return spawnAndExitLeftPos.transform;
        }
        else
        {
            return spawnAndExitRightPos.transform;
        }
    }

}
