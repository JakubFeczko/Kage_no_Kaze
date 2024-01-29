using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Sets up and manages the scene configurations and player stats for each level.
/// </summary>
public class SetUpScene : MonoBehaviour
{
    [SerializeField]
    public int level;

    /// <summary>
    /// Initializes default player stats for level 1 on awake.
    /// </summary>
    private void Awake()
    {
        if (level == 1)
        {
            PlayerStatsConfig playerStats = new PlayerStatsConfig
            {
                Health = 100,
                Stamina = 10,
                Xp = 0,
                XpPoint = 0,
                MaxHealth = 100,
                MaxStamina = 10
            };

            Save2Json(playerStats, Application.persistentDataPath + "/PlayerStatsLevel1.json");
        }
    
    }

    private void Start()
    {

    }

    /// <summary>
    /// Saves player stats to a JSON file.
    /// </summary>
    /// <param name="playerStats">Player stats configuration to be saved.</param>
    /// <param name="path">File path for saving the JSON data.</param>
    private void Save2Json(PlayerStatsConfig playerStats, string path)
    {
        string json = JsonUtility.ToJson(playerStats);
        System.IO.File.WriteAllText(path, json);
    }

    /// <summary>
    /// Loads player stats from a JSON file.
    /// </summary>
    /// <param name="path">File path for loading the JSON data.</param>
    /// <returns>Loaded player stats configuration.</returns>
    private PlayerStatsConfig LoadFromJson(string path)
    {
        if (System.IO.File.Exists(path))
        {
            string json = System.IO.File.ReadAllText(path);
            return JsonUtility.FromJson<PlayerStatsConfig>(json);
        }
        else
        {
            Debug.LogError("File not exist: " + path);
            return null;
        }
    }

    /// <summary>
    /// Saves player stats for the next scene based on the current level.
    /// </summary>
    /// <param name="statsConfig">Player stats configuration to be saved for the next scene.</param>
    public void SaveStatToNextScene(PlayerStatsConfig statsConfig)
    {
        switch (level)
        {
            case 1:
                Save2Json(statsConfig, Application.persistentDataPath + "/PlayerStatsLevel2.json");
                break;
            case 2:
                Save2Json(statsConfig, Application.persistentDataPath + "/PlayerStatsLevel3.json");
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Retrieves the player stats configuration for the current level.
    /// </summary>
    /// <returns>The loaded player stats configuration.</returns>
    public PlayerStatsConfig GetPlayerStatsConfig()
    {
        switch (level)
        {
            case 1:
                return LoadFromJson(Application.persistentDataPath + "/PlayerStatsLevel1.json");
            case 2:
                return LoadFromJson(Application.persistentDataPath + "/PlayerStatsLevel2.json");
            case 3:
                return LoadFromJson(Application.persistentDataPath + "/PlayerStatsLevel3.json");
            default:
                return null;
        }
    }

    /// <summary>
    /// Saves scene data to a JSON file.
    /// </summary>
    /// <param name="sceneData">Scene data to be saved.</param>
    /// <param name="path">File path for saving the JSON data.</param>
    private void SaveSceneDataToJson(SceneData sceneData, string path)
    {
        string json = JsonUtility.ToJson(sceneData);
        System.IO.File.WriteAllText(path, json);
    }

    /// <summary>
    /// Loads scene data from a JSON file.
    /// </summary>
    /// <param name="path">File path for loading the JSON data.</param>
    /// <returns>Loaded scene data.</returns>
    private SceneData LoadSceneDataFromJson(string path)
    {
        if (System.IO.File.Exists(path))
        {
            string json = System.IO.File.ReadAllText(path);
            return JsonUtility.FromJson<SceneData>(json);
        }
        else
        {
            Debug.LogError("File not exist: " + path);
            return new SceneData();
        }
    }

    /// <summary>
    /// Saves the completion status of a level to a JSON file.
    /// </summary>
    /// <param name="level">Level number to be saved.</param>
    public void SaveSceneData(int level)
    {
        SceneData sceneData = new SceneData
        {
            SceneIndex = level,
            IsSceneCompleted = true
        };

        switch (level)
        {
            case 1:
                SaveSceneDataToJson(sceneData, Application.persistentDataPath + "/SceneDataLevel1.json");
                break;
            case 2:
                SaveSceneDataToJson(sceneData, Application.persistentDataPath + "/SceneDataLevel2.json");
                break;
            case 3:
                SaveSceneDataToJson(sceneData, Application.persistentDataPath + "/SceneDataLevel3.json");
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Resets the scene data for all levels to the initial state.
    /// </summary>
    private void SaveSceneData()
    {
        
        SaveSceneDataToJson(new SceneData { SceneIndex = 1, IsSceneCompleted = false }, Application.persistentDataPath + "/SceneDataLevel1.json");
        SaveSceneDataToJson(new SceneData { SceneIndex = 2, IsSceneCompleted = false }, Application.persistentDataPath + "/SceneDataLevel2.json");
        SaveSceneDataToJson(new SceneData { SceneIndex = 3, IsSceneCompleted = false }, Application.persistentDataPath + "/SceneDataLevel3.json");
    }

    /// <summary>
    /// Gets the scene data to determine the last completed level.
    /// </summary>
    /// <returns>The data of the last completed level.</returns>
    public SceneData GetSceneData()
    {
        SceneData sceneOne = LoadSceneDataFromJson(Application.persistentDataPath + "/SceneDataLevel1.json");
        SceneData sceneTwo = LoadSceneDataFromJson(Application.persistentDataPath + "/SceneDataLevel2.json");
        SceneData sceneThree = LoadSceneDataFromJson(Application.persistentDataPath + "/SceneDataLevel3.json");

        if(sceneThree != null  && sceneThree.IsSceneCompleted == true){return sceneThree;}
        else if(sceneTwo != null && sceneTwo.IsSceneCompleted == true){return sceneTwo;}
        else if(sceneOne != null && sceneOne.IsSceneCompleted == true){return sceneOne;}
        else{return null;}
    }

    /// <summary>
    /// Resets all level data to start from the beginning.
    /// </summary>
    public void RestartLevels()
    {
        SaveSceneData();
    }
}
