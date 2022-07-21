using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using DG.Tweening;



public class SayiUretManager : MonoBehaviour
{


    [SerializeField]
    Transform trenTxtler;

    [SerializeField]
    Transform dragObjeler, dragObjeYerler;

    [SerializeField]
    GameObject dragObjePrefab;


    [SerializeField]
    public List<int> toplamaList = new List<int>();

    [SerializeField]
    AudioClip bubbleClip;

    List<Vector3> dropObjeYerlerList = new List<Vector3>();

    List<int> ayicikYaziList = new List<int>();

    int birinciSayi, ikinciSayi;

    string yazilacakYazi;

    int sayiAdet,ayicikAdet;



    GameManager gameManager;
    


    private void Awake()
    {
        gameManager = Object.FindObjectOfType<GameManager>();
      

    }

  

    public void SayilariUret()
    {
        if (toplamaList.Count > 0)
            toplamaList.Clear();

        if (ayicikYaziList.Count > 0)
            ayicikYaziList.Clear();



        if (gameManager.kacinciSeviye<=5)
        {
            for (int i = 4; i < 8; i++)
            {
                toplamaList.Add(i);
            }
        } 

        if(gameManager.kacinciSeviye>5 && gameManager.kacinciSeviye<=10)
        {
            for (int i = 6; i < 12; i++)
            {
                toplamaList.Add(i);
            }
        }

        if (gameManager.kacinciSeviye > 10 && gameManager.kacinciSeviye <= 15)
        {
            for (int i = 9; i < 15; i++)
            {
                toplamaList.Add(i);
            }
        }

        if (gameManager.kacinciSeviye > 15)
        {
            for (int i = 13; i < 21; i++)
            {
                toplamaList.Add(i);
            }
        }


        toplamaList = toplamaList.OrderBy(i => Random.value).ToList();


        


        StartCoroutine(TrenTextleriCikarRoutine());
        StartCoroutine(AyiciklariOlusturRoutine());

        
    }


    IEnumerator TrenTextleriCikarRoutine()
    {
        for (int i = 0; i < trenTxtler.childCount; i++)
        {
            trenTxtler.GetChild(i).GetComponent<CanvasGroup>().alpha = 0f;
            trenTxtler.GetChild(i).GetComponent<RectTransform>().localScale = Vector3.zero;
            trenTxtler.GetChild(i).GetComponent<TextMeshProUGUI>().text = TexteYazdir(toplamaList[i]);
        }

        yield return new WaitForSeconds(.1f);


        gameManager.GelenListeyiBurayaEkle();

        sayiAdet = 0;


        

        while (sayiAdet < trenTxtler.childCount)
        {

            trenTxtler.GetChild(sayiAdet).GetComponent<CanvasGroup>().DOFade(1, .3f);
            trenTxtler.GetChild(sayiAdet).GetComponent<RectTransform>().DOScale(1, .3f).SetEase(Ease.OutBack);


            sayiAdet++;

            yield return new WaitForSeconds(0.2f);
        }

    }


    IEnumerator AyiciklariOlusturRoutine()
    {
        yield return new WaitForSeconds(.01f);


        ayicikYaziList = ayicikYaziList.OrderBy(i => Random.value).ToList();

        yield return new WaitForSeconds(.1f);

        for (int i = 0; i < 4; i++)
        {
            GameObject dragObje = Instantiate(dragObjePrefab);

            dragObje.GetComponent<RectTransform>().position = dragObjeYerler.GetChild(i).GetComponent<RectTransform>().position;

            dragObje.GetComponent<RectTransform>().localScale = Vector3.zero;

            dragObje.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = ayicikYaziList[i].ToString();


            dragObje.transform.SetParent(dragObjeler);
        }


        ayicikAdet = 0;




        while (ayicikAdet < dragObjeler.childCount)
        {

            dragObjeler.GetChild(ayicikAdet).GetComponent<CanvasGroup>().DOFade(1, .3f);
            dragObjeler.GetChild(ayicikAdet).GetComponent<RectTransform>().DOScale(1, .3f).SetEase(Ease.OutBack);
            AudioSource.PlayClipAtPoint(bubbleClip, Camera.main.transform.position);

            ayicikAdet++;

            yield return new WaitForSeconds(0.2f);
        }
        

      
        




    }




    //vagonlar üzerine yazýlacak sayilari üreten fonksiyon
    string TexteYazdir(int listeDegeri)
    {
        birinciSayi = listeDegeri - Random.Range(1, listeDegeri - 1);
        ikinciSayi = listeDegeri - birinciSayi;

        ayicikYaziList.Add(birinciSayi + ikinciSayi);

        yazilacakYazi= birinciSayi.ToString() + "+" + ikinciSayi.ToString();

        return yazilacakYazi;        
    }

   
}
