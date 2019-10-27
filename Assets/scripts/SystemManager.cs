using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : MonoBehaviour
{

    [SerializeField]
    public float currentTime;
    [SerializeField]
    public bool isPlayingAnim;

    [SerializeField]
    GameObject playerGameObject;

    //all the sceen data
    [SerializeField]
    public SceenData sceenData;

    [SerializeField]
    public AudioData audioData;


    //prefabs by id in list 
    [SerializeField]
    public List<GameObject> prefabs;

    [SerializeField]
    public float isRecordingAudio =0;

    //[SerializeField]
    //Transform propHolder;

    // transform where newly instantiated props will be located, for example, at the hand of the player.
    public Transform propInstancePoint;

    // Start is called before the first frame update
    void Start()
    {
        //set this so our sceen has soem length
        audioData = new AudioData();
        sceenData = new SceenData();
        sceenData.sceenLenght = 60;
        if (playerGameObject == null)
        {
            Debug.LogError("please assign player gameobject");
        }
        //if (propHolder == null)
        //{
        //    Debug.LogError("please assign prop holder object that ll props get as parent");
        //}
       for(int i =0; i < prefabs.Count; i++)
        {
            createNewAnimatable(i);
            createNewAnimatable(i);
        }


    }

    // Update is called once per frame
    void Update()
    {
        if(isRecordingAudio > 0)
        {
            isRecordingAudio -= Time.deltaTime;
        }
        animateObjects();
        if (isPlayingAnim)
        {
            currentTime += Time.deltaTime;
            
        }
        if (Input.GetKey(KeyCode.O))
        {
            currentTime = 0;
        }
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
            loadSceen();
            
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            createNewAnimatable(0);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            startAudioRecording();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            playAudioRecording();
        }

    }
    public void loadSceen()
    {
        clearSceen();//clean up first
        Debug.Log("loading data....");
        loadData(1);
        Debug.Log("Data loaded!");
        initilizeSceen();
    }
    //makes a new animatable object 
    public void createNewAnimatable(int idNum)
    {
        //this was so pretty on one line but debuging :(
        //GameObject temp = Instantiate(prefabs[idNum], propHolder);
        GameObject temp;
        if (propInstancePoint != null) {
             temp = Instantiate(prefabs[idNum], propInstancePoint.position, Quaternion.identity);
        } else {
            temp = Instantiate(prefabs[idNum]);
        }
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
        audioData.CompressAndStore();
        BinarySerialization.WriteToBinaryFile(saveNumber.ToString(), "audioData" + saveNumber.ToString(), audioData, false);
        BinarySerialization.WriteToBinaryFile(saveNumber.ToString(), "saveData" + saveNumber.ToString(), sceenData, false);
    }
    public void loadData(int saveNumber)
    {
        audioData = BinarySerialization.ReadFromBinaryFile<AudioData>(saveNumber.ToString() + "/" + "audioData" + saveNumber.ToString());
        audioData.DecompressAndLoad();
        sceenData = BinarySerialization.ReadFromBinaryFile<SceenData>(saveNumber.ToString() + "/" + "saveData" + saveNumber.ToString());
    }
    public void initilizeSceen()
    {
        Debug.Log("intilizing sceen");
        currentTime = 0;
        
        foreach (AnimationData animatedObject in sceenData.objectAnimationData)
        {
            animatedObject.instance = GameObject.Instantiate(prefabs[animatedObject.assetIDNum]);//instatiate asset
            
            
            //force to go to fist postion if there is one
            
            /*PlaceAbleObject newAnimatedGameobjectScript= newAnimatedGameobject.GetComponent<PlaceAbleObject>();//get sccript 
            if (newAnimatedGameobjectScript.animationData.animationPoints.Count > 0)
            {
                newAnimatedGameobjectScript.animationData = animatedObject;//set data
                newAnimatedGameobjectScript.currentAnimationIndex = 0;//set index to zero, probably not needed
                newAnimatedGameobjectScript.currentAnimationPoint = newAnimatedGameobjectScript.animationData.animationPoints[0];//set the first postion
                newAnimatedGameobjectScript.forceToCurrentAnimationPoint();//force it to go to the first point 
            }*/
        }
    }

    //animates all the oobjects 
    public void animateObjects()
    {
        foreach (AnimationData animatedObject in sceenData.objectAnimationData)
        {
            animateObject(animatedObject);
        }
    }

    //animates a specific object
    public void animateObject(AnimationData animatedObject)
    {
        if (animatedObject.instance == null)
        {
            Debug.LogWarning("no isntance of this, this is worrying");

        }
        else
        {
            if (!animatedObject.instance.GetComponent<AnimationObject>().isBeingHeld) {
                if (animatedObject.animationPoints.Count > 0)
                {
                    int currentIndex = 0;
                    int nextIndex = animatedObject.animationPoints.Count - 1;
                    
                    //if we before the first point
                    if(animatedObject.animationPoints[currentIndex].time > currentTime)
                    {
                        nextIndex = 0;
                    }
                    //if after the last 
                    else if (animatedObject.animationPoints[nextIndex].time < currentTime)
                    {
                        currentIndex = nextIndex;
                    }
                    else
                    {
                        while (animatedObject.animationPoints[currentIndex].time < currentTime)
                        {

                            currentIndex++;
                        }
                        while (animatedObject.animationPoints[nextIndex].time > currentTime)
                        {
                            nextIndex--;
                        }

                        if (nextIndex < 0)
                        {
                            nextIndex = 0;
                        }
                    }


                    AnimationPoint currentAnimPoint = animatedObject.animationPoints[currentIndex];
                    AnimationPoint nextAnimationPoint = animatedObject.animationPoints[nextIndex];




                    //calculate the transition percent btween the current point and the point we are moving to 

                    float animationTransitionPercent = 1;
                    if(currentIndex != nextIndex)
                    {
                        animationTransitionPercent =(currentTime - currentAnimPoint.time) / (nextAnimationPoint.time - currentAnimPoint.time);
                    }
                    //lerp the transforms 
                    animatedObject.instance.transform.position = Vector3.Lerp(currentAnimPoint.position, nextAnimationPoint.position, animationTransitionPercent);
                    animatedObject.instance.transform.eulerAngles = Vector3.Lerp(currentAnimPoint.rotation, nextAnimationPoint.rotation, animationTransitionPercent);
                    animatedObject.instance.transform.localScale = Vector3.Lerp(currentAnimPoint.scale, nextAnimationPoint.scale, animationTransitionPercent);
                    //Debug.Log("moving object between frame " + currentIndex + " and " + nextIndex + " %" + animationTransitionPercent);
                    animatedObject.instance.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                }

            }
           
        }
        
    }
    public void addAnimationFrame(GameObject animatingObject)
    {
        //loop through untill we find the right one
        foreach (AnimationData animatedObject in sceenData.objectAnimationData)
        {
            if(animatedObject.instance == animatingObject)
            {
                int nextIndex = 0;
                if (animatedObject.animationPoints.Count > 0)
                {
                    int currentIndex = 0;
                    nextIndex = animatedObject.animationPoints.Count - 1;

                    //if we before the first point
                    if (animatedObject.animationPoints[currentIndex].time > currentTime)
                    {
                        nextIndex = 0;
                    }
                    //if after the last 
                    else if (animatedObject.animationPoints[nextIndex].time < currentTime)
                    {
                        currentIndex = nextIndex;
                    }
                    else
                    {
                        while (animatedObject.animationPoints[currentIndex].time < currentTime)
                        {

                            currentIndex++;
                        }
                        while (animatedObject.animationPoints[nextIndex].time > currentTime)
                        {
                            nextIndex--;
                        }

                        if (nextIndex < 0)
                        {
                            nextIndex = 0;
                        }
                    }
                }
                //make and insert
                AnimationPoint animationPointTosave = new AnimationPoint(currentTime, animatingObject.transform, Color.white);
                animatedObject.animationPoints.Add(animationPointTosave);//add dumbly

                animatedObject.animationPoints.Sort((x, y) => x.time.CompareTo(y.time));//sort them by time os its at the right postion 

            }
        }
    }
    public void startAudioRecording()
    {
        Debug.Log("Recording audio");
        //AudioSource audioSource = GetComponent<AudioSource>();
        //audioData.audioClip = AudioClip.Create("audio", 44100 * 10, 1, 44100, false);
        audioData.audioClip = Microphone.Start(null, false, 60, 44100);
        isRecordingAudio = 60;
        
        //audioSource.Play();
        //audioSource.clip.get
    }
    public void playAudioRecording()
    {
        playAudioRecording(0);
    }
    public void playAudioRecording(float playTime)
    {
        Debug.Log("playing Audio");
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.time = playTime;
        audioSource.clip = audioData.audioClip;
        audioSource.Play();
        
    }
    public void stopAudio()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
    }
}
