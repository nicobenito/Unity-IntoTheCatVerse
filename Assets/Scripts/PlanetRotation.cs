using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Orbit;

public class PlanetRotation : MonoBehaviour
{
    public float RotationSpeed = 15f;
    public bool RotationActive = true;
    public int ObjectsTotal = 1;
    public List<GameObject> Objects;
    public Transform planetCenter;
    public GameManager gameManager;

    public PlanetTypes PlanetType = PlanetTypes.BLUE;

    private Transform pTransform;
    private bool firstRun = true;
    private List<GameObject> createdObs;
    private int fuelCount = 0;
    
    void Start()
    {
        GetPlanetDifficulty();
        createdObs = new List<GameObject>();
        pTransform = gameObject.GetComponent<Transform>();
        SetObjects();
    }

    public void OnEnable()
    {
        // Debug.Log("RESET PLANET");
        gameObject.transform.rotation = Quaternion.identity;
        ToggleRotation(true);
        // gameObject.GetComponent<Transform>().Rotate(new Vector3(0f, 0f, 0f));
        if (!firstRun) // small fix to avoid two renders on the first run mode
        {
            GetPlanetDifficulty();
            SetObjects();
        }
        firstRun = false;
    }

    void GetPlanetDifficulty()
    {
        Debug.Log("Planet type: " + PlanetType);
        switch (PlanetType)
        {
            case PlanetTypes.BLUE:
                RotationSpeed = 20f;
                ObjectsTotal = 15;
                break;
            case PlanetTypes.YELLOW:
                RotationSpeed = 30f;
                ObjectsTotal = 25;
                break;
            case PlanetTypes.RED:
                RotationSpeed = 40f;
                ObjectsTotal = 25;
                break;
            default:
                Debug.LogError("Unrecognized Planet Type");
                break;
        }
    }

    public void SetPlanetDifficulty(PlanetTypes pType)
    {
        PlanetType = pType;
    }
    void Update()
    {
        if (RotationActive) { pTransform.Rotate(Vector3.forward * (RotationSpeed * Time.deltaTime)); }
    }

    public void ToggleRotation(bool value)
    {
        RotationActive = value;
    }

    void SetObjects()
    {
        fuelCount = 0;
        for (int i = 0; i < createdObs.Count; i++)
        {
            Destroy(createdObs[i]);
        }
        int columnValue = 3;
        List<float> objPositions = new List<float>() { 17.5f, 19.5f, 21.5f };
        float angle = 360f / (float)ObjectsTotal;
        for (int i = 0; i < ObjectsTotal; i++)
        {
            if (i * angle > 110f || i * angle < 35f) // fix to not render objects on the start of the runner
            {
                Quaternion rotation = Quaternion.AngleAxis(i * angle, Vector3.forward);
                Vector3 direction = rotation * Vector3.right;
                Quaternion fixedRotation = rotation * Quaternion.Euler(0, 0, -90);
                List<float> availablePos = new List<float>(objPositions);
                for (int j = 0; j < columnValue - 1; j++)
                {
                    if (UnityEngine.Random.Range(1, 4) <= 2)
                    {
                        int selectedPos = UnityEngine.Random.Range(0, availablePos.Count);
                        Vector3 position = planetCenter.position + (direction * availablePos[selectedPos]); // distance from center
                        GameObject ob = Instantiate(Objects[UnityEngine.Random.Range(0, Objects.Count)], position, fixedRotation);
                        if (ob.tag == "Gem")
                        {
                            fuelCount++;
                        }
                        ob.transform.parent = pTransform;
                        createdObs.Add(ob);
                        availablePos.RemoveAt(selectedPos);
                    }
                }       
            }            
        }
        gameManager.SetFuelCount(fuelCount);
    }
}
