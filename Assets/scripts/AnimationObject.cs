using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class AnimationObject : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    SystemManager systemManager;
    [SerializeField]
    public bool isBeingHeld = false;

    public SteamVR_Input_Sources handType; 
    public SteamVR_Action_Boolean MenuButton; 
    void Start()
    {
        systemManager = GameObject.Find("SystemManager").GetComponent<SystemManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isBeingHeld)
        {
            if (GetMenuDown() || Input.GetKeyDown(KeyCode.V))
            {
                Debug.Log("saveing keyframe");
                systemManager.addAnimationFrame(this.gameObject);
             }
        }
    }
    public void pickedup()
    {
        isBeingHeld = true;
    }
    public void droped()
    {
        isBeingHeld = false;
    }



    public bool GetMenuDown() // 1
    {
        return MenuButton.GetStateDown(handType);
    }
}
