
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAttack : EnemyComponent
{
    public int priority;

    public abstract bool CanAttackPlayer();
    public abstract void AttackPlayer();
}
