using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInteraction : MonoBehaviour {

    private DialogueSystem dialogueSystem;

    public string Name;

    [TextArea(5, 10)]
    public string[] sentences;

	// Use this for initialization
	void Start ()
    {
        dialogueSystem = FindObjectOfType<DialogueSystem>();
	}
	

    public void OnTriggerEnter(Collider other)
    {
        this.gameObject.GetComponent<ItemInteraction>().enabled = true;
        //Can add interaction button in
        if((other.gameObject.tag == "Player"))
        {
            this.gameObject.GetComponent<ItemInteraction>().enabled = true;
            dialogueSystem.dialogueLines = sentences;
            dialogueSystem.ItemInteraction();
        }
        if(dialogueSystem.dialogueEnded == true)
        {
            Destroy(this);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        dialogueSystem.OutOfRange();
        this.gameObject.GetComponent<ItemInteraction>().enabled = false;
    }
}
