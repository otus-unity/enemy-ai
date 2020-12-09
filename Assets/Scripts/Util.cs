
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    class RaycastHitComparer : IComparer<RaycastHit>
    {
        public static RaycastHitComparer instance = new RaycastHitComparer();

        public int Compare(RaycastHit a, RaycastHit b)
        {
            if (a.distance < b.distance)
                return -1;
            else if (a.distance > b.distance)
                return 1;
            else
                return 0;
        }
    }

    static RaycastHit[] raycastHits = new RaycastHit[512];

    public static (RaycastHit[], int) castRay(Vector3 start, Vector3 direction)
    {
        int hitCount = Physics.RaycastNonAlloc(start, direction, raycastHits);
        Array.Sort(raycastHits, 0, hitCount, RaycastHitComparer.instance);
        return (raycastHits, hitCount);
    }

    public static (RaycastHit[], int) castRay(Vector3 start, Vector3 direction, float distance)
    {
        int hitCount = Physics.RaycastNonAlloc(start, direction, raycastHits, distance);
        Array.Sort(raycastHits, 0, hitCount, RaycastHitComparer.instance);
        return (raycastHits, hitCount);
    }
}
