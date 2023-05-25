using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWallCheck : MonoBehaviour
{
    public bool isCanMove;
    private bool istouchedWall;
    private bool isTouchedPlayer;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            istouchedWall = true;
        }
        if (other.gameObject.CompareTag("Player"))
        {
            isTouchedPlayer = true;
        }
        if (isTouchedPlayer && istouchedWall)
        {
            isCanMove = false;
        }
        else
        {
            isCanMove = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            istouchedWall = false;
        }
        if (other.gameObject.CompareTag("Player"))
        {
            isTouchedPlayer = false;
        }
    }
}
