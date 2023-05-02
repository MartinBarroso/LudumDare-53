using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class House : MonoBehaviour
{
    [SerializeField] private TMP_Text deliverText = null;
    [SerializeField] private Canvas canvas = null;
    [SerializeField] private int messageTime = 3;
    [SerializeField] private AudioClip successClip;


    public static int count = 0;
    public int index = 0;
    public bool active = false;
    public bool killOnStart = false;

    public int position = 0;
    public bool hasPosition = false;

    string deliveryMessage = "Thanks capo!";

    void Awake()
    {
        index = count;
        count++;
    }

    void Start()
    {
        if (killOnStart)
        {
            Destroy(this);
            count--;
        }
    }
    
    IEnumerator ShowDeliverMessage()
    {
        deliverText.text = deliveryMessage;
        yield return new WaitForSeconds(messageTime);
        canvas.enabled = false;
    }

    private void ShowMessage()
    {
        StartCoroutine(ShowDeliverMessage());
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!active) return;
        if(collision.CompareTag("Player"))
        {
            Player p = collision.GetComponent<Player>();
            if (Player.hasPackage)
            {
                ShowMessage();
                p.DropPackage();
                SoundManager.Instance.PlaySound(successClip);
                Bag.ResetBag(Destination.HOUSE);
            }
        }
    }

}
