using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerCombat playerCombat;

    void Start()
    {
        playerCombat.wep = 1;
    }

}
