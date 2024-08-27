using System;
using TMPro;
using UnityEngine;


public class TimedSelfDestruct : MonoBehaviour
{
    // Total lifetime of current platform
    private float lifeTime = 10f;

    // Time when this platform should spawn another one
    private float reproduceTime = 4f;

    // Whether it has spawned another platform or not
    private bool hasReproduced = false;

    // The timer showing the remaining time
    private TextMeshPro timerDisplay;

    // Visble & editable in Inpector
    // Hold global game state
    private GlobalGameState gameState;

    void Start()
    {
        // Get the Global game state
        gameState = GameObject.FindWithTag("GlobalGameState").GetComponent<GlobalGameState>();
        
        // Push the current object's position into the 
        // positions queue which is maintained inside GlobalGameState
        gameState.positions.Enqueue(gameObject.transform.position);
        
        // Get the min and max lifetime of platform
        float minTime = gameState.game_data.pulpit_data.min_pulpit_destroy_time;
        float maxTime = gameState.game_data.pulpit_data.max_pulpit_destroy_time;
        
        // Set the lifetime
        lifeTime = UnityEngine.Random.Range(minTime, maxTime);
        
        // Set the reproduce time
        // Since the timer is running backwards not forwards
        // The actual reproduce time comes after subtracting the 
        // given time from lifeTime
        reproduceTime = lifeTime - gameState.game_data.pulpit_data.pulpit_spawn_time;
        
        // To display the remaining time
        timerDisplay = GetComponentInChildren<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update the Display to show remaining time
        timerDisplay.text = Math.Round(lifeTime, 2).ToString();
        
        // This will cause platform to shrink at rapid
        // rate rather than vanishing when the life time
        // is less than 0.94 (this makes it challenging)
        if(lifeTime < 0.94)
        {
            Vector3 scale = gameObject.transform.localScale;
            if(scale.x > 0 && scale.y > 0 && scale.z > 0) 
                gameObject.transform.localScale -= new Vector3(0.01f + lifeTime/20.0f, 0, 0.01f + lifeTime/20.0f);
        }
        // If the current platform has not yet reproduced and
        // it has entered in reproducing time and
        // lifeTime is not zero then reproduce and set
        // hasReproduced to true
        if (!hasReproduced && lifeTime <= reproduceTime && lifeTime > 0.0f)
        {
            reproduce();
            hasReproduced = true;
        }
        // Else call timer ended function
        else if (lifeTime <= 0.0f)
        {
            timerEnded();
        }

        // Reduce the lifeTime
        lifeTime -= Time.deltaTime;
    }

    // Used to get unit vector in a random direction
    // choosen from 4 cardinal directions
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

    // Funtion to create new platform
    void reproduce()
    {
        // Get current position and decalre new position
        Vector3 currPos = gameObject.transform.position;
        Vector3 newPos;

        // These deltas on being added to current position
        // give new position vector. 
        int deltaX = 0;
        int deltaZ = 0;
        
        do
        {
            // Get the deltas aka random unit vector
            int[] deltas = getRandomDeltas();
            
            // Unit vector scaled by 9
            // This sets the appropriate distance
            // between the new and old platforms.
            deltaX = deltas[0] * 9;
            deltaZ = deltas[1] * 9;

            // Getting new postion vector by adding the detla
            // vector to old position vector
            newPos = new Vector3(currPos.x + deltaX, currPos.y, currPos.z + deltaZ);

        // If the vector is already in the positions queue (which
        // contains positions of last 2 platforms) then loop again
        // until we get a new vector which is not present in positions queue.
        } while (gameState.positions.Contains(newPos));

        // Instantiate platform with the new position vector
        Instantiate(Resources.Load("TimedSelfDestructFloor", typeof(GameObject)) as GameObject, newPos, Quaternion.identity);
    }

    // When the timer ends this function is called
    // This destroys the current object. While also 
    // removing the old positions from the positions queue
    // from the GlobalGameState as now those platforms no longer exits
    void timerEnded()
    {
        // Removing the old objects
        if (gameState.positions.Count > 2) gameState.positions.Dequeue();
        
        // Destroying this one
        Destroy(gameObject);
    }
}
