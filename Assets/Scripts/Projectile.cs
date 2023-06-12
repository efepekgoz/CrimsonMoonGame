using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private PlayerCombat playerCombat;

    private void Start()
    {
        playerCombat = FindObjectOfType<PlayerCombat>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Player")
        {
            Debug.Log("Damage");
            playerCombat.playerHp -= 40;

        }
        if (collision.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
