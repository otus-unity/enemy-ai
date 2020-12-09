
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerDetector : EnemyComponent
{
    public abstract bool CanSeePlayer();
}
