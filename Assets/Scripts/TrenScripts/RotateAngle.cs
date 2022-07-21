using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAngle : MonoBehaviour
{
    [SerializeField]
    int donusHizi=5000;

    TrenManager trenManager;
    private void Awake()
    {
        trenManager = FindObjectOfType<TrenManager>();
    }
   void Update () {

       if(trenManager.tekerlerDonsunmu)
       {
           transform.Rotate (Vector3.forward * donusHizi * Time.deltaTime, Space.World);
       }
        
 
    }
}
