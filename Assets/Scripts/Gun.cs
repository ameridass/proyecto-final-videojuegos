using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
public class Gun : MonoBehaviour
{
    private StarterAssetsInputs _input;
    // Start is called before the first frame update
    void Start()
    {
        _input = transform.root.GetComponent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_input.shoot)
        {
            Shoot();
            _input.shoot = false;
        }
    }

    void Shoot()
    {
        Debug.Log("Shooting!");
    }
}
