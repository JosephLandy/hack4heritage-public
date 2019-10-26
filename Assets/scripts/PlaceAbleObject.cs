using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


/*
 * Script to control objects that can get placed and motion recoreded
 * 
 * 
 * 
 */
public class PlaceAbleObject : MonoBehaviour
{

    /*public SteamVR_Action_Boolean grabPinch; //Grab Pinch is the trigger, select from inspecter
    public SteamVR_Input_Sources inputSource = SteamVR_Input_Sources.Any;//which controller
                                                                         // Use this for initialization

    void OnEnable()
    {
        if (grabPinch != null)
        {
            grabPinch.AddOnStateDownListener(menuButtonDown, inputSource);
        }
    }


    private void OnDisable()
    {
        if (grabPinch != null)
        {
            grabPinch.RemoveOnStateDownListener(menuButtonDown, inputSource);
        }
    }*/



    public SteamVR_Input_Sources handType; // 1
    public SteamVR_Action_Boolean MenuButton; // 2



    [SerializeField]
    public AnimationData animationData;
    [SerializeField]
    public float currentTime;
    [SerializeField]
    bool isRecordingMode = false;
    [SerializeField]
    Color currentColor;
    [SerializeField]
    int currentAnimationIndex =0;
    [SerializeField]
    AnimationPoint currentAnimationPoint;
    [SerializeField]
    float animationTransitionPercent;
    [SerializeField]
    public bool isBeingHeld = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    //another hack so we knwo when we are being picked up
    public void pickedup()
    {
        isBeingHeld = true;
    }
    public void droped()
    {
        isBeingHeld = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetMenuDown())
        {
            Debug.Log("menu button pressed");
        }

        currentTime += Time.deltaTime;

        if (isRecordingMode)
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                recordTransform();
            }
        }
        else
        {
            //super messy way to reintilize animation index
            if(currentTime < 0.1)
            {
                currentAnimationIndex = 0;
                currentAnimationPoint = animationData.animationPoints[0];
            }
            //if there are still aniamtion points 
            if(currentAnimationIndex < animationData.animationPoints.Count-1)
            {
                //this is the next point
                AnimationPoint nextAnimationPoint = animationData.animationPoints[currentAnimationIndex +1];
                //calculate the transition percent btween the current point and the point we are moving to 
                animationTransitionPercent =   (currentTime - currentAnimationPoint.time)/(nextAnimationPoint.time - currentAnimationPoint.time) ;
                //lerp the transforms 
                this.transform.position = Vector3.Lerp(currentAnimationPoint.position, nextAnimationPoint.position, animationTransitionPercent);
                this.transform.eulerAngles = Vector3.Lerp(currentAnimationPoint.rotation, nextAnimationPoint.rotation, animationTransitionPercent);
                this.transform.localScale = Vector3.Lerp(currentAnimationPoint.scale, nextAnimationPoint.scale, animationTransitionPercent);
                //go to the next one if this animation is done
                if (animationTransitionPercent > 0.99)
                {
                    currentAnimationPoint = nextAnimationPoint;//move the current animtion point forward
                    currentAnimationIndex++;
                }

                
            }
            
        }



    }
    public bool GetMenuDown() // 1
    {
        return MenuButton.GetStateDown(handType);
    }

    /*private void menuButtonDown(SteamVR_Action_In action_In)
    {
        recordTransform();
    }*/

    //saves the current transform to animation data 
    void recordTransform()
    {
        AnimationPoint animationPointTosave = new AnimationPoint(currentTime,this.transform,currentColor);
        animationData.animationPoints.Add(animationPointTosave);
    }

}
