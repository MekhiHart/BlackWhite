using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // *Private
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject mainBackground;
    int currentLevel;

    // * Public
    public static GameManager instance;
    public string currentColor;

    string levelName = "Level_";

    private void Start() {
        currentLevel = 1;
    }


    private void OnEnable() {
        SceneManager.sceneLoaded+= SceneLoaded;
        Portal.nextLevel += Load_NextScene;
        PlayerController.restartLevel += RestartLevel;
        
    } // * OnEnable

    private void Awake() {
        if (instance != null){ // * Destroys duplicate of GameManager
            Destroy(gameObject);
        }
        else{
            instance = this; // * Only executes once 
            DontDestroyOnLoad(gameObject);
        }
    } // * Awake

    private void Load_NextScene(){ // * Connected Event from Portal Script to change levels
        currentLevel += 1;
        SceneManager.LoadScene(levelName + currentLevel.ToString());
        mainBackground.GetComponent<ColorController>().ResetColor(); // * Resets the color of background 
    } // * Load_NextScene

    private void SceneLoaded(Scene scene, LoadSceneMode mode){ // * Need to know these parameters to subcribe to sceneLoaded
        DontDestroyOnLoad(mainBackground);
        DontDestroyOnLoad(mainCamera);
        GameObject[] gameObjects = scene.GetRootGameObjects(); // * Gets game objects of scene in an array
        foreach (GameObject obj in gameObjects){
            if (obj.name == "Main Camera"){
                Destroy(obj); // ! Destroys the Main Camera in that loaded scene (Solves issue for when player dies in level 1 and also helps out when testing out scenes with maincamera)
            }
            else if (obj.name == "Background") Destroy(obj); // ! Destroys Scene Bacgkground (Solves issue for when player dies in level 1 )
        }
    } // * SceneLoaded

    private void RestartLevel(){ // * Reloads current scene player is in
        SceneManager.LoadScene(levelName + currentLevel.ToString());
        mainBackground.GetComponent<ColorController>().ResetColor(); // * Resets the color of background 

    }
        

}
