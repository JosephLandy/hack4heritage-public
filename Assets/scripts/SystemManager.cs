using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : MonoBehaviour
{

    [SerializeField]
    GameObject playerGameObject;


    // Start is called before the first frame update
    void Start()
    {
        if (playerGameObject == null)
        {
            Debug.LogError("please assign player gameobject");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.G))
        {
            playerGameObject.transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.B))
        {
            playerGameObject.transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime;
        }
    }
}
