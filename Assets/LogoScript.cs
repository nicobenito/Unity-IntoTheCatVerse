using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoScript : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(OffLogo());
        FindObjectOfType<AudioManager>().Play("Logo");
    }

    IEnumerator OffLogo()
    {
        yield return new WaitForSeconds(4.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
