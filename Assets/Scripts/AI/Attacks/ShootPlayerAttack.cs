
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPlayerAttack : EnemyAttack
{
    float reloadTimer;
    Player player;

    public float reloadTime;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        if (reloadTimer > 0.0f)
            reloadTimer -= Time.deltaTime;
    }

    public override bool CanAttackPlayer()
    {
        return (reloadTimer <= 0.0f && enemy.seesPlayer);
    }

    public override void AttackPlayer()
    {
        if (!CanAttackPlayer())
            return;

        reloadTimer = reloadTime;
        transform.LookAt(player.transform);
        character.Shoot();
    }

    public override void OnEncounteredPlayer()
    {
        character.hasGun = true;
    }

    public override void OnLostPlayer()
    {
        character.hasGun = false;
    }
}
