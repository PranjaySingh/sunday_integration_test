# Sunday Integration Test Project.
<br></br>

#### **Important :** _Once the project is cloned/ downloaded, please go to “Assets/Firebase/Plugins/x86_64/FirebaseCppApp-11_8_1.bundle.zip” and unzip/unpack the compressed bundle file before opening the unity project. I have reached my LFS data limit. Thus I am unable to track large files with git LFS._


__Unity Version :__ `2022.3.22f1`

__Link to Test Task Problem :__ https://gitlab.com/alireza27/sunday-unity-integration-developer-test
#
## Critical Issues

#### MyEventSystem Class: 
 - __Issue:__ 
 MyEventSystem class is unable to find GameAnalytics required to send level start and finish events. Although the GameAnalytics unitypackage containing the script for handling events has been imported, it is not functioning as expected.

-  __Solution:__
 `MyEventSystem.cs` was the one and only script placed inside an assembly Definition called `MyEventSystem.asmdef` and the assembly definition could not reference the GameAnalyticsSDK namespace. Simply placing the MyEventSystem.cs out of the assembly definition solved the issue.

    Another possible solution would be to simply remove the MyEventSystem assembly definition as it only contains a single script and creating an assembly definition for a single script is not very beneficial at least in this particular project.

#### Performance Issues:
 - __Issue:__ 
Despite the game having minimal objects and scripts, performance issues persist on mobile devices.

 - __Solution:__
 `BallRoller.cs` was trying to find the PlayerBall game object (`GameObject.Find(“PlayerBall”)`) in every frame update. Finding a GameObject in the scene every frame hinders the performance of the game. I fixed it by referencing the PlayerBall at the “Start” of the script and introduced a fail safe to only find the player ball in Update if the player ball looses the reference somehow.

    Another possible solution would be to simply attach the BallRoller.cs to the PlayerBall game object and then simply accessing the PlayerBall’s components using gameObject.GetComponent<>.

#### Frame Rate Dependency: 
 - __Issue:__ 
Controls behave inconsistently depending on the frames per second (FPS) the game is running at.

 - __Solution:__
Since `BallRoller.cs` uses physics and adds a torque to the player ball’s Rigidbody every frame in the Update method, this leads to the inconsistency of different resulting velocity of the player ball on devices with different fps. A simple fix for this is to use the `FixedUpdate` method instead of the Update method. FixedUpdate executes a fixed number of times in a certain time interval (50 times a second by default, this can be changed) rather than executing every frame. 

#### Git Repository: 
 - __Issue:__ 
There are issues with the git repository as numerous irrelevant files are included in pushes.

 - __Solution:__
To fix this, we should only commit and push the Assets and ProjectSettings folder to git. The other folders such as the Library folder, UserSettings folder contain data that Unity can easily regenerate and thus is not required to be pushed. The Library Folder should especially be avoided as it contains various data files that are larger than the accepted 100mb per file push limit in git. If in case you need to push larger files, it is recommended that you track them in git LFS i.e git Large File System.
#
#
## Bonus Issues

#### Firebase Analytics Integration:
 - __Issue:__ 
Firebase Analytics integration is missing. I need to send the same events (start, fail, and finish) tracked by Game Analytics to Firebase too. Additional docs: https://firebase.google.com/docs/analytics/unity/start

 - __Solution:__
FireBase Is Integrated successfully. 
App Package Name : `com.Sunday.IntegrationTestProject`
`FireBaseEvents.cs` contains the logic to send the “Start_Levelæ, “Fail_Level” and “Complete_Level” event logs to firebase

#### Level Management:
 - __Issue:__ 
Adding and managing new levels within the project is challenging.

 - __Solution:__
I Created a `LevelManager.cs` that takes the names of the level scenes in a list of strings. This Singleton script dynamically loads the next level in the list of level names if the current level is successfully completed or simply reloads the current level if the level fails. Once the last level is successfully completed, it then loads the level 1 again.
#
