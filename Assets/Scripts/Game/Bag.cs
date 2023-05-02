using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Destination
{
    CEMENTERY,
    HOUSE
}

public class Bag : MonoBehaviour
{
    [SerializeField] private static int deliveryScore = 1;

    private static Bag instance;

    public static bool isGrabbed = false;

    public static Action<int> OnArrive;

    public static int deliveryObjetive;
    public static int deliveryInitial;

    public SpriteRenderer sr = null;

    public static Bag Instance
    {
        get
        {
            // Si no hay una instancia existente, intenta encontrarla en la escena
            if (instance == null)
            {
                instance = FindObjectOfType<Bag>();

                // Si no se encontró en la escena, crea un nuevo GameObject y adjunta el componente
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<Bag>();
                    singletonObject.name = "MySingleton";
                    //DontDestroyOnLoad(singletonObject);
                }
            }

            return instance;
        }
    }
    private void Awake()
    {
        // Asegura que solo haya una instancia del Singleton en la escena
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        sr = instance.GetComponent<SpriteRenderer>();
    }
    public static void ResetBag(Destination destination)
    {
        OnArrive(destination == Destination.CEMENTERY ? 0 : deliveryScore);
        Score.AddScore(deliveryScore);
    }

    private void Update()
    {
        sr.enabled = !isGrabbed;
    }

    public static void Attach(Transform target)
    {
        instance.transform.position = target.position;
        instance.sr.enabled = false;
    }
    
    public void SetDeliveryOrder(GameManager.DeliveryOrder deliveryOrder)
    {
        deliveryObjetive = deliveryOrder.house;
        deliveryInitial = deliveryOrder.building;
    }

}
