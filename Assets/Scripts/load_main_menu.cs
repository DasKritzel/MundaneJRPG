using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class load_main_menu : MonoBehaviour
{
    public string SceneName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadmenu()
    {
        Debug.Log("Ich lade jetzt die Scene " + SceneName);
        SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
    }

    public void loadbattle()
    {
        Debug.Log("Ich lade jetzt die Scene " + SceneName);
        SceneManager.LoadScene(SceneName, LoadSceneMode.Additive);
        menu_controller.RoomScene.SetActive(false);
    }

}
