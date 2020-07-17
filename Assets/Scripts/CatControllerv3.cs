using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatControllerv3 : MonoBehaviour
{
    public float StepDistance;
    public float Speed;
    public Button btn;
    public GameManager gameManager;

    private Vector2 targetPos;
    private float initalPos;
    private float stepDirection;
    private int step = 1;

    private int catLife = 3;
    private Animator catAnimator;
    private int fuelCount = 0;
    private bool gameIsOver;
    private AudioManager audioManager;

    void Awake()
    {
        catAnimator = gameObject.GetComponent<Animator>();
        btn.onClick.AddListener(MakeMove);
        initalPos = transform.position.y;
        targetPos = transform.position;
        stepDirection = StepDistance;
        gameIsOver = false;
        audioManager = FindObjectOfType<AudioManager>();
    }
    void Update()
    {
        if (catLife <= 0 && !gameIsOver)
        {
            gameIsOver = true;
            gameManager.SetScores();
            gameManager.ViewTransition("gameover");
        }
        transform.position = Vector2.MoveTowards(transform.position, targetPos, Speed * Time.deltaTime);
    }

    void MakeMove()
    {
        if (transform.position.y == targetPos.y)
        {
            if (step == 3)
            {
                stepDirection = stepDirection * -1;
                step = 1;
            }
            step++;

            targetPos = new Vector2(transform.position.x, transform.position.y + stepDirection);
        }
        

    }

    void OnEnable()
    {
        fuelCount = 0;
        step = 1;
        stepDirection = StepDistance;
        targetPos = new Vector2(transform.position.x, initalPos);
        catLife = 3;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // Debug.Log("CAT COLLIDER: " + col.gameObject.tag);
        if (col.gameObject.tag == "Gem")
        {
            audioManager.Play("Fuel");
            col.gameObject.SetActive(false);
            gameManager.FillFuel(200f);
            gameManager.UpdateFuelCount(1);
            fuelCount++;
        }
        else if (col.gameObject.tag == "Meteor")
        {
            audioManager.Play("Meteor");
            audioManager.Play("CatDamage");
            col.gameObject.SetActive(false);
            catLife -= 1;
            catAnimator.SetTrigger("Hit");
        }
    }

}
