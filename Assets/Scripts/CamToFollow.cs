using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamToFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;

    Vector3 offset = new(0f, 10f, -8f);

    private void LateUpdate()
    {
        Vector3 currentPos = player.transform.position + offset;
        transform.position = currentPos;
    }
}
