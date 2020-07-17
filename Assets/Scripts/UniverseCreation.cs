using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Orbit;

public class UniverseCreation : MonoBehaviour
{
    public List<GameObject> Centers;
    public List<GameObject> Planets;
    public int UniverseSize;
    public int OrbitSpeedBase = 40;
    public int SatelliteOrbitSpeed = 40;
    public GameManager gameManager;


    void Awake()
    {
        GameObject universeCenter = gameObject;
        // add child planets to center
        AddPlanets(universeCenter, 2, 1f);
        
        // run loop based on size
        int orbitSize = 4;
        int orbitPeriod = OrbitSpeedBase;
        int planetsPerOrbit = 4;
        for (int i = 1; i <= UniverseSize; i++)
        {
            // add 4 child sun and add child planets to each one of this
            for (int p = 0; p < planetsPerOrbit; p++)
            {
                GameObject center = Instantiate(Centers[Random.Range(0, Centers.Count)], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                center.transform.parent = universeCenter.transform;
                Orbit centerScript = center.GetComponent<Orbit>();
                centerScript.orbitPath = new Ellipse(orbitSize, orbitSize);
                // set orbit progress for each sun
                centerScript.orbitProgress = (float)p / planetsPerOrbit;
                centerScript.orbitPeriod = orbitPeriod;
                AddPlanets(center, 2, 1f);
            }
            // set orbit size, amount of planets and speed based on index (orbit size base = 2 then + 2 after 4 loops)
            orbitSize += 4;
            planetsPerOrbit += 2;
            orbitPeriod += 4;
        }

    }

    void AddPlanets(GameObject center, int amount, float orbitSize)
    {
        for (int i = 1; i <= amount; i++)
        {
            GameObject planet = Instantiate(Planets[Random.Range(0, Planets.Count)], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            planet.transform.parent = center.transform;
            Orbit planetScript = planet.GetComponent<Orbit>();
            planetScript.orbitPath = new Ellipse(orbitSize, orbitSize);
            planetScript.orbitProgress = (float)i / amount;
            planetScript.orbitPeriod = SatelliteOrbitSpeed;
            orbitSize += 0.5f;
        }
        
    }

    public void GoToRunner(PlanetTypes pType)
    {
        gameManager.ViewTransition("runner", pType);
    }
}
