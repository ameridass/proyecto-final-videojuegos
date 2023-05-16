using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 25.0f;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        

        if (other.gameObject.tag == "Enemy")
        {
            // Destruye la bala después de impactar
            Destroy(gameObject);
            print("SHOT");
        }
      
    }

}
