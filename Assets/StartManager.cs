using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    public Animator fadeAnimator;
    private AudioManager audioManager;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.Play("StartMenu");
    }

    public void StartGame()
    {
        audioManager.Play("Bip");
        StartCoroutine(SetLevel());
    }

    public void GoHighScores()
    {
        audioManager.Play("Bip");
        StartCoroutine(HighScores());
    }

    IEnumerator HighScores()
    {
        fadeAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    IEnumerator SetLevel()
    {
        fadeAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
