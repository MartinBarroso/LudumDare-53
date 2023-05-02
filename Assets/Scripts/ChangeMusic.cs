using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.GetMusicSource().clip = SoundManager.Instance.mainMenu;
        SoundManager.Instance.GetMusicSource().Play();
        Screen.SetResolution(1280,720,false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
