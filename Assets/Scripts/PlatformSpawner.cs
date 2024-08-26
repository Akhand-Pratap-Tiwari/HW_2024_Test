using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject prefab;
    public float targetTime = 10.0f;
    public float timeLimit = 5f;
    GameObject floorInstance;
    void Start()
    {
        prefab = Resources.Load("Floor", typeof(GameObject)) as GameObject;
        Vector3 position = new Vector3(0, -1.5f, 0);
        floorInstance = Instantiate(prefab, position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        print(targetTime);
        if (targetTime <= timeLimit && targetTime > 0.0f)
        {

        }
        else if (targetTime <= 0.0f)
        {
            timerEnded();
        }

        targetTime -= Time.deltaTime;


    }

    void timerEnded()
    {
        Destroy(gameObject);
    }

}
