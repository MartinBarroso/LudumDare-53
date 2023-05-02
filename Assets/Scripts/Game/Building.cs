using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class Building : MonoBehaviour
{
    public ZombieSpawn zombieSpawn;
    public static int count = 0;
    public int index = 0;
    public bool active = false;
    public bool delivered = false;
    public TMP_Text buildingText;
    public GameObject package;
    public string buildingMessage1 = "We already delovered your order, you have to deliver it!";
    public string buildingMessage2 = "Here is your order";
    public string buildingMessage3 = "You have no orders here";



    void Awake()
    {
        index = count;
        count++;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (active)
        {

            if (other.tag != "Player") return;
            if (delivered)
            {
                buildingText.text = buildingMessage1;
            }
            else
            {
                buildingText.text = buildingMessage2;
                delivered = true;
                zombieSpawn.SetZombieTarget(package);
                other.GetComponent<Player>().SetPackage();
            }
        }
        else
        {
            buildingText.text = buildingMessage3;
        }
    }
}