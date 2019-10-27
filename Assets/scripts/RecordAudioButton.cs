using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordAudioButton : MonoBehaviour
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
        if (systemManager.isRecordingAudio >0)
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
        if (systemManager.isRecordingAudio < 0.01)
        {
            systemManager.startAudioRecording();
            systemManager.currentTime = 0;
            systemManager.isPlayingAnim = true;
        }
    }
}
