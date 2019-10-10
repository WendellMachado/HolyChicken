using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class mSceneManagement : MonoBehaviour
{
    [SerializeField]
    GameObject menu, game, menuToGameTransition, clouds, sun, bgGame, player, bgGover, eggOver, eggOverPrefab;

    [SerializeField]
    GameObject[] enemyPrefabs;

    [SerializeField]
    Text scoreTxt, highscore, scoretxtGame, feedback;

    Animator anim;

    Color color, color2;

    bool gamePlaying, firstRestart;

    float time, timeToNextLevel, score;

    int level, enemiesOnScreen;

    void Configure()
    {
        PlayGamesClientConfiguration config = new
            PlayGamesClientConfiguration.Builder()
            .Build();

        // Initialize and activate the platform
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
    }

    public void Login()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            //feedback.text = "login " + success;
        });
    }

    void showLeaderBoard()
    {
        PlayGamesPlatform.Instance.ShowLeaderboardUI();
    }

    void ReportScore()
    {
        Social.ReportScore(Mathf.RoundToInt(score), "CgkI-9SigvYSEAIQAA", (bool success) =>
        {
            //feedback.text = "report " + success;
        });
    }

    void Awake()
    {
        Configure();
    }


    void Start()
    {
        Login();
        Invoke("nullFeedback", 5);

        firstRestart = true;
        anim = this.gameObject.GetComponent<Animator>();
        color = color2 = Color.white;
        level = 4;
        timeToNextLevel = 1;
        score = 0;
        //scoreTxt.text = string.Empty + score;
        scoretxtGame.text = string.Empty + score;
    }

    void CallMid()
    {
        anim.SetTrigger("CallMid");
    }

    void CallGame()
    {
        Destroy(menu);
        anim.SetTrigger("CallGame");
    }

    IEnumerator gameOn()
    {
        yield return new WaitForSeconds(0.05f);

        color.a += 0.05f;

        bgGame.GetComponent<SpriteRenderer>().color = color;

        sun.GetComponent<SpriteRenderer>().color = color;

        for (int i = 0; i < clouds.transform.childCount; i++)
        {
            clouds.transform.GetChild(i).GetComponent<SpriteRenderer>().color = color;
        }

        if (color.a > 0)
        {
            StartCoroutine(gameOn());
        }

    }

    void StartGame()
    {
        Destroy(menuToGameTransition);

        game.SetActive(true);

        color.a = 0;

        bgGame.GetComponent<SpriteRenderer>().color = color;

        sun.GetComponent<SpriteRenderer>().color = color;

        for (int i = 0; i < clouds.transform.childCount; i++)
        {
            clouds.transform.GetChild(i).GetComponent<SpriteRenderer>().color = color;
        }
        StartCoroutine(gameOn());

        anim.SetTrigger("GameOn");
    }

    void EggOpen()
    {
        Destroy(game.transform.GetChild(3).gameObject);
        anim.SetTrigger("EggOpen");
    }

    void SpawnChicken()
    {
        Destroy(game.transform.GetChild(3).gameObject);
        GameObject nplayer;

        nplayer = (GameObject)Instantiate(player, game.transform.GetChild(3).transform.position, player.transform.rotation);
        nplayer.GetComponent<SpriteRenderer>().sortingOrder = 11;
        nplayer.GetComponent<Player>().gManager = this;

        gamePlaying = true;
        scoreTxt.gameObject.SetActive(true);
        StartCoroutine("TimePass");
    }

    public void SpawnEnemies()
    {
        Enemy nEnemy;

        for (int i = 0; i < level; i++)
        {
            int j = Mathf.RoundToInt(Random.Range(0, enemyPrefabs.Length));

            nEnemy = (Enemy)Instantiate(enemyPrefabs[j], enemyPrefabs[j].transform.position, enemyPrefabs[j].transform.rotation).gameObject.GetComponent<Enemy>();
            nEnemy.gManager = this;

            enemiesOnScreen++;
        }
    }

    IEnumerator TimePass()
    {
        yield return new WaitForSeconds(1);

        score++;
        //scoreTxt.text = string.Empty + score;
        scoretxtGame.text = string.Empty + score;

        if (enemiesOnScreen <= 0)
        {
            time++;

            if (time >= timeToNextLevel)
            {
                level++;
                time = 0;
                SpawnEnemies();
            }
        }

        StartCoroutine("TimePass");
    }

    public int gEnemiesOnScreen
    {
        get { return enemiesOnScreen; }
        set { enemiesOnScreen = value; }
    }

    public void CallGameOver()
    {
        StopAllCoroutines();
        anim.SetTrigger("CallGameOver");
        scoreTxt.text = string.Empty + score;

        if (score > PlayerPrefs.GetInt("Highscore"))
        {
            PlayerPrefs.SetInt("Highscore", Mathf.RoundToInt(score));
        }

        highscore.text = "Highscore: " + PlayerPrefs.GetInt("Highscore");

        //if (!firstRestart)
        //{
            eggOver.GetComponent<Animator>().SetTrigger("Appear");
        //}

            ReportScore();
    }


    void CallRestartGame()
    {
        color2.a = 1;

        scoreTxt.gameObject.SetActive(true);
        scoreTxt.color = color2;

        highscore.gameObject.SetActive(true);
        highscore.color = color2;

        bgGover.transform.position = Vector2.zero;

        for (int i = 0; i < eggOver.transform.childCount; i++)
        {
            eggOver.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().color = color2;
        }

        eggOver.GetComponent<Animator>().SetTrigger("Break");
    }

    public void EggOverRespawn()
    {
        Egg ne;
        ne = (Egg) Instantiate(eggOverPrefab, this.transform).gameObject.GetComponent<Egg>();
        ne.Manager = this;
        ne.transform.SetParent(this.transform.GetChild(0));
        ne.transform.Translate(0, 0, 10);
        ne.gameObject.transform.GetChild(0).gameObject.GetComponent<mButton>().setReceiver = this.gameObject;
        eggOver = ne.gameObject;
        anim.SetTrigger("GoverOut");
    }

    void RestartGame()
    {
        firstRestart = false;
        GameObject nplayer;
        level = 4;
        score = 0;
        scoreTxt.text = string.Empty;
        scoretxtGame.text = string.Empty + score;

        nplayer = (GameObject)Instantiate(player, eggOverPrefab.transform.position, player.transform.rotation);
        nplayer.GetComponent<SpriteRenderer>().sortingOrder = 10;
        nplayer.GetComponent<Player>().gManager = this;

        gamePlaying = true;
        scoreTxt.gameObject.SetActive(true);
        StartCoroutine("TimePass");
    }

    void nullFeedback()
    {
        feedback.text = string.Empty;
    }
}
