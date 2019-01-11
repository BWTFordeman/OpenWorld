using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestManager : MonoBehaviour {
    public static QuestManager manager = null;

    public BaseQuest[] quests;
    public List<BaseQuest> questLog;
    public bool[] questCompleted;

    public DialogManager dialogManager;

    private void Awake()
    {
        if (manager == null)
            manager = this;
        else if (manager != this)
            Destroy(gameObject);

        DontDestroyOnLoad(transform.parent);
        questLog = new List<BaseQuest>();
    }

    private void Start()
    {
        questCompleted = new bool[quests.Length];

        int i = 0;

        foreach (var quest in quests)
        {
            quest.questID = i++;
            quest.manager = this;
            quest.gameObject.SetActive(false);
        }
    }

    public void ShowQuestText(string questText)
    {
        string[] temp = new string[1];
        temp[0] = questText;
        dialogManager.currentLine = 0;
        dialogManager.ShowDialog(temp);
    }

    public int GetQuestLogEntryNo(BaseQuest quest)
    {
        return questLog.IndexOf(quest);
    }
}
