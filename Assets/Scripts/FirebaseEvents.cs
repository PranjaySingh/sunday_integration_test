using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Extensions;
using Firebase;
using Firebase.Analytics;

public class FirebaseEvents : MonoBehaviour
{
    FirebaseApp app;
    public static FirebaseEvents Instance;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                app = FirebaseApp.DefaultInstance;

                // Set a flag here to indicate whether Firebase is ready to use by your app.
            }
            else
            {
                Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }

    public void StartLevel(int level)
    {
        Debug.Log("Firebase Event 'Start_level' sent");
        FirebaseAnalytics.LogEvent("Start_Level", new Parameter("Level:", level));
    }

    public void FailLevel(int level)
    {
        Debug.Log("Firebase Event 'Fail_level' sent");
        FirebaseAnalytics.LogEvent("Fail_Level", new Parameter("Level:", level));

    }

    public void CompleteLevel(int level)
    {
        Debug.Log("Firebase Event  'Complete_level' sent");
        FirebaseAnalytics.LogEvent("Complete_Level", new Parameter("Level:", level));
    }

}
