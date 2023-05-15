using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDefinition : MonoBehaviour
{
    public float speed = 3.0f;
    public float health = 100;
    public float detectionDistance = 5.0f;
    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget < detectionDistance)
        {
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            transform.position += directionToTarget * speed * Time.deltaTime;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
