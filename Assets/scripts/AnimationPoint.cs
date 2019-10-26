using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class AnimationPoint
{
    /// <summary>
    /// the time stamp of this postion
    /// </summary>
    public float time;
    /// <summary>
    /// vector 3 postion
    /// </summary>
    public SerializableVector3 position;
    /// <summary>
    /// vector 3 rotation
    /// </summary>
    public SerializableVector3 rotation;
    /// <summary>
    /// vecotr 3 scale
    /// </summary>
    public SerializableVector3 scale;

    /// <summary>
    /// color as a rgb vector 3 
    /// </summary>
    public SerializableVector3 color;

    public AnimationPoint()
    {

    }
    public AnimationPoint(float playtime, Transform objectTransform, Color particleColor) 
    { 
        time = playtime;
        position = objectTransform.position;
        rotation = objectTransform.rotation.eulerAngles;
        scale = objectTransform.localScale;
        color = new SerializableVector3(particleColor.r, particleColor.g, particleColor.r);

    }
    

    

}
