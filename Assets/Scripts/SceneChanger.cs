using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] string Destination;

    private void OnTriggerEnter2D(Collider2D other)
    {

        SceneManager.LoadScene(Destination);
    }
}

// Scene0 : Entry Village Tent