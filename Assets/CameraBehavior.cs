using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public Transform rocket;
    public Transform cat;
    public CinemachineVirtualCamera cinemachine;

    // private Camera camera;
    void Start()
    {
        // camera = gameObject.GetComponent<Camera>();
        SetUniverseView();
    }

    public void SetUniverseView()
    {
        // camera.orthographicSize = 30f;
        cinemachine.m_Lens.OrthographicSize = 30f;
        cinemachine.Follow = rocket;
        var cineBody = cinemachine.GetCinemachineComponent<CinemachineFramingTransposer>();
        cineBody.m_ScreenX = 0.5f;
    }

    public void SetRunnerView()
    {
        //gameObject.transform.position
        //camera.orthographicSize = 9;
        cinemachine.m_Lens.OrthographicSize = 9f;
        cinemachine.Follow = cat;
        var cineBody = cinemachine.GetCinemachineComponent<CinemachineFramingTransposer>();
        cineBody.m_ScreenX = 0.2f;
    }
}
