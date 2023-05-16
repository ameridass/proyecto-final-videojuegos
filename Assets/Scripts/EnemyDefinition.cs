using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDefinition : MonoBehaviour
{
    public float speed = 3.0f;
    public float health = 100;
    public float detectionDistance = 5.0f;
    private Transform target;
    public int routine;
    public float chronometer;
    public Animator animator;
    public Quaternion angle;
    public float degree;
    public bool attack;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        animator.SetBool("isDead", false);
    }

    // Update is called once per frame
    void Update()
    {
        comportamiento();
       
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            animator.SetBool("isDead", true);
            Die();
        }
    }
    public void comportamiento()
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget > detectionDistance)
        {
            //Vector3 directionToTarget = (target.position - transform.position).normalized;
            //transform.position += directionToTarget * speed * Time.deltaTime;
            endAttack();
            animator.SetBool("run", false);
            chronometer += 1 * Time.deltaTime;
            if (chronometer >= 4)
            {
                routine = Random.Range(0, 2);
                chronometer = 0;
            }
            switch (routine)
            {
                case 0:
                    animator.SetBool("walk", false);
                    break;
                case 1:
                    degree = Random.Range(0, 360);
                    angle = Quaternion.Euler(0, degree, 0);
                    routine++;
                    break;
                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, angle, 0.5f);
                    transform.Translate(Vector3.forward * 1 * Time.deltaTime);
                    animator.SetBool("walk", true);
                    break;
            }
        }
        else
        {

            if (distanceToTarget > 1 && !attack)
            {
                var lookPos = target.transform.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                animator.SetBool("walk", false);
                animator.SetBool("run", true);
                transform.Translate(Vector3.forward * 2 * Time.deltaTime);
                animator.SetBool("attack", false);
            }
            else
            {
                animator.SetBool("walk", false);
                animator.SetBool("run", false);

                animator.SetBool("attack", true);
                attack = true;
            }
               

        }
    }
    public void endAttack()
    {
        animator.SetBool("attack", false);
        attack = false;
    }

    public void Die()
    {
        
        Destroy(gameObject, 5f);
    }

}
