using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiverScript : MonoBehaviour {
    public BaseQuest quest;
    public BaseQuest questPrerequisite;

    public bool prerequisite;
    public bool prerequisiteMet;

    public string[] newQuest;
    public string[] onQuest;
    public string[] completedQuest;
    
    [SerializeField]
    private DialogManager dialogManager;
    [SerializeField]
    private QuestManager questManager;

    void Start()
    {
        dialogManager = FindObjectOfType<DialogManager>();
        questManager = FindObjectOfType<QuestManager>();
    }

    void FixedUpdate()
    {
        if (prerequisite)
        {
            if (questManager.questCompleted[questPrerequisite.questID] && !questManager.quests[questPrerequisite.questID].gameObject.activeSelf)
                prerequisiteMet = true;
        }
        else
            prerequisiteMet = true;
    }

    public void GiveQuest()
    {
        dialogManager.giveQuest.gameObject.SetActive(false);
        dialogManager.accept.gameObject.SetActive(true);
        dialogManager.decline.gameObject.SetActive(true);
        dialogManager.ShowDialog(newQuest);
    }

    public void AcceptQuest()
    {
        dialogManager.accept.gameObject.SetActive(false);
        dialogManager.decline.gameObject.SetActive(false);
        questManager.quests[quest.questID].gameObject.SetActive(true);
        questManager.quests[quest.questID].StartQuest();
    }

    public void DeclineQuest()
    {
        dialogManager.accept.gameObject.SetActive(false);
        dialogManager.decline.gameObject.SetActive(false);
        dialogManager.CloseBox();
    }

    public void OnQuest()
    {
        dialogManager.giveQuest.gameObject.SetActive(false);
        dialogManager.currentLine = 0;
        dialogManager.ShowDialog(onQuest);
    }

    public void HandInQuest()
    {
        dialogManager.giveQuest.gameObject.SetActive(false);
        dialogManager.currentLine = 0;
        dialogManager.ShowDialog(completedQuest);
        questManager.quests[quest.questID].EndQuest();
        questManager.quests[quest.questID].gameObject.SetActive(false);
    }

    public void CheckQuest()
    {
        if (prerequisiteMet)
        {
            if (!questManager.questCompleted[quest.questID] && !questManager.quests[quest.questID].gameObject.activeSelf)
            {
                dialogManager.giveQuest.gameObject.SetActive(true);
                dialogManager.giveQuest.onClick.RemoveAllListeners();
                dialogManager.giveQuest.onClick.AddListener(GiveQuest);

                dialogManager.accept.onClick.RemoveAllListeners();
                dialogManager.accept.onClick.AddListener(AcceptQuest);
                dialogManager.decline.onClick.RemoveAllListeners();
                dialogManager.decline.onClick.AddListener(DeclineQuest);
            }
            else if (!questManager.questCompleted[quest.questID] && questManager.quests[quest.questID].gameObject.activeSelf)
            {
                dialogManager.giveQuest.gameObject.SetActive(true);
                dialogManager.giveQuest.onClick.RemoveAllListeners();
                dialogManager.giveQuest.onClick.AddListener(OnQuest);
            }
            else if (questManager.questCompleted[quest.questID] && questManager.quests[quest.questID].gameObject.activeSelf)
            {
                dialogManager.giveQuest.gameObject.SetActive(true);
                dialogManager.giveQuest.onClick.RemoveAllListeners();
                dialogManager.giveQuest.onClick.AddListener(HandInQuest);
            }
        }
    }

}
