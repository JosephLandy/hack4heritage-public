using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playpausebutton : MonoBehaviour
{
    SystemManager systemManager;
    void Start()
    {
        systemManager = GameObject.Find("SystemManager").GetComponent<SystemManager>();
    }

    // Update is called once per frame
    public bool pressed = false;
    public GameObject playIcon;
    public GameObject pauseIcon;

    void Update()
    {
        if (systemManager.isPlayingAnim)
        {
            playIcon.SetActive(true);
            pauseIcon.SetActive(false);
        }
        else
        {
            playIcon.SetActive(false);
            pauseIcon.SetActive(true);
        }
    }

    public void pressedToggle()
    {
        pressed = !pressed;
        systemManager.isPlayingAnim = !systemManager.isPlayingAnim;
        if (systemManager.isPlayingAnim)
        {
            systemManager.playAudioRecording(systemManager.currentTime);
        }
        else
        {
            systemManager.stopAudio();
        }
    }

}
