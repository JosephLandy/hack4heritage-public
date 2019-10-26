using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
///ALL the data in a scene that needs to be saved/loaded 
public class SceenData
{
    
    //lenght of this sceen / story
    [SerializeField]
    public float sceenLenght;
    [SerializeField]
    public List<AnimationData> objectAnimationData;
    public SceenData()
    {
        sceenLenght = 60;
        objectAnimationData = new List<AnimationData>();
    }
}
