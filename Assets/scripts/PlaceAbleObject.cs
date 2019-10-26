using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/*
 * Script to control objects that can get placed and motion recoreded
 * 
 * 
 * 
 */
public class PlaceAbleObject : MonoBehaviour
{
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = Time.deltaTime;

        if (isRecordingMode)
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                recordTransform();
            }
        }
        else
        {
            //if there are still aniamtion points 
            if(currentAnimationIndex < animationData.animationPoints.Count)
            {
                //this is the next point
                AnimationPoint nextAnimationPoint = animationData.animationPoints[currentAnimationIndex +1];
                animationTransitionPercent =   (currentTime - currentAnimationPoint.time)/(nextAnimationPoint.time - currentAnimationPoint.time) ;
                //this.transform.position = 

                
            }
            
        }



    }

    //saves the current transform to animation data 
    void recordTransform()
    {
        AnimationPoint animationPointTosave = new AnimationPoint(currentTime,this.transform,currentColor);
        animationData.animationPoints.Add(animationPointTosave);
    }

}
