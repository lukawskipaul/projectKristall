using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCrate : MonoBehaviour
{
    [SerializeField]
    string nameText;

    [SerializeField]
    GameObject crate, rock, creature;

    [SerializeField]
    Transform creatureMoveLocation;


    [SerializeField]
    // public CrateBreaking crateBreaking;

    public string[] sentences;

    private DialogueSystem dialogueSystem;
    // Use this for initialization

    private AudioSource cratebreakSound;

    private bool isCrateBroken = false;


    public string NameText
    {
        get
        {
            return nameText;
        }
    }

    void Start()
    {
        dialogueSystem = FindObjectOfType<DialogueSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == rock)
        {
            Destroy(crate);
            creature.transform.position = creatureMoveLocation.position;
            creature.transform.rotation = creatureMoveLocation.rotation;
            PowerupManager.Instance.UnlockPowerup(PowerupManager.Instance.levitateMoveObject);
            PowerupManager.Instance.UnlockPowerup(PowerupManager.Instance.levitateObject);
            dialogueSystem.dialogueLines = sentences;
            dialogueSystem.ItemInteraction();
        }

        // }
        //public void DoActivate()
        // {
        // whatever we want to happen
        //   crateBreaking.crate(this.gameObject);
        //   cratebreakSound.Play(); // PLay Sound here
        //   if (isCrateBroken)
        //   {
        //      anim.SetBool("IsBroken", true);
        //      iscrateBreaking = false;
        // }
        //  if (!iscrateBreaking)
        //      {
        //     anim.SetBool("IsBroken", false);
        //     iscrateBreaking = true;
        //   }
        // }
        // }

        // public void DoActivate()
        // {
        //     throw new System.NotImplementedException();
        // }
    }
}
