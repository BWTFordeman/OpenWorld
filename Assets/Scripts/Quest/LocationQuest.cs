using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationQuest : BaseQuest {
    public QuestLocationTrigger[] locations;
    public bool[] completedLocations;

    public bool triggered;

	// Use this for initialization
	public override void Awake () {
        base.Awake();
        triggered = false;
        completedLocations = new bool[locations.Length];

        foreach (var location in locations)
        {
            location.gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	public void FixedUpdate () {
        
		for (int i = 0; i < locations.Length; i++)
        {
            if (locations[i].trigger)
            {
                completedLocations[i] = true;
                locations[i].gameObject.SetActive(false);
            }
            locations[i].UpdateText((manager.GetQuestLogEntryNo(this) + 1).ToString());
        }

        bool temp = true;
        foreach (var completed in completedLocations)
        {
            if (!completed)
                temp = false;
        }

        if (temp && !triggered)
        {
            triggered = true;
            HandInQuest();
        }
	}
    public override void StartQuest()
    {
        foreach (var location in locations)
        {
            location.gameObject.SetActive(true);
        }
        base.StartQuest();
    }
}
