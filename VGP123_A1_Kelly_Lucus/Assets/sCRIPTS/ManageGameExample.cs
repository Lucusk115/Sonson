using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ManageGameExample : MonoBehaviour
{

    static ManageGameExample _instance = null;


    public static ManageGameExample Instance
    {
        get { return _instance; }
        //set { _instance = value; } 

    }

    public void endGame()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void restart()
    {
        SceneManager.LoadScene("Level1");
    }



    private void Awake()
    {
        if (_instance) //ensures game manager is called then destroyed 
        {
            DestroyImmediate(gameObject); // Destroys current game manager
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            _instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.L))
        {
            SceneManager.LoadScene("Level1", LoadSceneMode.Additive);
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene("TitleScreen");
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
                        Application.Quit();
#endif
        }
    }
}      




