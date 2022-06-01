using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class select_objects : MonoBehaviour
{
    public Camera Levelcam;
    Ray ray;
    RaycastHit hit;

    public LayerMask Selectable;
    public bool IsInMenu = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)&&!IsInMenu)
        {
            ray = Levelcam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100, Selectable))
            {
                Debug.Log("hit");
                if (hit.transform.gameObject.CompareTag("Enemy"))
                {
                    hit.transform.gameObject.GetComponent<load_main_menu>().loadmenu();
                }
                else if (hit.transform.gameObject.CompareTag("Gadget"))
                {

                }
            }
        }
    }
}
