
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeCoverBehaviour : EnemyBehaviour
{
    public override void OnEncounteredPlayer()
    {
        var nearestCover = Util.FindNearest(transform.position, FindObjectsOfType<CoverPoint>());
        if (nearestCover != null) {
            character.navMeshAgent.destination = nearestCover.transform.position;
            enemy.SetCurrentBehaviour(this);
        }
    }

    public override void UpdateAI()
    {
        if (enemy.seesPlayer)
            enemy.TryAttackPlayer();
    }
}
