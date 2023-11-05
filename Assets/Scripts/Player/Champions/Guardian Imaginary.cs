using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianImaginary : MonoBehaviour
{
    [Header("Atribute")]
    [SerializeField] private float guardianSpeed;
    [SerializeField] private float damage;
    [SerializeField] private float damagePenetration;
    [SerializeField] private float stopDistance;

    private void Update()
    {
        Transform target = FindNearbyObject(GameObject.FindGameObjectsWithTag("Champion"), transform);
        if(Vector2.Distance(transform.position,target.position) > stopDistance)
             transform.position = Vector2.MoveTowards(transform.position, target.transform.position, guardianSpeed * Time.deltaTime);
    }

    private Transform FindNearbyObject(GameObject[] objects, Transform guardianTransform) {
        float closestDistanceSqr = Mathf.Infinity;
        GameObject closestObject = null;

        foreach (GameObject obj in objects)
        {
            float distanceSqr = (obj.transform.position - guardianTransform.position).sqrMagnitude;
            if (distanceSqr < closestDistanceSqr)
            {
                closestDistanceSqr = distanceSqr;
                closestObject = obj;
            }
        }

        return closestObject.transform;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }
}
