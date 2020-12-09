
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public float rotateSpeed;

    public Character character { get; private set; }

    void Start()
    {
        character = GetComponent<Character>();
        character.running = true;
        character.hasGun = true;
    }

    void Update()
    {
        if (!character.isDead)
            HandleInput();
    }

    void HandleInput()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        transform.rotation *= Quaternion.AngleAxis(x * rotateSpeed, Vector3.up);

        Vector3 direction = transform.TransformDirection(new Vector3(0.0f, 0.0f, y));
        direction.y = 0.0f;

        character.SetVelocity(direction * moveSpeed);

        if (Input.GetMouseButtonDown(0))
            character.Shoot();
    }
}
