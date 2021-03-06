using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DialogHandler : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI dialogBox;
    [SerializeField]
    private TextMeshProUGUI npcNameTxt;
    public CanvasGroup btns;

    public NPCController npc;

    [HideInInspector]
    public string dialog;
    private string dialogToDisplay;
    [HideInInspector]
    public bool isFinished = false;

    private Coroutine printDialog;

    private void Start()
    {
        //Start the coroutine that prints every character at a time;
        printDialog = StartCoroutine(printText());
        npcNameTxt.text = npc.displayName;

        //Make the buttons invisible at start
        btns.alpha = 0;
        btns.interactable = false;
    }

    private void Update()
    {
        //if the NPC is done talking, make the buttons appear.
        btns.alpha = isFinished ? 1 : 0;
        btns.interactable = isFinished;

        bool isOverUI = EventSystem.current.IsPointerOverGameObject();
        if (!isFinished)
        {
            if ((Input.GetMouseButtonDown(0) && isOverUI))
            {
                StopCoroutine(printDialog);
                dialogBox.text = dialog;
                isFinished = true;
            }
            else
            {
                dialogBox.text = dialogToDisplay;
            }
        }
        if(Input.touchCount > 0)
        {
            int id = Input.GetTouch(0).fingerId;
            if (EventSystem.current.IsPointerOverGameObject(id))
            {
                if (!isFinished)
                {
                    StopCoroutine(printDialog);
                    dialogBox.text = dialog;
                    isFinished = true;
                }
            }
        }
    }
    /// <summary>
    /// Destroy dialog box if button is pressed
    /// </summary>
    public void closeBtn()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// complete quest
    /// </summary>
    public void completeQuest()
    {
        Debug.Log("Complete quest");
        GameObject.Find("QuestMenu").GetComponent<QuestUI>().completeQuest();
        closeBtn();
    }

    /// <summary>
    /// Prints text with interval
    /// </summary>
    /// <returns>Prints text with interval</returns>
    IEnumerator printText()
    {
        //Print each letter one at a time.
        isFinished = false;
        yield return new WaitForSeconds(.05f);
        foreach (char c in dialog.ToCharArray())
        {
            dialogToDisplay += c;
            yield return new WaitForSeconds(.05f);
        }
        isFinished = true;
    }
}
