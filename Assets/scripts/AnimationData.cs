using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class AnimationData
{
    public int assetIDNum;
    public List<AnimationPoint> animationPoints;
    [System.NonSerialized]
    public GameObject instance;
    [System.NonSerialized]
    public int currentAnimIndx;
    
    public AnimationData(GameObject ninstance)
    {
        this.instance = ninstance;
        currentAnimIndx = 0;
        animationPoints = new List<AnimationPoint>();
    }
}
