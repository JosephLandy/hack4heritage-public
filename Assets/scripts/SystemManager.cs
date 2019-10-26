using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : MonoBehaviour
{

    [SerializeField]
    GameObject playerGameObject;

    //all the sceen data
    [SerializeField]
    SceenData sceenData;
    
    //prefabs by id in list 
    [SerializeField]
    List<GameObject> prefabs;

    [SerializeField]
    Transform propHolder;



    // Start is called before the first frame update
    void Start()
    {
        //set this so our sceen has soem length
        sceenData = new SceenData();
        sceenData.sceenLenght = 30;
        if (playerGameObject == null)
        {
            Debug.LogError("please assign player gameobject");
        }
        if (propHolder == null)
        {
            Debug.LogError("please assign prop holder object that ll props get as parent");
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
        if (Input.GetKeyDown(KeyCode.M))
        {
            saveData(1);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            //clear all sceen objects


            Debug.Log("loading data....");
            loadData(1);
            Debug.Log("Data loaded!");
            initilizeSceen();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            createNewAnimatable(0);
        }


    }
    //makes a new animatable object 
    public void createNewAnimatable(int idNum)
    {
        //this was so pretty on one line but debuging :(
        GameObject temp = Instantiate(prefabs[idNum], propHolder);
        AnimationData temp2 = new AnimationData(temp);
        temp2.assetIDNum = idNum;
        sceenData.objectAnimationData.Add(temp2);

    }
    //loops through all sceen objects and yeets them to the garbage collector 
    public void clearSceen()
    {
        foreach (AnimationData animatedObject in sceenData.objectAnimationData)
        {
            Destroy(animatedObject.instance);
        }
    }


    /// <summary>
    /// saves all the animation data and other data by seriliving to disk 
    /// </summary>
    public void saveData(int saveNumber)
    {
        BinarySerialization.WriteToBinaryFile(saveNumber.ToString(), "saveData" + saveNumber.ToString(), sceenData, false);
    }
    public void loadData(int saveNumber)
    {
        sceenData = BinarySerialization.ReadFromBinaryFile<SceenData>(saveNumber.ToString() + "/" + "saveData" + saveNumber.ToString());
    }
    public void initilizeSceen()
    {
        Debug.Log("intilizing sceen");
        clearSceen();//clean up first
        foreach (AnimationData animatedObject in sceenData.objectAnimationData)
        {
            GameObject newAnimatedGameobject = GameObject.Instantiate(prefabs[animatedObject.assetIDNum]);//instatiate asset
            //force to go to fist postion if there is one
            
            PlaceAbleObject newAnimatedGameobjectScript= newAnimatedGameobject.GetComponent<PlaceAbleObject>();//get sccript 
            if (newAnimatedGameobjectScript.animationData.animationPoints.Count > 0)
            {
                newAnimatedGameobjectScript.animationData = animatedObject;//set data
                newAnimatedGameobjectScript.currentAnimationIndex = 0;//set index to zero, probably not needed
                newAnimatedGameobjectScript.currentAnimationPoint = newAnimatedGameobjectScript.animationData.animationPoints[0];//set the first postion
                newAnimatedGameobjectScript.forceToCurrentAnimationPoint();//force it to go to the first point 
            }
        }
    }
    



}
