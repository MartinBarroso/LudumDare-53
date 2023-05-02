using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject MenuScene;
    [SerializeField] private GameObject CreditsScene;
    private bool isCreditsOn = false;
    void Start()
    {
        isCreditsOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeScene();
    }

    private void ChangeScene()
    {
        CreditsScene.SetActive(isCreditsOn);
    }

    public void ToggleCredits()
    {
        isCreditsOn = !isCreditsOn;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    public void ExitGame()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.button);
        Debug.Log("Quit!");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
   	    Application.Quit();
#endif
    }
  
}
