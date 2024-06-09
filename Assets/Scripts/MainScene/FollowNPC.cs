using System.Collections.Generic;
using UnityEngine;

public class FollowNPC : MonoBehaviour
{
    public DialogManager dialogManager;
    public List<Sprite> playerDialogs;
    public List<Sprite> npcDialogs;
    public List<Sprite> completedGameDialog;
    public List<Sprite> completedPlayerDialog;
    public GameData gameData;
    public int numberOfGame;

    [SerializeField]
    private AudioSource playerAudioSource;

    [SerializeField]
    private AudioClip playerAudioClip;

    private void Start()
    {
        //playerAudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            // Проверяем попадание луча в коллайдер объекта
            if (hit.collider != null && hit.transform == transform)
            {
                playerAudioSource.PlayOneShot(playerAudioClip);

                OnObjectClicked();
            }
        }
    }

    private void OnObjectClicked()
    {
        Debug.Log("Object clicked");

        if (gameData.Games[numberOfGame] == false)
        {
            dialogManager.StartDialog(playerDialogs, npcDialogs);
        }
        else
        {
            dialogManager.StartDialog(completedPlayerDialog, completedGameDialog);
        }
        
    }
}
