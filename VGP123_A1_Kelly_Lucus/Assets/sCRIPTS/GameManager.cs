using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Must be added if using SceneManager functions
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    // Creates a class variable to keep track of 'GameManager' instance
    static GameManager _instance = null;

    // Used to keep track of 'score' in game
    int _score;

    // Used to instantiate 'Character'
    public GameObject playerPrefab;
    public int playerLives;
    
    
    // Use this for initialization
    void Start () {

        // Check if 'GameManager' instance exists
        if (instance)
            // 'GameManager' already exists, delete copy
            Destroy(gameObject);
        else
        {
            // 'GameManager' does not exist so assign a reference to it
            instance = this;

            // Do not destroy 'GameManager' on Scene change
            DontDestroyOnLoad(this);
        }

        // Assign a starting score
        score = 0;
	}
	
	// Update is called once per frame
	void Update () {

        // Check if 'Escape' was pressed
        if (Input.GetKeyDown(KeyCode.L))
        {
            // If player is on 'Screen_Title' (Scene Name)
            if (SceneManager.GetActiveScene().name == "Titlescreen")
            {
                // Go to 'Level1' Scene
                // - Scene must be loaded in Build Settings or it will not work
                // - Build Settings are located at Menu Bar: Edit->Build Settings
                // - Drag the Scenes in the project into 'Scenes in Build' space
                playerLives = 3;
                SceneManager.LoadScene("Level1");
            }
            // If player is on 'Level1' (Scene Name)
            else if (SceneManager.GetActiveScene().name == "Level1")
                // Go to 'Screen_Title' Scene
                // - Scene must be loaded in Build Settings or it will not work
                // - Build Settings are located at Menu Bar: Edit->Build Settings
                // - Drag the Scenes in the project into 'Scenes in Build' space
                SceneManager.LoadScene("Titlescreen");

            else if (SceneManager.GetActiveScene().name == "GameOver")
            {
                SceneManager.LoadScene("Titlescreen");
            }
        }

	}

    // Called when 'Character' is spawned
    public void spawnPlayer(int spawnLocation)
    //public void spawnPlayer(Transform spawnLocation)
    //public void spawnPlayer(Vector3 spawnLocation)
    //public void spawnPlayer(GameObject spawnLocation)
    {
        // Requires spawnPoint to be named (SceneName)_(number)
        // - Level1_0
        string spawnPointName = SceneManager.GetActiveScene().name
            + "_" + spawnLocation;

        // Find location to spawn 'Character' at
        Transform spawnPointTransform = 
            GameObject.Find(spawnPointName).GetComponent<Transform>();

        // Check if 'playerPrefab' and 'spawnPointTransform' exist
        if (playerPrefab && spawnPointTransform)
        {
            // Instantiate (Create) 'Character' GameObject
            Instantiate(playerPrefab, spawnPointTransform.position,
                spawnPointTransform.rotation);
        }
        else
            // Prints a message to Console (Shortcut: Control+Shift+C)
            Debug.LogError("Missing Player Prefab or SpawnPoint");

    }

    // Give access to private variables (instance variables)
    // - Not needed if using public variables
    // - Variable must be declared above
    // - Variable and method must be static
    public static GameManager instance
    {
        get { return _instance; }   // can also use just 'get;'
        set { _instance = value; }  // can also use just 'set;'
    }

    // Give access to private variables (instance variables)
    // - Not needed if using public variables
    public int score
    {
        get { return _score; }      // can also use just 'get;'
        set { _score = value; }     // can also use just 'set;'
    }

   

    public void endGame()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void restart()
    {
        SceneManager.LoadScene("Level1");
    }

    public static void KillPlayer (playerControls player)
    {
        Destroy(player.gameObject);
        
    }
}
