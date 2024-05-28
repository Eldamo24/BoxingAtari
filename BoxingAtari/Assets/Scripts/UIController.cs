using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    private PlayerController playerController;
    private EnemyController enemyController;
    private TMP_Text scorePlayer;
    private TMP_Text scoreEnemy;
    private TMP_Text timer;
    private int minutes = 1;
    private int seconds = 30;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject winnerPanel;
    [SerializeField] private TMP_Text winText;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        scorePlayer = GameObject.Find("ScorePlayer").GetComponent<TMP_Text>();
        scoreEnemy = GameObject.Find("ScoreEnemy").GetComponent<TMP_Text>();
        timer = GameObject.Find("Timer").GetComponent<TMP_Text>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        enemyController = GameObject.Find("Enemy").GetComponent<EnemyController>();
        scoreEnemy.text = "0";
        scorePlayer.text = "0";
        timer.text = minutes.ToString() + ":" + seconds.ToString();
        StartCoroutine("Timer");
    }

    public void UpdateScorePlayer()
    {
        scorePlayer.text = playerController.PlayerPoints.ToString();
    }

    public void UpdateScoreEnemy()
    {
        scoreEnemy.text = enemyController.EnemyPoints.ToString();
    }

    private void Update()
    {
        if(minutes == 0 && seconds == 0)
        {
            StopCoroutine("Timer");
            playerController.enabled = false;
            enemyController.enabled = false;
            gamePanel.SetActive(false);
            winnerPanel.SetActive(true);
            Winner();
            StartCoroutine("Restart");
        }
    }

    IEnumerator Timer()
    {
        string message;
        while (true)
        {
            seconds--;
            if (seconds < 0)
            {
                minutes--;
                seconds = 59;
            }
            if (seconds < 10)
                message = minutes.ToString() + ":0" + seconds.ToString();
            else
                message = minutes.ToString() + ":" + seconds.ToString();
            timer.text = message;
            yield return new WaitForSeconds(1);
        }
    }

    private void Winner()
    {
        if(int.Parse(scorePlayer.text) > int.Parse(scoreEnemy.text))
        {
            winText.text = "Player wins";
        }
        else if(int.Parse(scorePlayer.text) < int.Parse(scoreEnemy.text))
        {
            winText.text = "Enemy wins";
        }
        else
        {
            winText.text = "Draw";
        }
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Game");
    }


}
