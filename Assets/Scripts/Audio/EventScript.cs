using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventScript : MonoBehaviour
{

    [SerializeField]
    string m_eventName;

    public void OnClick()
    {
        AkSoundEngine.PostEvent(m_eventName, gameObject);
    }
   
}
