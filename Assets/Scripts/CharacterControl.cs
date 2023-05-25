using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    [Header("CharacterControlSettings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed = 5f;

    [SerializeField] private FloatingJoystick joystick;

    Rigidbody playerRb;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        CharacterMove();
    }

    private void CharacterMove()
    {
        Vector3 direction = new(joystick.Horizontal, 0, joystick.Vertical);
        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        playerRb.velocity = direction * moveSpeed;
    }


}
