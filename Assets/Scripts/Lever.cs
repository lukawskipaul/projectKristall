using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour, IActivatable {

    [SerializeField]
    string nameText;

    [SerializeField]
    LeverPuzzle leverPuzzle;

    Animator anim;
    [FMODUnity.EventRef]
    FMOD.Studio.EventInstance leverPullSound;

   
    private bool isLeverPulled = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        leverPullSound = FMODUnity.RuntimeManager.CreateInstance("event:/lever sound (3)");

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
        leverPullSound.start();
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
