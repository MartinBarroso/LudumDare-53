using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public struct DeliveryOrder
    {
        public int house;
        public int building;
    }


    [SerializeField] private Transform houses = null;
    [SerializeField] private Transform buildings = null;
    [SerializeField] private AudioClip notif;
    [SerializeField] private House[] houseList;
    private static Building[] buildingList;

    private DeliveryOrder actualOrder;
    private DeliveryOrder nextOrder;




    private void Awake()
    {
        // houseList = houses.GetComponentsInChildren<House>();
        foreach (House h in houseList)
        {
            Debug.Log("House " + h.name + " is index " + h.index);
        }
        buildingList = buildings.GetComponentsInChildren<Building>();
        nextOrder = CreateDeliveryRequest();
        Bag.OnArrive += SwapDeliveryOrders;
    }

    private void OnDestroy()
    {
        Bag.OnArrive -= SwapDeliveryOrders;
    }

    private void Start()
    {
        SwapDeliveryOrders(0);
    }

    private void SwapDeliveryOrders(int score)
    {
        SoundManager.Instance.PlaySound(notif);
        buildingList[actualOrder.building].delivered = false;
        buildingList[actualOrder.building].active = false;
        houseList[actualOrder.house].active = false;
        actualOrder = nextOrder;
        buildingList[actualOrder.building].active = true;
        houseList[actualOrder.house].active = true;
        Bag.Attach(buildingList[actualOrder.building].transform);
        Bag.Instance.SetDeliveryOrder(actualOrder);
        nextOrder = CreateDeliveryRequest();
    }

    private DeliveryOrder CreateDeliveryRequest()
    {
        DeliveryOrder delorder;
        delorder.house = UnityEngine.Random.Range(0, houseList.Length);
        delorder.building = UnityEngine.Random.Range(0, buildingList.Length);
        Debug.Log("Deliver Order> From: Building " + delorder.building + " - To: House " + delorder.house);
        DisplayOrder.ChangeLocation(buildingList[actualOrder.building].name,houseList[actualOrder.house].name);
        return delorder;
    }




}
