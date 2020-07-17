using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public List<Text> scores;
    public List<Text> names;
    public Animator fade;

    void Start()
    {
        SetScoresText();
        FindObjectOfType<AudioManager>().Play("GameOver");
    }

    void SetScoresText()
    {
        Debug.Log("Request scores");
        int highScoreOne = PlayerPrefs.GetInt("highscoreone", 0);
        int highScoreTwo = PlayerPrefs.GetInt("highscoretwo", 0);
        int highScoreThree = PlayerPrefs.GetInt("highscorethree", 0);

        string highScoreOneName = PlayerPrefs.GetString("highscorenameone", "cat");
        string highScoreTwoName = PlayerPrefs.GetString("highscorenametwo", "cat");
        string highScoreThreeName = PlayerPrefs.GetString("highscorenamethree", "cat");

        scores[0].text = highScoreOne.ToString();
        scores[1].text = highScoreTwo.ToString();
        scores[2].text = highScoreThree.ToString();

        names[0].text = highScoreOneName;
        names[1].text = highScoreTwoName;
        names[2].text = highScoreThreeName;

    }

    public void ReturnToMenu()
    {
        FindObjectOfType<AudioManager>().Play("Bip");
        StartCoroutine(ReturnAction());
    }

    IEnumerator ReturnAction()
    {
        fade.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        FindObjectOfType<AudioManager>().StopAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

}
