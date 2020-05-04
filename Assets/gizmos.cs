using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{

    public float Attack_range;
    public float Chase_Range;
    public float MoveSpeed;
    public float RotSpeed;
    public float Destination;
    public float Distance;
    public LayerMask targetMask;
    public LayerMask obstacleMask;
    public float viewAngle;
    public List<Transform> visibleTargets = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        visibleTargets.Clear();

        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, Chase_Range, targetMask);
        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;

            Vector3 dirToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2 && Vector3.Angle(transform.forward, dirToTarget) >= (-viewAngle) / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);

                    Debug.DrawRay(transform.position, target.position - transform.position, Color.green);
                }
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Attack_range);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, Chase_Range);
    }
}
