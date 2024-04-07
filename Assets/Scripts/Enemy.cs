using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public int currentLevel = 1;
    public string sceneToLoad = "Level1";

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "PlayerBall")
        {
            MyEventSystem.I.FailLevel(currentLevel);
            FirebaseEvents.Instance.FailLevel(currentLevel);                    //pj was here : triggering a Firebase event.

            //SceneManager.LoadScene(sceneToLoad);
            LevelManager.Instance.ReloadLevel();                                //pj was here : Using LevelManager to dynamically load levels
        }
    }
}
