
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackIfSeesPlayerBehaviour : EnemyBehaviour
{
    public override void UpdateAI()
    {
        if (enemy.seesPlayer)
            enemy.TryAttackPlayer();
    }
}
