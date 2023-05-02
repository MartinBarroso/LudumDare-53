using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayOrder : MonoBehaviour
{
   [SerializeField] private  TMPro.TMP_Text text;
   [SerializeField] private static string origin;
   [SerializeField] private static string end;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Take the package in " + origin + " to retrieve it in " + end +"!";
        
    }

    public static  void ChangeLocation(string newOrigin, string newEnd)
    {
        origin = newOrigin;
        end = newEnd;
    }
}
