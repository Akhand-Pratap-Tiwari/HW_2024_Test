using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreVolume : MonoBehaviour
{
    GlobalGameState gameState;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Player"))
        {
            gameState.score++;
            Destroy(gameObject);
        }
    }

    void Start()
    {
        gameState = GameObject.FindWithTag("GlobalGameState").GetComponent<GlobalGameState>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
