
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerDetector : PlayerDetector
{
    public float detectionDistance;
    Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    public override bool CanSeePlayer()
    {
        Vector3 origin = transform.position;
        origin.y = character.visibilityCheckSource.transform.position.y;

        Vector3 target = player.transform.position;
        target.y = origin.y;

        Vector3 direction = target - origin;

        float distanceToPlayer = direction.magnitude;
        if (distanceToPlayer > detectionDistance)
            return false;

        direction /= distanceToPlayer;
        var (hits, hitCount) = Util.castRay(origin, direction, detectionDistance);

        Character hitCharacter = null;
        int hitIndex = 0;
        while (hitIndex < hitCount) {
            hitCharacter = hits[hitIndex].transform.GetComponent<Character>();
            if (hitCharacter != character)
                break;
            ++hitIndex;
            hitCharacter = null;
        }

        if (hitIndex >= hitCount)
            return false;

        return hits[hitIndex].transform.GetComponent<Player>() != null;
    }
}
