using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketMovementV2 : MonoBehaviour
{
    public float Speed = 5f;
    public float RotateSpeed = 200f;
    public Joystick joystick;
    public FuelBarBehavior fuelBar;
    public GameManager gameManager;
    public GameObject ignition;
    public float maxFuel;
    public Animator rechargedAnimator;
    public Animator maxFuelAnimator;
    public Text Days;
    public Text Months;
    public Text Years;
    public bool isGameOn;

    private Rigidbody2D rb;
    private float fuel;
    private int days;
    private int months;
    private int years;
    private float currentTime;
    private float timeMark;
    public int amountDays;
    private bool gameIsOver;
    private AudioManager audioManager;
    private bool isPlayingSound = false;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        maxFuel = fuel = 300f;
        fuelBar.SetMaxFuel(fuel);
        days = 0;
        timeMark = 25;
        currentTime = 0;
        amountDays = 0;
        gameIsOver = false;
        isGameOn = false;
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void FillFuel(float refill, bool displayMaxText = false)
    {
        if (displayMaxText)
            maxFuelAnimator.SetTrigger("MaxFuel");
;        if(refill > 0)
        {
            rechargedAnimator.SetTrigger("Recharged");
        }
        if (fuel + refill > maxFuel)
            fuel = maxFuel;
        else
            fuel += refill;
        
        fuelBar.SetMaxFuel(maxFuel);
        fuelBar.SetFuel(fuel);
    }

    void FixedUpdate()
    {
        if (isGameOn)
        {
            float xAxis = joystick.Horizontal;
            float yAxis = joystick.Vertical;
            if (fuel > 0)
            {
                if (xAxis == 0f && yAxis == 0f)
                {
                    audioManager.Stop("Rocket");
                    isPlayingSound = false;
                    fuel -= maxFuel * 0.00025f;
                    rb.velocity = rb.velocity * 0.97f;
                    ignition.SetActive(false);
                }
                else
                {
                    if(!isPlayingSound)
                        audioManager.Play("Rocket");
                    isPlayingSound = true;
                    ignition.SetActive(true);
                    Vector2 direction = new Vector2(xAxis, yAxis);
                    direction.Normalize();

                    float rotateAmount = Vector3.Cross(direction, transform.up).z;

                    rb.angularVelocity = -rotateAmount * RotateSpeed;

                    rb.velocity = transform.up * Speed;
                    fuel -= 0.5f;
                }
            }
            else if (!gameIsOver)
            {
                gameIsOver = true;
                gameManager.SetScores();
                gameManager.ViewTransition("gameover");
            }
            fuelBar.SetFuel(fuel);
            currentTime += Time.timeSinceLevelLoad;
            if (currentTime > timeMark)
            {
                timeMark = currentTime + 25;
                currentTime = 0;
                days++;
                amountDays++;
                if (days == 30)
                {
                    days = 0;
                    months++;
                }
                if (months == 12)
                {
                    months = 0;
                    years++;
                }
                Days.text = days.ToString();
                Months.text = months.ToString();
                Years.text = years.ToString();
            }
        }
    }
}
