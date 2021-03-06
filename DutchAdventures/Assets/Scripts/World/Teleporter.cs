using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Teleporter : MonoBehaviour
{
    public Transform player;
    public string sceneName;
    protected float minDist = 2;
    [SerializeField]
    private GameObject pointer;
    private bool canTravel = false;
    public PlayerSpawn entracePos;
    public PlayerSpawn exitPos;
    public Vector2 destination;
    private BoxCollider2D boxCollider2D;
    public GameObject fadeObject;
    public GameObject loadingText;

    void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    /// <summary>
    /// Place the player at the place where the player entered the building
    /// </summary>
    private void Start()
    {
        player.position = this.GetPreviousPosition(entracePos, exitPos, player);
        Vector3 pos = exitPos.spawnPosition;
        pos.z = -10;
        Camera.main.transform.position = pos;
    }

    private void Update()
    {
        //get the distance between the player and pointer
        float dist = Vector2.Distance(player.position, transform.position);
        canTravel = dist < minDist;
        pointer.SetActive(canTravel);
    }

    /// <summary>
    /// use the teleport function onclick
    /// </summary>
    private void OnMouseDown()
    {
        StartCoroutine(teleport());
    }

    /// <summary>
    /// use the teleport function on entering a collision
    /// </summary>
    /// <param name="collision">Collision trigger</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(teleport());
    }

    /// <summary>
    /// teleport the player to the given position and load the scene
    /// </summary>
    /// <returns>Load player into new scene</returns>
    private IEnumerator teleport()
    {
        if (canTravel)
        {
            entracePos.spawnPosition = player.position;
            exitPos.spawnPosition = destination;
            if(fadeObject != null)
            { 
                Animator anim = fadeObject.GetComponent<Animator>();
                anim.SetTrigger("Start");
                yield return new WaitForSeconds(1f);
                
            }
            if (loadingText != null)
            {
                loadingText.SetActive(true);
            }
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
    }

    /// <summary>
    /// spawn the player and check whether a position has been given before
    /// </summary>
    /// <param name="entrance">Entrance object</param>
    /// <param name="exit">Exit object</param>
    /// <param name="player">Player object</param>
    /// <returns>Returns previous location of player</returns>
    private Vector2 GetPreviousPosition(PlayerSpawn entrance, PlayerSpawn exit, Transform player)
    {
        if (HasPreviousPosition(entrance) && HasPreviousPosition(exit))
        {
            return exit.spawnPosition;
        }
        else 
        {
            return player.position;
        }
    }

    /// <summary>
    /// check if there is a previous position
    /// </summary>
    /// <param name="pos">Player position</param>
    /// <returns>True or false</returns>
    private bool HasPreviousPosition(PlayerSpawn pos)
    {
        return !pos.spawnPosition.Equals(Vector2.zero);
    }

}

