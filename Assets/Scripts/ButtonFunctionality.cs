using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctionality : MonoBehaviour {

    [SerializeField]
    private GameObject mainMenu, creditsMenu;

    [SerializeField]
    private string startLevel;

    public AudioSource audio;
    public AudioClip hoverSound;
    public AudioClip clickSound;


    public void OnStartClick()
    {
        SceneManager.LoadScene(startLevel);
    }

    public void OnCreditsClick()
    {
        mainMenu.SetActive(false);
        creditsMenu.SetActive(true);

        CameraMovement.creditsClicked = true;
    }

    public void OnBackClick()
    {
        mainMenu.SetActive(true);
        creditsMenu.SetActive(false);
        CameraMovement.creditsClicked = false;

    }


    public void OnQuitClick()
    {
        Application.Quit();
    }

    //Sound Functions
    public void HoverSound()
    {
        audio.PlayOneShot(hoverSound);
    }

    public void ClickSound()
    {
        audio.PlayOneShot(clickSound);
    }
	
}
