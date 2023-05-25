using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallStuck : MonoBehaviour
{
    public static bool isStuck;
    Enemy enemy;

    private void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            enemy.TargetPosition = enemy.PickRandomTarget();
        }
    }
}
