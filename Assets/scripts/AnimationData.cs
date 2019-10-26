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
    public AnimationData(GameObject ninstance)
    {
        this.instance = ninstance;
    }
}
