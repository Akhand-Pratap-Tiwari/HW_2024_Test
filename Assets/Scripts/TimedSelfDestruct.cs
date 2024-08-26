using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static Unity.VisualScripting.Metadata;

public class TimedSelfDestruct : MonoBehaviour
{
    public float lifeTime = 10f;
    public float reproduceTime = 4f;
    public bool hasReproduced = false;
    GlobalGameState gameState;

    void Start()
    {
        gameState = GameObject.FindWithTag("GlobalGameState").GetComponent<GlobalGameState>();
        gameState.positions.Enqueue(gameObject.transform.position);
        float minTime = gameState.game_data.pulpit_data.min_pulpit_destroy_time;
        float maxTime = gameState.game_data.pulpit_data.max_pulpit_destroy_time;
        lifeTime = UnityEngine.Random.Range(minTime, maxTime);
        reproduceTime = lifeTime - gameState.game_data.pulpit_data.pulpit_spawn_time;
    }

    // Update is called once per frame
    void Update()
    {
        //print(lifeTime);
        TextMeshPro child = GetComponentInChildren<TextMeshPro>();
        child.text = lifeTime.ToString();
        if (!hasReproduced && lifeTime <= reproduceTime && lifeTime > 0.0f)
        {
            reproduce();
            hasReproduced = true;
        }
        else if (lifeTime <= 0.0f)
        {
            timerEnded();
        }

        lifeTime -= Time.deltaTime;
    }
    int[] getRandomDeltas()
    {
        int[,] values = {
            {0, 1},
            {1, 0},
            {-1, 0},
            {0, -1}
        };
        int idx = UnityEngine.Random.Range(0, values.GetLength(0));
        int[] deltas = { values[idx, 0], values[idx, 1] };
        return deltas;
    }
    void reproduce()
    {
        Vector3 currPos = gameObject.transform.position;
        Vector3 newPos;
        int deltaX = 0;
        int deltaZ = 0;
        do
        {
            int[] deltas = getRandomDeltas();
            deltaX = deltas[0] * 9;
            deltaZ = deltas[1] * 9;
            newPos = new Vector3(currPos.x + deltaX, currPos.y, currPos.z + deltaZ);
        } while (gameState.positions.Contains(newPos));
        Instantiate(Resources.Load("TimedSelfDestructFloor", typeof(GameObject)) as GameObject, newPos, Quaternion.identity);
    }
    void timerEnded()
    {
        if (gameState.positions.Count > 2) gameState.positions.Dequeue();
        Destroy(gameObject);
    }
}
