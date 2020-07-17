using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Orbit;

public class GameManager : MonoBehaviour
{
    public GameObject UniverseView;
    public GameObject RunnerView;
    public CameraBehavior CameraScript;
    public GameObject RunnerButton;
    public GameObject UniverseJoystick;
    public GameObject FuelBar;
    public Animator Transition;
    public RocketMovementV2 rockerScript;
    public GameObject gameOverDisplay;
    public PlanetRotation planetRotationScript;
    public RocketMovementV2 rocketScript;
    public GameObject highScore;
    public GameObject tutorial;

    private float fuelTank = 0f;
    private PlanetTypes pType;
    private int fuelCount;
    private bool newHighScore = false;
    private bool shouldStart = false;
    private AudioManager audioManager;
    private bool isGameOver = false;

    void Start()
    {
        tutorial.SetActive(true);
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.Play("Universe1");
    }

    public void SwitchPage()
    {
        Animator tutAnimator = tutorial.GetComponent<Animator>();
        tutAnimator.SetTrigger("Close");
        if (shouldStart)
        {
            StartCoroutine(EndTutorial());
        }
        shouldStart = true;
    }

    public void StartGame()
    {
        StartCoroutine(EndTutorial());
    }

    IEnumerator EndTutorial()
    {
        FindObjectOfType<AudioManager>().Play("Bip");
        yield return new WaitForSeconds(2f);
        tutorial.SetActive(false);
        rockerScript.isGameOn = true;
    }

    public void ViewTransition(string view, PlanetTypes type = PlanetTypes.BLUE)
    {
        if (!isGameOver)
        {
            if (view != "universe")
                pType = type;
            StartCoroutine(SetTransition(view));
        }
    }

    IEnumerator SetTransition(string view)
    {
        Transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        if (view == "universe")
            ChangeToUniverse();
        else if (view == "runner")
            ChangeToRunner();
        else if (view == "gameover")
            DisplayGameOver();

    }
    public void ChangeToUniverse()
    {
        string song = Random.Range(1, 4).ToString();
        Debug.Log("UNIVERSE" + song);
        audioManager.StopAll();
        audioManager.Play("Universe" + song);
        UniverseView.SetActive(true);
        CameraScript.SetUniverseView();
        UniverseJoystick.SetActive(true);
        RunnerButton.SetActive(false);
        RunnerView.SetActive(false);
        FuelBar.SetActive(true);
        if(fuelCount == 0)
        {
            Debug.Log("ALL FUEL GATHERED, fuel:" + rockerScript.maxFuel);
            rockerScript.maxFuel += GetFuelReward();
            Debug.Log("FUEL UPDATE: " + GetFuelReward());
            rockerScript.FillFuel(rockerScript.maxFuel, true);
        }
        else
            rockerScript.FillFuel(fuelTank);
    }

    public void ChangeToRunner()
    {
        string song = Random.Range(1, 3).ToString();
        Debug.Log("RUNNER" + song);
        audioManager.StopAll();
        audioManager.Play("Runner" + song);
        fuelTank = 0f;
        planetRotationScript.SetPlanetDifficulty(pType);
        RunnerView.SetActive(true);
        CameraScript.SetRunnerView();
        UniverseJoystick.SetActive(false);
        RunnerButton.SetActive(true);
        UniverseView.SetActive(false);
        FuelBar.SetActive(false);
    }

    public void FillFuel(float fuel)
    {
        fuelTank += fuel;
    }

    public void DisplayGameOver()
    {
        isGameOver = true;
        audioManager.StopAll();
        audioManager.Play("GameOver");
        UniverseView.SetActive(false);
        RunnerView.SetActive(false);
        FuelBar.SetActive(false);
        UniverseJoystick.SetActive(false);
        RunnerButton.SetActive(false);
        gameOverDisplay.SetActive(true);
        rockerScript.isGameOn = false;
        if (newHighScore)
        {
            Debug.Log("Display new high score txt");
            highScore.SetActive(true);
        }
    }

    public void SetScores()
    {
        int score = rockerScript.amountDays;
        List<float> highScores = new List<float>();
        highScores.Add(PlayerPrefs.GetInt("highscoreone", 0));
        highScores.Add(PlayerPrefs.GetInt("highscoretwo", 0));
        highScores.Add(PlayerPrefs.GetInt("highscorethree", 0));

        for (int i = 0; i < highScores.Count; i++)
        {
            if(score > highScores[i])
            {
                Debug.Log("New high score, calculating...");
                string pos = "";
                switch (i)
                {
                    case 0:
                        pos = "one";
                        break;
                    case 1:
                        pos = "two";
                        break;
                    case 2:
                        pos = "three";
                        break;
                    default:
                        break;
                }
                PlayerPrefs.SetString("highscorename" + pos, GetRandomName());
                PlayerPrefs.SetInt("highscore" + pos, score);
                newHighScore = true;
                break;
            }
        }
    }

    string GetRandomName()
    {
        List<string> titles = new List<string>() { "SGT", "LT", "CPT", "MAJ", "COL", "LTG" };
        List<string> firstNames = new List<string>() { "Simba", "Tigger", "Felix", "Lucky", "Gizmo", "Kitty", "Jack", "Sasha", "Fluffy", "Muffin", "Molly", "Misty", "Princess", "Pumpkin", "Sam", "Precious", "Toby" };
        List<string> lastNames = new List<string>() { "Cathletic", "Fur-midable", "Feline", "Claw-ver", "Pretty", "Cat-atonic", "Meow", "Paws", "Paw-some", "Cat-titude", "Mew-sic", "Fur-ward", "Fitzferal", "Catsby", "Meowling", "Catperniucus", "Skywhisker" };
        List<string> extras = new List<string>() { "II", "III" };

        string randomName = titles[Random.Range(0, titles.Count)] + ". " + firstNames[Random.Range(0, firstNames.Count)] + " " + lastNames[Random.Range(0, lastNames.Count)];
        if (Random.Range(0, 4) == 1)
        {
            randomName += " " + extras[Random.Range(0, extras.Count)];
        }

        return randomName;
    }

    public void ResetAll()
    {
        Debug.Log("RESET CALL");
        StartCoroutine(ResetLevel());
    }

    public void GoToMenu()
    {
        StartCoroutine(GoMenu());
    }

    IEnumerator GoMenu()
    {
        Transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    IEnumerator ResetLevel()
    {
        Transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SetFuelCount(int count)
    {
        fuelCount = count;
    }

    public void UpdateFuelCount(int updateValue)
    {
        fuelCount -= updateValue;
    }

    float GetFuelReward()
    {
        float reward = 0;
        switch (pType)
        {
            case PlanetTypes.BLUE:
                reward = 20f;
                break;
            case PlanetTypes.YELLOW:
                reward = 40f;
                break;
            case PlanetTypes.RED:
                reward = 90f;
                break;
            default:
                Debug.LogError("Unrecognized Planet Type");
                break;
        }
        return reward;
    }
}

