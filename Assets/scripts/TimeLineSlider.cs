using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLineSlider : MonoBehaviour
{

    SystemManager systemManager;
    Valve.VR.InteractionSystem.LinearMapping linearMapping;
    public bool overiding = false;
    // Start is called before the first frame update
    void Start()
    {
        systemManager = GameObject.Find("SystemManager").GetComponent<SystemManager>();
        linearMapping =  this.GetComponent<Valve.VR.InteractionSystem.LinearMapping>();
    }

    // Update is called once per frame
    void Update()
    {
        if (overiding)
        {
            systemManager.currentTime = linearMapping.value * systemManager.sceenData.sceenLenght;
        }
        else
        {
            linearMapping.value = systemManager.currentTime / systemManager.sceenData.sceenLenght;
        }
    }
    public void held()
    {
        overiding = true;
    }
    public void notheld()
    {
        overiding = false;
    }
}
