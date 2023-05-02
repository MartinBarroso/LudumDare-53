using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private int damage;
    private List<Zombie> zombies;   
    private void OnTriggerEnter2D(Collider2D collider)
    {
       
        if (collider.CompareTag("Enemy") && !collider.GetComponent<Zombie>().hasBeenAttacked)
        {
            collider.GetComponent<Zombie>().LoseHealth(damage);
            collider.transform.GetComponent<Zombie>().hasBeenAttacked = true;

        }

    }

}
