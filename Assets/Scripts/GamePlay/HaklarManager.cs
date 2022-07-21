using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaklarManager : MonoBehaviour
{
    [SerializeField]
    GameObject hak1_Img, hak2_Img, hak3_Img;

    public int kalanHak;

    void Start()
    {
        kalanHak = 3;

        HaklariGoster();
    }


    public void HaklariGoster()
    {
        if(kalanHak==3)
        {
            hak1_Img.SetActive(true);
            hak2_Img.SetActive(true);
            hak3_Img.SetActive(true);
        } else if(kalanHak==2)
        {
            hak1_Img.SetActive(true);
            hak2_Img.SetActive(true);
            hak3_Img.SetActive(false);
        }
        else if (kalanHak == 1)
        {
            hak1_Img.SetActive(true);
            hak2_Img.SetActive(false);
            hak3_Img.SetActive(false);
        }
        else if (kalanHak == 0)
        {
            hak1_Img.SetActive(false);
            hak2_Img.SetActive(false);
            hak3_Img.SetActive(false);
        }
    }
   
}
