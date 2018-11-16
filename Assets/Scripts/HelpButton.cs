using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HelpButton : MonoBehaviour {

    public GameObject helpPanel;


    public void QuestionButton()
    {
        helpPanel.SetActive(true);
    }

    public void BackButton()
    {
        helpPanel.SetActive(false);
    }

    public void ReturnToMenuButton()
    {
        SceneManager.LoadScene("Start_Menu");
    }
}
