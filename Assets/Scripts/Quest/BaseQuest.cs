using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseQuest : MonoBehaviour {
    public GameObject HandInLocation;

    public enum QuestState
    {
        Started,
        Completed,
        Finished
    };

    public int questID;

    public QuestManager manager;

    public string startText;
    public string completeText;

    public QuestState state;

    virtual public void Awake()
    {
        HandInLocation.GetComponent<QuestLocationTrigger>().UpdateText("?");
        HandInLocation.SetActive(false);
    }

    virtual public void StartQuest()
    {
        manager.ShowQuestText(startText);
        manager.questLog.Add(this);
        state = QuestState.Started;
    }

    virtual public void HandInQuest()
    {
        state = QuestState.Completed;
        manager.ShowQuestText(completeText);
        manager.questCompleted[questID] = true;
        HandInLocation.SetActive(true);
    }

    virtual public void EndQuest()
    {
        state = QuestState.Finished;
        manager.questLog.Remove(this);
        HandInLocation.SetActive(false);
    }
}
