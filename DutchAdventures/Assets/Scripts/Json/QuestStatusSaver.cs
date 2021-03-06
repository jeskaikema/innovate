using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestStatusSaver : MonoBehaviour
{
    private JsonHandler jsonHandler;

    private NpcQuestStatuses npcStatuses = new NpcQuestStatuses();

    private void Awake()
    {
        jsonHandler = GameObject.FindGameObjectWithTag("JsonHandler").GetComponent<JsonHandler>();
        npcStatuses = jsonHandler.ReadFromJson<NpcQuestStatuses>("npcQuestData");
    }

    /// <summary>
    /// write the npc quest data to the json
    /// </summary>
    /// <param name="npcName">Npc name</param>
    /// <param name="hasAccepted">If accept quest</param>
    /// <param name="hasCompletedQuest">If quest is completed</param>
    public void writeNpcStatusToJson(string npcName, bool hasAccepted, bool hasCompletedQuest)
    {
        //looping through every npc's status
        for (int i = 0; i < npcStatuses.statuses.Length; i++)
        {
            if (npcStatuses.statuses[i].npcName == npcName)
            {
                npcStatuses.statuses[i] = new NpcQuestStatus(npcName, hasAccepted, hasCompletedQuest);
            }
        }

        jsonHandler.WriteToJson(npcStatuses, "NpcQuestData");
    }

    /// <summary>
    /// get the npc's status from the json file
    /// </summary>
    /// <param name="npcName">Npc name</param>
    /// <returns>Returns status of NPC in map</returns>
    public bool[] getNpcStatus(string npcName)
    {
        //NPCController npcController = GameObject.Find(npcName).GetComponent<NPCController>();
        npcStatuses = jsonHandler.ReadFromJson<NpcQuestStatuses>("npcQuestData");

        for (int i = 0; i < npcStatuses.statuses.Length; i++)
        {
            if (npcStatuses.statuses[i].npcName == npcName)
            {
                return new bool[2] { npcStatuses.statuses[i].hasTakenQuest, npcStatuses.statuses[i].hasCompletedQuest };
            }
        }
        return new bool[2] { false, false };
    }
}

[System.Serializable]
public class NpcQuestStatuses
{
    public NpcQuestStatus[] statuses;
}

[System.Serializable]
public class NpcQuestStatus
{
    public string npcName;
    public bool hasTakenQuest;
    public bool hasCompletedQuest;

    public NpcQuestStatus(string npcName, bool hasTakenQuest, bool hasCompletedQuest)
    {
        this.npcName = npcName;
        this.hasTakenQuest = hasTakenQuest;
        this.hasCompletedQuest = hasCompletedQuest;
    }
}
