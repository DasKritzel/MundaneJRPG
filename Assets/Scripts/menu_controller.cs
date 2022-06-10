using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu_controller : MonoBehaviour
{
    public GameObject RoomObjects;
    public GameObject Pause_menu;
    public select_objects Clickables;

    public static GameObject RoomScene;
    // Start is called before the first frame update
    void Start()
    {
        RoomScene = RoomObjects;
        Close_Ingame_Menu();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
         Pause_menu.SetActive(true);
            Clickables.IsInMenu = true;
        }
    }

    /// <summary>
    /// closes the game
    /// </summary>
    public void Exit_game()
    {
#if UNITY_EDITOR
        Debug.Log("Exit Game");
#endif
        Application.Quit();
    }


    public void Close_Ingame_Menu()
    {
     Pause_menu.SetActive(false);
        Clickables.IsInMenu = false;
    }
}
