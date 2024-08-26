using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class PlayerData
{
    public float speed;
}

[System.Serializable]
public class PulpitData
{
    public float min_pulpit_destroy_time;
    public float max_pulpit_destroy_time;
    public float pulpit_spawn_time;
}

[System.Serializable]
public class GameData
{
    public PlayerData player_data;
    public PulpitData pulpit_data;
}

public class GlobalGameState : MonoBehaviour
{
    public int score = 0;
    public Queue<Vector3> positions = new Queue<Vector3>();
    public GameData game_data;
    // Start is called before the first frame update
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
    void Start()
    {
        loadGameData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
