using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTextScript : MonoBehaviour {
    public InputManager inputManager;
    public GameObject player;

    public string[] dialog;
    [SerializeField]
    private DialogManager manager;
    [SerializeField]
    private QuestGiverScript[] quests;

    private int callbackID;

    // Use this for initialization
    void Start()
    {
        manager = FindObjectOfType<DialogManager>();
        quests = gameObject.GetComponents<QuestGiverScript>();
        player = GameObject.FindGameObjectWithTag("Player");
    }


    public void Talk()
    {
        // If the player is facing the NPC within a 90 degree angle
        float dot = Vector3.Dot(player.transform.forward, (transform.position - player.transform.position).normalized);
        if (dot > 0.7f)
        {
            if (!manager.dialogBox.activeSelf)
            {
                foreach (var quest in quests)
                {
                    quest.CheckQuest();
                }

                manager.currentLine = 0;

                manager.ShowDialog(dialog);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            callbackID = inputManager.RegisterAction(InputManager.Keys.interract, Talk);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inputManager.RemoveAction(InputManager.Keys.interract, Talk);
            callbackID = 0;
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        if (Input.GetKeyDown(KeyCode.F))
    //        {
    //            if (!manager.dialogBox.activeSelf)
    //            {
    //                foreach (var quest in quests)
    //                {
    //                    quest.CheckQuest();
    //                }
    //
    //                manager.currentLine = 0;
    //
    //                manager.ShowDialog(dialog);
    //            }
    //        }
    //    }
    //}
}
