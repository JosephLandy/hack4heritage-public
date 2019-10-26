using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxController : MonoBehaviour
{
    // Start is called before the first frame update


    //public Material[] skyboxes;

    public List<Material> skyboxes;

    private int _skyboxIndex = 0;
    public int SkyboxIndex {
        get {
            return _skyboxIndex;
        }
        set {
            if (value >= 0 && value < skyboxes.Count) {
                _skyboxIndex = value;
                RenderSettings.skybox = skyboxes[value];
            }
        }
    }

    void Start() {
        SkyboxIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.S)) {
            SkyboxIndex = (_skyboxIndex + 1) % skyboxes.Count;
       } 
    }
}
