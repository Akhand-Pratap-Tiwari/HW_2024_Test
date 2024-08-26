using System.Collections.Generic;
using System.IO;
using UnityEngine;

// For player speed
// Default speed is given in JSON
[System.Serializable]
public class PlayerData
{
    public float speed;
}

// Platform lifetime details
[System.Serializable]
public class PulpitData
{
    public float min_pulpit_destroy_time;
    public float max_pulpit_destroy_time;
    public float pulpit_spawn_time;
}

// Combining both above class into a
// single container class
[System.Serializable]
public class GameData
{
    public PlayerData player_data;
    public PulpitData pulpit_data;
}

public class GlobalGameState : MonoBehaviour
{

    public MidGamePause midGamePause;

    public int score = 0;

    // This will hold last 2 platform positions 
    // This will be used to avoid spawning in same place
    public Queue<Vector3> positions = new Queue<Vector3>();

    // This will contain the imported JSON game data
    public GameData game_data;

    // Will be used to read and load game data into game_data variable
    void loadGameData() {
        string path = Path.Combine("Assets/Resources/doofusDiary.json");
        print(path);

        if (File.Exists(path))
        {
            // Read the entire file and save its contents as a string
            string jsonString = File.ReadAllText(path);

            // Deserialize the JSON string into a GameData object
            game_data = JsonUtility.FromJson<GameData>(jsonString);

            // Use the loaded data (for example, print it to the console)
            // Debug Printing is necessary here as file loading caused error on first
            // time opening. Cause unknown. After that error never happened again.
            // Might help if future if it ever happens.
            print("Player Speed: " + game_data.player_data.speed);
            print("Min Pulpit Destroy Time: " + game_data.pulpit_data.min_pulpit_destroy_time);
            print("Max Pulpit Destroy Time: " + game_data.pulpit_data.max_pulpit_destroy_time);
            print("Pulpit Spawn Time: " + game_data.pulpit_data.pulpit_spawn_time);
        }
        else
        {
            print("Cannot find gameData.json file!");
        }
    }

    // Once the game starts first thing is to load the game data.
    void Start()
    {
        loadGameData();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            midGamePause.Setup(score);
        }
    }
}
