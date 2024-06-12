using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public SpriteRenderer playerDialogRenderer;
    public SpriteRenderer npcDialogRenderer;

    private Queue<Sprite> playerDialogQueue;
    private Queue<Sprite> npcDialogQueue;

    [SerializeField]
    private int NumberGame;

    public GGMove ggmove;

    public Test test;

    [SerializeField]
    private GameData gameData;

    private bool isDialog = false;

    [SerializeField]
    private GameObject gamePrefab;

    public float dialogDisplayTime = 2f;

    public bool IsDialog
    {
        get { return isDialog; }
    }

    private void Start()
    {
        playerDialogQueue = new Queue<Sprite>();
        npcDialogQueue = new Queue<Sprite>();
    }

    public void StartDialog(List<Sprite> playerDialogs, List<Sprite> npcDialogs)
    {
        if (ggmove.gameObject.transform.rotation.y == 1 || ggmove.gameObject.transform.rotation.y == -1)
        {
            playerDialogRenderer.flipX = true;
        }
        else
        {
            playerDialogRenderer.flipX = false;
        }

        isDialog = true;

        ggmove.gameObject.transform.position = gameObject.transform.position + gameObject.GetComponent<ActivateDialog>().DialogPos; 

        playerDialogQueue.Clear();
        npcDialogQueue.Clear();

        foreach (Sprite dialog in playerDialogs)
        {
            playerDialogQueue.Enqueue(dialog);
        }

        foreach (Sprite dialog in npcDialogs)
        {
            npcDialogQueue.Enqueue(dialog);
        }

        StartCoroutine(DisplayNextDialog());
    }

    private IEnumerator DisplayNextDialog()
    {
        while (playerDialogQueue.Count > 0 || npcDialogQueue.Count > 0)
        {
            if (playerDialogQueue.Count > 0)
            {
                playerDialogRenderer.sprite = playerDialogQueue.Dequeue();
                playerDialogRenderer.enabled = true;
                yield return new WaitForSeconds(dialogDisplayTime);
                playerDialogRenderer.enabled = false;
            }

            if (npcDialogQueue.Count > 0)
            {
                npcDialogRenderer.sprite = npcDialogQueue.Dequeue();
                npcDialogRenderer.enabled = true;
                yield return new WaitForSeconds(dialogDisplayTime);
                npcDialogRenderer.enabled = false;
            }
        
        }

        ggmove.gameObject.transform.position = new Vector3(-9.13f, -1.9f, 1);
        
        isDialog = false;

        if (gamePrefab != null && gameData.Games[NumberGame] == false)
        {
            ggmove.isMiniGameRunning = true;

            test.game = Instantiate(gamePrefab);
        }

        if (gameObject.name == "грустный")
        {
            gameData.IsAcked = true;
        }
        
        yield return null;
    }
}
