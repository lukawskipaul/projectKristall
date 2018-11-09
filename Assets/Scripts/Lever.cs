using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour, IActivatable {

    [SerializeField]
    string nameText;

    [SerializeField]
    LeverPuzzle leverPuzzle;

    Animator anim;
    private AudioSource leverPullSound; //Declaring the name lever pull sound


    private bool isLeverPulled = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        leverPullSound = GetComponent<AudioSource>(); //Is the leberpullsound audio source

    }

    private void Update()
    {
        if (!leverPuzzle.correctLever && isLeverPulled == true)
        {
            StartCoroutine(AnimationWait());
            
        }
    }

    public string NameText
    {
        get
        {
            return nameText;
        }
    }

    public void DoActivate()
    {
        // whatever we want to happen
        leverPuzzle.CheckLever(this.gameObject);
        leverPullSound.Play();
        if (isLeverPulled)
        {
            anim.SetBool("IsUp", true);
            isLeverPulled = false;
        }
        if (!isLeverPulled)
        {
            anim.SetBool("IsUp", false);
            isLeverPulled = true;
        }

        Debug.Log("Lever Activated");  // just for testing
    }

    IEnumerator AnimationWait()
    {
        yield return new WaitForSeconds(.5f);
        anim.SetBool("IsUp", true);
        isLeverPulled = false;
    }
}
