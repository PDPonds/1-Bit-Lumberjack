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

    [Header("===== Generate Setting =====")]
    [SerializeField] float minSpawnBirdDelayTime;
    [SerializeField] float maxSpawnBirdDelayTime;
    float curDelayTime;

    [Header("===== Bird =====")]
    public float birdGoBackTime;
    [HideInInspector] public Bird curBird;

    private void Start()
    {
        ResetDelayTime();
    }

    private void Update()
    {
        GenerateBird();
    }

    void GenerateBird()
    {
        if (curBird == null)
        {
            curDelayTime -= Time.deltaTime;
            if (curDelayTime < 0)
            {
                SpawnBird();
            }

        }
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

        curBird = bird;
    }

    public void ResetDelayTime()
    {
        float delayTime = Random.Range(minSpawnBirdDelayTime, maxSpawnBirdDelayTime);
        curDelayTime = delayTime;
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
