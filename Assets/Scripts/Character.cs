
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    const float ShootTraceTime = 0.1f;

    public Transform shootSource;
    public Transform visibilityCheckSource;
    public LineRenderer shootTrace;
    public ParticleSystem hitParticles;
    public ParticleSystem bloodParticles;

    public Animator animator { get; private set; }
    public NavMeshAgent navMeshAgent { get; private set; }
    public Health health { get; private set; }

    public bool isDead { get; private set; }
    public bool running { set { animator.SetBool("run", value); } }
    public bool hasGun { set { animator.SetBool("hasGun", value); } }

    new Rigidbody rigidbody;
    new Collider collider;
    float shootTraceTimer = 0.0f;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        collider = GetComponent<Collider>();
        rigidbody = GetComponent<Rigidbody>();
        health = GetComponent<Health>();
    }

    void Update()
    {
        if (shootTraceTimer > 0.0f) {
            shootTraceTimer -= Time.deltaTime;
            if (shootTraceTimer <= 0.0f)
                shootTrace.enabled = false;
        }
    }

    public void SetVelocity(Vector3 velocity)
    {
        animator.SetBool("moving", velocity.magnitude > 0.001f);
        navMeshAgent.velocity = velocity;
    }

    public void Shoot()
    {
        if (shootTraceTimer > 0.0f)
            return;

        Vector3 start = shootSource.position;
        var (hits, hitCount) = Util.castRay(start, transform.TransformDirection(Vector3.forward));

        Character character = null;
        int hitIndex = 0;
        while (hitIndex < hitCount) {
            character = hits[hitIndex].transform.GetComponent<Character>();
            if (character != this)
                break;
            ++hitIndex;
            character = null;
        }

        if (hitIndex < hitCount) {
            if (character != null)
                character.TakeDamage();
            else {
                hitParticles.transform.SetParent(null, false);
                hitParticles.transform.position = hits[hitIndex].point;
                hitParticles.Play();
            }

            shootTrace.SetPosition(0, start);
            shootTrace.SetPosition(1, hits[hitIndex].point);
            shootTrace.enabled = true;
            shootTraceTimer = ShootTraceTime;
        }
    }

    public void TakeDamage()
    {
        if (health != null) {
            --health.value;
            if (health.value <= 0) {
                isDead = true;
                animator.SetBool("dead", true);
                Destroy(rigidbody);
                Destroy(collider);
                Destroy(navMeshAgent);
            }
        }

        bloodParticles.Play();
    }
}
