using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main_menu_controller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
        Debug.Log("Exit Game");
        Application.Quit();
    }
}
