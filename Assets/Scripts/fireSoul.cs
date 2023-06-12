using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireSoul : MonoBehaviour
{
    public GameObject fireSoulObj;
    public PlayerCombat playerCombat;
    public GameObject canvas;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canvas.SetActive(true);
            StartCoroutine(DeactivateCanvasAfterDelay(5f));
            Debug.Log("Soul Acquired");
            playerCombat.wep = 1;
            fireSoulObj.SetActive(false);

        }
    }
    private IEnumerator DeactivateCanvasAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canvas.SetActive(false);
    }
}
