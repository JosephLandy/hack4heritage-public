using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Valve.VR.Extras;

public class ClickHandler : MonoBehaviour
{

    public SteamVR_LaserPointer laserPointer;
    SystemManager sm;

    void Awake() {
        sm = GameObject.Find("SystemManager").GetComponent<SystemManager>();
        //laserPointer.PointerIn += PointerInside;
        //laserPointer.PointerOut += PointerOutside;
        laserPointer.PointerClick += PointerClick;
    }

    public void PointerClick(object sender, PointerEventArgs e) {
        SpawningPointerTarget t = e.target.GetComponent<SpawningPointerTarget>();
        if (t != null) {
            t.OnHit();
        }

    }

    //public void PointerInside(object sender, PointerEventArgs e) {
    //    if (e.target.name == "Cube") {
    //        Debug.Log("Cube was entered");
    //    } else if (e.target.name == "Button") {
    //        Debug.Log("Button was entered");
    //    }
    //}

    //public void PointerOutside(object sender, PointerEventArgs e) {
    //    if (e.target.name == "Cube") {
    //        Debug.Log("Cube was exited");
    //    } else if (e.target.name == "Button") {
    //        Debug.Log("Button was exited");
    //    }
    //}


}
