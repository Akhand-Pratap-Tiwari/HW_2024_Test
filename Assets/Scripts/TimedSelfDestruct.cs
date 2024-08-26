using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TimedSelfDestruct : MonoBehaviour
{
    public float lifeTime = 10f;
    public float reproduceTime = 5f;
    public bool hasReproduced = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //print(lifeTime);
        if (!hasReproduced && lifeTime <= reproduceTime && lifeTime > 0.0f)
        {
            reproduce();
            hasReproduced=true;
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
        GameObject[] floorsWithTag = GameObject.FindGameObjectsWithTag("SelfDestructingFloor");
        List<Vector3> positions = new List<Vector3>();
        foreach (GameObject selfDesFloor in floorsWithTag)
        {
            positions.Add(selfDesFloor.transform.position);
            print("Position: " + selfDesFloor.transform.position);
        }
        //print(positions[0].x + positions[0].x + positions[0].x + positions[0].x + );
        do {
            deltaX = 0;
            deltaZ = 0;
            int[] deltas = getRandomDeltas();
            deltaX = deltas[0] * 9;
            deltaZ = deltas[1] * 9;
            newPos = new Vector3(currPos.x + deltaX, currPos.y, currPos.z + deltaZ);
        } while (positions.Contains(newPos));
        print(newPos.x + " " + newPos.z);
        Instantiate(Resources.Load("TimedSelfDestructFloor", typeof(GameObject)) as GameObject, newPos, Quaternion.identity);
    }
    void timerEnded()
    {
        Destroy(gameObject);
    }
}
