using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    public Transform orbitObject;
    public Ellipse orbitPath;

    [Range(0f, 1f)]
    public float orbitProgress = 0f;
    public float orbitPeriod = 3f;
    public bool orbitActive = true;
    public GameObject visitedMark;
    public enum PlanetTypes
    {
        BLUE = 0,
        YELLOW = 1,
        RED = 2
    }

    public PlanetTypes PlanetType = PlanetTypes.BLUE;

    private Vector2 orbitPos;
    private bool visited = false;

    void Start()
    {
        // lr = gameObject.AddComponent(typeof(LineRenderer)) as LineRenderer;
        if (orbitObject == null) {
            orbitActive = false;
            return;
        }

        CircleCollider2D cilCol = gameObject.AddComponent<CircleCollider2D>();
        cilCol.isTrigger = true;

        // SetOrbitingObjPos();
        // DrawOrbit();
        // StartCoroutine(AnimateOrbit());
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // Debug.Log("PLANET COLLIDER");
        if (col.gameObject.tag == "Player" && !visited)
        {
            visited = true;
            GameObject mark = Instantiate(visitedMark, gameObject.transform.position, Quaternion.identity) as GameObject;
            mark.transform.parent = gameObject.transform;
            Debug.Log("HIT WITH PLANET: " + PlanetType);
            gameObject.transform.parent.GetComponent<UniverseCreation>().GoToRunner(PlanetType);
        }
    }

    //void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.gameObject.tag == "Player" && !visited)
    //    {
    //        visited = true;
    //        // gameObject.GetComponent<SpriteRenderer>().color = new Color(104f, 104f, 104f);
    //        GameObject mark = Instantiate(visitedMark, gameObject.transform.position, Quaternion.identity) as GameObject;
    //        mark.transform.parent = gameObject.transform;
    //    }
    //}

    void SetOrbitingObjPos() {
        orbitPos = orbitPath.Evaluate(orbitProgress);
        orbitObject.localPosition = new Vector2(orbitPos.x, orbitPos.y);
        // DrawOrbit();
    }

    //void OnEnable()
    //{
    //    StartCoroutine(AnimateOrbit());
    //}

    void Update()
    {
        if (orbitPeriod < 0.1f)
        {
            orbitPeriod = 0.1f;
        }
        float orbitSpeed = 1f / orbitPeriod;
        if (orbitActive)
        {
            orbitProgress += Time.deltaTime * orbitSpeed;
            orbitProgress %= 1f;
            SetOrbitingObjPos();
            // yield return null;
        }
    }

    //IEnumerator AnimateOrbit() {
    //    if (orbitPeriod < 0.1f) {
    //        orbitPeriod = 0.1f;
    //    }
    //    float orbitSpeed = 1f / orbitPeriod;
    //    while (orbitActive) {
    //        orbitProgress += Time.deltaTime * orbitSpeed;
    //        orbitProgress %= 1f;
    //        SetOrbitingObjPos();
    //        yield return null;
    //    }
    //}

    //void DrawOrbit()
    //{
    //    int segments = 20;
    //    Vector3[] points = new Vector3[segments + 1];
    //    for (int i = 0; i < segments; i++)
    //    {
    //        Vector2 pos2d = orbitPath.Evaluate((float)i / (float)segments);
    //        points[i] = new Vector3(pos2d.x, pos2d.y, 0f);
    //    }
    //    points[segments] = points[0];
    //    lr.positionCount = segments + 1;
    //    lr.SetPositions(points);
    //}
}
