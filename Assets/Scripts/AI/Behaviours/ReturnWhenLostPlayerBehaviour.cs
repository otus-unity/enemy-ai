
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnWhenLostPlayerBehaviour : EnemyBehaviour
{
    Vector3 startingPoint;

    void Start()
    {
        startingPoint = character.transform.position;
    }

    public override void OnLostPlayer()
    {
        character.navMeshAgent.destination = startingPoint;
        enemy.SetCurrentBehaviour(this);
    }

    public override void UpdateAI()
    {
        if (enemy.seesPlayer || Vector3.Distance(character.navMeshAgent.destination, startingPoint) < 0.1f)
            enemy.SetCurrentBehaviour(enemy.defaultBehaviour);
    }
}
