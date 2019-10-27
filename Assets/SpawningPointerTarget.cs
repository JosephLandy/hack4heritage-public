using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Valve.VR.InteractionSystem

public class SpawningPointerTarget : MonoBehaviour
{
    public GameObject createMe; // prefab to instantiate, corresponding to the object of this menu item.
    int creationIndex;
    SystemManager sm;
    //private Transform player;
    //private Transform hand; // we want to instantiate the object selected at the location of the players hand. 

    private void Awake() {
        sm = GameObject.Find("SystemManager").GetComponent<SystemManager>();
        //player = GameObject.Find("Player").transform;
        //hand = player.Find("SteamVRObjects/RightHand");
        for (int i = 0; i < sm.prefabs.Count; i++) {
            if (sm.prefabs[i] == createMe) {
                creationIndex = i;
            }
        }
    }

    public void OnHit() {

        Debug.Log("instantiating " + gameObject.name);

        if (createMe != null) {
            sm.createNewAnimatable(creationIndex);
        }
    }
}
