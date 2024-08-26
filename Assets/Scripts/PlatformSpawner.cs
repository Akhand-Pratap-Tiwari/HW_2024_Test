using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject prefab;
    public float targetTime = 10.0f;

    void Start()
    {
        prefab = GameObject.CreatePrimitive(PrimitiveType.Cube);
        prefab.transform.position = new Vector3(0, -1.5f, 0);
        prefab.transform.localScale = new Vector3(9, 0.5f, 9);
        Material newMat = Resources.Load("Materials/Green", typeof(Material)) as Material;
        prefab.GetComponent<Renderer>().material = newMat;
    }

    // Update is called once per frame
    void Update()
    {
        print(targetTime);
        if (targetTime <= 0.0f)
        {
            timerEnded();
        }
        else
        {
            targetTime -= Time.deltaTime;
        }

    }

    void timerEnded()
    {
        prefab = null;
        Destroy(prefab);
    }

}
