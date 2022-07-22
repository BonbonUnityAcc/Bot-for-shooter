using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerUnit : MonoBehaviour
{
    public GameObject player;
    public GameObject monster;
    public GameObject[] spawnPositions;
    private void Start()
    {
        Instantiate(player, spawnPositions[Random.Range(0, spawnPositions.Length)].transform);
        Instantiate(monster, spawnPositions[Random.Range(0, spawnPositions.Length)].transform);
    }
}
