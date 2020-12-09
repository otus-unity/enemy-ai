
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyController controller;
    public EnemyBehaviour defaultBehaviour;

    Character character;
    EnemyComponent[] enemyComponents;
    EnemyAttack[] enemyAttacks;
    PlayerDetector[] playerDetectors;

    public EnemyBehaviour currentBehaviour { get; private set; }
    public bool playerRaycastSucceeded { get; private set; }
    public bool seesPlayer { get; private set; }

    void Awake()
    {
        character = GetComponent<Character>();
        enemyComponents = GetComponents<EnemyComponent>();
        enemyAttacks = GetComponents<EnemyAttack>();
        playerDetectors = GetComponents<PlayerDetector>();
    }

    void Start()
    {
        currentBehaviour = defaultBehaviour;
        if (controller != null)
            controller.AddEnemy(this);
    }

    void OnDestroy()
    {
        if (controller != null)
            controller.RemoveEnemy(this);
    }

    public void SetCurrentBehaviour(EnemyBehaviour behaviour)
    {
        currentBehaviour = behaviour;
    }

    public bool TryAttackPlayer()
    {
        EnemyAttack selectedAttack = null;
        int selectedPriority = 0;

        foreach (var attack in enemyAttacks) {
            if (!attack.CanAttackPlayer())
                continue;

            int priority = attack.priority;
            if (selectedAttack == null || priority > selectedPriority) {
                selectedAttack = attack;
                selectedPriority = priority;
            }
        }

        if (selectedAttack == null)
            return false;

        selectedAttack.AttackPlayer();
        return true;
    }

    public void NotifyPlayerSeen()
    {
        foreach (var component in enemyComponents)
            component.OnEncounteredPlayer();
    }

    public void NotifyPlayerLost()
    {
        foreach (var component in enemyComponents)
            component.OnLostPlayer();
    }

    void Update()
    {
        if (character.isDead)
            return;

        playerRaycastSucceeded = false;
        foreach (var detector in playerDetectors) {
            if (detector.CanSeePlayer()) {
                playerRaycastSucceeded = true;
                break;
            }
        }

        bool seesPlayerThisFrame = playerRaycastSucceeded;
        if (!seesPlayerThisFrame && controller != null && controller.playerSeen)
            seesPlayerThisFrame = true;

        if (seesPlayerThisFrame != seesPlayer) {
            seesPlayer = seesPlayerThisFrame;
            if (seesPlayer)
                NotifyPlayerSeen();
            else
                NotifyPlayerLost();
        }

        if (currentBehaviour != null)
            currentBehaviour.UpdateAI();
    }
}
