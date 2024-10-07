using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NPCInteract : MonoBehaviour
{
    private GameObject box;
    private GameObject cont;
    private GameObject gossip;
    private GameObject trade;
    private GameObject exit;
    bool interactable;
    public Dialogue dialogue;

    void Start()
    {
        box = GameObject.Find("Dialog");
        cont = GameObject.Find("Next");
        box.SetActive(false);
        cont.SetActive(false);
        gossip = GameObject.Find("Gossip");
        trade = GameObject.Find("Trade");
        exit = GameObject.Find("Exit");
        interactable = false;
        
    }

    void Update()
    {
        if(interactable && Input.GetKeyDown(KeyCode.E))
        {
            TriggerDialogue();
            FindAnyObjectByType<PlayerController>().canMove = false;
            FindAnyObjectByType<Flashlight>().isOn = false;
            FindAnyObjectByType<PlayerShooting>().canFire = false;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        interactable = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        interactable = false;
    }

    public void TriggerDialogue()
    {
        if (dialogue == null)
        {
            Debug.LogWarning("No dialogue assigned to NPC!");
            return;
        }
        gossip.SetActive(true);
        trade.SetActive(true);
        exit.SetActive(true);
        box.SetActive(false);
        cont.SetActive(false);
        FindObjectOfType<DialogueManager>().SetCurrentDialogue(dialogue);
    }
}
