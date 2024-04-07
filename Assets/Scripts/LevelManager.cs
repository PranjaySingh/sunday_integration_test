using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string[] levelSceneNames;
    private int currentLevelIndex = 0;

    public static LevelManager Instance;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadNextLevel()
    {

        currentLevelIndex++;
        if (currentLevelIndex < levelSceneNames.Length)
        {
            Loadlevel(currentLevelIndex);
        }
        else
        {
            currentLevelIndex = 0;
            Loadlevel(currentLevelIndex);
        }
        Debug.Log("Congrats, Loading next level :" + levelSceneNames[currentLevelIndex]);
    }

    public void ReloadLevel()
    {
        Loadlevel(currentLevelIndex);
        Debug.Log("Oops, Reloading level:" + levelSceneNames[currentLevelIndex]);
    }

    private void Loadlevel(int index)
    {
        SceneManager.LoadScene(levelSceneNames[index]);
    }
}
