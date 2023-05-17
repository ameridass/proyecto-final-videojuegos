using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using System;

public class Gun : MonoBehaviour
{
    private StarterAssetsInputs _input;
    [SerializeField]
    private GameObject BulletPrefab;
    [SerializeField]
    private GameObject BulletStartShoot;
    [SerializeField]
    private float bulletSpeed = 20f;
    
    private Transform firePoint;
    // Start is called before the first frame update
    void Start()
    {
        _input = transform.root.GetComponent<StarterAssetsInputs>();
        firePoint= BulletStartShoot.transform;
    }

    // Update is called once per frame
    /*void Update()
    {
        if (_input.shoot)
        {
            Shoot();
            _input.shoot = false;
        }
    }

    /*void Shoot()
    {
        GameObject bullet = Instantiate(BulletPrefab, BulletStartShoot.transform.position, transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
        Destroy(bullet, 1);

    }*/
    void Update()
    {
        if (_input.shoot)
        {
            // Debug.Log("SHOOT");
            Shoot();
            _input.shoot = false;
        }
    }
    /*void Shoot() // intento 2
    {
        // Ray ray = Camera.main.ScreenPointToRay(BulletStartShoot.transform.position);       
        Ray ray = Camera.main.ScreenPointToRay(firePoint.position);

        GameObject bullet = Instantiate(BulletPrefab, firePoint.position, firePoint.rotation) as GameObject; 
        //GameObject bullet = Instantiate(BulletPrefab) as GameObject;

        bullet.AddComponent<Rigidbody>();
        bullet.GetComponent<Rigidbody>().mass = 3;      
        bullet.GetComponent<Rigidbody>().AddForce(ray.direction * bulletSpeed, ForceMode.Impulse);    


        bullet.AddComponent<BoxCollider>();

        RaycastHit hit;

        if (Physics.Raycast(firePoint.position, firePoint.forward, out hit))
        {         

            if(hit.collider.name == "DirtyEnemy")
            {
                // Acciones a realizar si el Raycast golpeó un objeto
                Debug.Log("Golpeaste a: " + hit.collider.name);
            }

            // Aquí puedes realizar las acciones adicionales que desees, como aplicar daño al objeto golpeado, etc.
        }
    }*/
    void Shoot()
    {
        // Crear un rayo desde el firePoint
        Ray ray = new Ray(firePoint.position, firePoint.forward);

        // Crear una instancia de la bala desde el prefab
        GameObject bullet = Instantiate(BulletPrefab, firePoint.position, firePoint.rotation);

        // Añadir un Rigidbody a la bala y configurar su velocidad
        Rigidbody rb = bullet.AddComponent<Rigidbody>();
        rb.mass = 3;
        rb.velocity = ray.direction * bulletSpeed;

        // Añadir un BoxCollider a la bala
        bullet.AddComponent<BoxCollider>();

        // Realizar el Raycast manualmente
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            EnemyDefinition enemyTarget = hit.collider.GetComponent<EnemyDefinition>();
            if (hit.collider.CompareTag("Enemy"))
            {
                // Acciones a realizar si el Raycast golpeó un objeto con la etiqueta "Enemy"
                //Debug.Log("Golpeaste a: " + hit.collider.name);

                enemyTarget.TakeDamage(10);
                Debug.Log("Enemy healt " + enemyTarget.health);
                Destroy(bullet, 1.0f);
            } else
            {
                Destroy(bullet, 2.0f);
            }
        }
    }
}
