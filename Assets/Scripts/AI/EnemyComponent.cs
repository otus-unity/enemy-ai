
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyComponent : MonoBehaviour
{
    public Character character { get; private set; }
    public Enemy enemy { get; private set; }

    protected virtual void Awake()
    {
        character = GetComponent<Character>();
        enemy = GetComponent<Enemy>();
    }

    public virtual void OnEncounteredPlayer() {}
    public virtual void OnLostPlayer() {}
}
