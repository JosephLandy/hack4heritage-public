using System.Collections.Generic;
using UnityEngine;

public class SpawningInterface : MonoBehaviour
{

    public SystemManager sm;

    public List<GameObject> props;
    // Start is called before the first frame update
    void Start()
    {
        sm = GameObject.Find("SystemManager").GetComponent<SystemManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
