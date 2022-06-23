using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main_menu_controller : MonoBehaviour
{
    public GameObject Menu;
    public GameObject Options;
    public GameObject Credits;
    // Start is called before the first frame update
    void Start()
    {
        OpenMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
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
    public void OpenOptions()
    {
        Menu.SetActive(false);
        Options.SetActive(true);

    }
    public void OpenMenu()
    {
        Menu.SetActive(true);
        Options.SetActive(false);
        Credits.SetActive(false);

    }
    public void OpenCredits()
    {
        Menu.SetActive(false);
        Credits.SetActive(true);

    }


}
