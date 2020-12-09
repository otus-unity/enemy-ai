
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Character character;

    void Awake()
    {
        character = GetComponent<Character>();
    }
}
