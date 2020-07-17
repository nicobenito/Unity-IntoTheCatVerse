using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarShipController : MonoBehaviour
{

    public GameManager gameManager;
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            gameObject.transform.parent.GetComponent<PlanetRotation>().ToggleRotation(false);
            gameManager.ViewTransition("universe");
        }        
    }
}
