using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private int hod = 0;

    private Coroutine coroutine;

    [SerializeField]
    private GameObject nextgame;

    [SerializeField]
    private int currentLvl;

    [SerializeField]
    private Node targetOfGame;

    [SerializeField]
    private GameObject star;

    [SerializeField]
    private GameData gameData;

    AudioSource source;

    [SerializeField]
    AudioClip clip;

    public int CurrentLvl
    {
        get { return currentLvl; }
    }

    public int Hod { get { return hod; } set { hod = value; } }

    public Coroutine Coroutine { get { return coroutine; } set { coroutine = value; } }


    private void Start()
    {
        source = GameObject.Find("Main Camera").GetComponent<AudioSource>();
    }

    public IEnumerator MovingCharacter(GameObject player, Vector3 actualPos)
    {
        while (Vector3.Distance(player.transform.position, actualPos) > 0.01f)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, actualPos, 4.5f * Time.deltaTime);

            yield return new WaitForSeconds(0.01f);
        }

        if (targetOfGame == gameObject.GetComponent<CharacterControlelr>().CurrentNode)
        {
            source.PlayOneShot(clip);

            StartCoroutine(MovingStar());
            
        }

        hod++;

        coroutine = null;
    }


        
    

    public void WaitForPlayerMove()
    {
        hod++;
    }

    IEnumerator MovingStar()
    {
        int step = 0;

        star.GetComponent<SpriteRenderer>().sortingOrder = 15;

        while(step < 100)
        {
            star.transform.position += new Vector3(0, 2f, 0) * Time.deltaTime;

            yield return new WaitForSeconds(0.01f);

            step++;
        }

        if (currentLvl == 1 || currentLvl == 2)
        {
            Instantiate(nextgame);

            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);

            gameData.CompleteMiniGame1(2, true);

            GameObject.Find("девочка").GetComponent<GGMove>().isMiniGameRunning = false;

            GameObject.Find("девочка").gameObject.transform.position = new Vector3(-9.13f, -1.9f, 1);

        }
    }
}
