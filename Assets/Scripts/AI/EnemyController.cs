
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    HashSet<Enemy> enemies = new HashSet<Enemy>();
    public bool playerSeen { get; private set; }

    public void AddEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
    }

    public void RemoveEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);
    }

    void Update()
    {
        playerSeen = false;
        foreach (var enemy in enemies) {
            if (enemy.playerRaycastSucceeded) {
                playerSeen = true;
                break;
            }
        }
    }
}
