using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PompMotionControl : MonoBehaviour
{
    Animator animator;
    public Transform player;
    [SerializeField]
    private GameObject world;
    [SerializeField]
    private GameObject mingame;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //save the item once the action has been done 6 times
        if (animator.GetInteger("amountOfPomps") == 6)
        {
            KeyItemsSaver keyItemSaver = player.GetComponent<KeyItemsSaver>();
            keyItemSaver.setItem("WaterFulled", true);
            keyItemSaver.setItem("Jerrycan", false);

            world.SetActive(true);
            mingame.SetActive(false);
            
            Debug.Log("Minigame compleeted");
        }
        else 
        {
            world.SetActive(false);

        }

    }
    /// <summary>
    /// if pressed on handle the animation triggers
    /// </summary>
    private void OnMouseDown()
    {
            animator.SetTrigger("isTouched");
            animator.SetInteger("amountOfPomps", animator.GetInteger("amountOfPomps") + 1);
    }

}
