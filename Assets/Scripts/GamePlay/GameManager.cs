using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Transform yeniDropObjeler,dropObjeler,vagonTxtler;

    [SerializeField]
    Transform trueFalseObjeler;


    [SerializeField]
    TMP_Text dogruTxt, yanlisTxt;

    [SerializeField]
    GameObject sonucPanel,trenObje,haklarObje,tahtaObje,kitaplikObje;

    [SerializeField]
    TMP_Text sonucPanelDogruTxt, sonucPanelYanlisTxt;

    [SerializeField]
    Transform geriSayimSayilarTransform;
    [SerializeField]
    AudioClip damlaClip, trueClip,falseClip, startClip,alkisClip;

    public int kacinciSeviye;

    

    List<int> gelenList = new List<int>();
    List<int> dropList = new List<int>();

    bool sonucDogrumu;

    int trueFalseAdet,sayiAdet;

    int dogruAdet, yanlisAdet;

    SayiUretManager sayiUretManager;
    HaklarManager haklarManager;

  


    int geriSayimAdet;

    private void Awake()
    {
        sayiUretManager = Object.FindObjectOfType<SayiUretManager>();
        haklarManager = Object.FindObjectOfType<HaklarManager>();
        

        kacinciSeviye = 1;


    }

    private void Start()
    {
        sonucPanel.GetComponent<CanvasGroup>().alpha = 0f;
        sonucPanel.GetComponent<RectTransform>().localScale = Vector3.zero;

        trenObje.SetActive(true);
        haklarObje.SetActive(true);
        tahtaObje.SetActive(true);
        kitaplikObje.SetActive(true);


        tahtaObje.GetComponent<CanvasGroup>().alpha = 0f;
        haklarObje.GetComponent<CanvasGroup>().alpha = 0f;
        kitaplikObje.GetComponent<CanvasGroup>().alpha = 0f;


        geriSayimAdet = 0;

        kacinciSeviye = 1;

        for (int i = 0; i < trueFalseObjeler.childCount; i++)
        {
            trueFalseObjeler.GetChild(i).GetChild(0).GetComponent<CanvasGroup>().alpha = 0f;
            trueFalseObjeler.GetChild(i).GetChild(0).GetComponent<RectTransform>().localScale = Vector3.zero;

            trueFalseObjeler.GetChild(i).GetChild(1).GetComponent<CanvasGroup>().alpha = 0f;
            trueFalseObjeler.GetChild(i).GetChild(1).GetComponent<RectTransform>().localScale = Vector3.zero;
        }

        for (int i = 0; i < vagonTxtler.childCount; i++)
        {
            vagonTxtler.GetChild(i).GetComponent<CanvasGroup>().alpha = 0f;
            vagonTxtler.GetChild(i).GetComponent<RectTransform>().localScale = Vector3.zero;

        }


        dogruAdet = 0;
        yanlisAdet = 0;

        dogruTxt.text = dogruAdet + " Doðru";
        yanlisTxt.text = yanlisAdet + " Yanlýþ";

        

       

    }


    public void OyunuBaslat()
    {
        kitaplikObje.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
        haklarObje.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
        tahtaObje.GetComponent<CanvasGroup>().DOFade(1, 0.5f);


        StartCoroutine(SayilariAnimasyonluAcRoutine());
    }



    IEnumerator SayilariAnimasyonluAcRoutine()
    {
        yield return new WaitForSeconds(.5f);


        geriSayimSayilarTransform.GetChild(sayiAdet).gameObject.SetActive(true);
        geriSayimSayilarTransform.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
        geriSayimSayilarTransform.GetComponent<RectTransform>().DOScale(1, 0.5f);
        AudioSource.PlayClipAtPoint(damlaClip, Camera.main.transform.position);

        


        yield return new WaitForSeconds(1f);

        geriSayimSayilarTransform.GetComponent<CanvasGroup>().DOFade(0, 0.5f);
        geriSayimSayilarTransform.GetComponent<RectTransform>().DOScale(0, 0.5f);

        yield return new WaitForSeconds(0.5f);

        geriSayimSayilarTransform.GetChild(sayiAdet).gameObject.SetActive(false);


        sayiAdet++;

        if (sayiAdet == 3)
        {
            AudioSource.PlayClipAtPoint(startClip, Camera.main.transform.position);
            sayiUretManager.SayilariUret();

        }

        if (sayiAdet < geriSayimSayilarTransform.transform.childCount)
        {
            StartCoroutine(SayilariAnimasyonluAcRoutine());
        }




    }




    public void GelenListeyiBurayaEkle()
    {
        if (gelenList.Count > 0)
            gelenList.Clear();


        for (int i = 0; i < 4; i++)
        {
            gelenList.Add(sayiUretManager.toplamaList[i]);
        }
    }



    public void BirakilanSayiyiKontrolEt()
    {
      


        if(yeniDropObjeler.childCount==4)
        {
            if (dropList.Count > 0)
                dropList.Clear();

            for (int i = 0; i < dropObjeler.childCount; i++)
            {
                dropList.Add(dropObjeler.GetChild(i).GetComponent<Drop>().gelenDeger);

                yeniDropObjeler.GetChild(i).GetComponent<DragDrop>().enabled = false;
            }


            //Dizilerdeki sonuçlarý karþýlaþtýrýyoruz
            SonucuKontrolEt();
        }
    }



    public void SonucuKontrolEt()
    {
        
        sonucDogrumu = DizileriKarsilastir();

        if (sonucDogrumu)
        {
            kacinciSeviye++;

            dogruAdet++;
            dogruTxt.text =  dogruAdet +" Doðru" ;
            StartCoroutine(TrueIconlariAcRoutine());
            StartCoroutine(YeniOyunaGecRouitine());
          
            AudioSource.PlayClipAtPoint(alkisClip, Camera.main.transform.position);
           

        }
        else
        {
            yanlisAdet++;
            yanlisTxt.text = yanlisAdet + " Yanlýþ";


            AudioSource.PlayClipAtPoint(falseClip, Camera.main.transform.position);

            StartCoroutine(TrueFalseIconlariAcRoutine());


            haklarManager.kalanHak--;

            if (haklarManager.kalanHak <= 0)
            {

                haklarManager.HaklariGoster();
                Invoke("SonucPaneliniAc",3f);
            }
            else
            {
                StartCoroutine(YeniOyunaGecRouitine());
                haklarManager.HaklariGoster();
            }



        }

    }

    void SonucPaneliniAc()
    {
        trenObje.SetActive(false);
        haklarObje.SetActive(false);
        tahtaObje.SetActive(false);
        kitaplikObje.SetActive(false);


        sonucPanelDogruTxt.text = dogruAdet.ToString();
        sonucPanelYanlisTxt.text = yanlisAdet.ToString();



        sonucPanel.GetComponent<CanvasGroup>().DOFade(1, .3f);
        sonucPanel.GetComponent<RectTransform>().DOScale(1, .3f).SetEase(Ease.OutBack);
    }


    IEnumerator YeniOyunaGecRouitine()
    {
        yield return new WaitForSeconds(4f);

       

       



        sayiAdet = 3;

        while (sayiAdet >=0)
        {
            AudioSource.PlayClipAtPoint(damlaClip, Camera.main.transform.position);
            trueFalseObjeler.GetChild(sayiAdet).GetChild(0).GetComponent<CanvasGroup>().DOFade(0, .3f);
            trueFalseObjeler.GetChild(sayiAdet).GetChild(0).GetComponent<RectTransform>().DOScale(0, .3f);

            trueFalseObjeler.GetChild(sayiAdet).GetChild(1).GetComponent<CanvasGroup>().DOFade(0, .3f);
            trueFalseObjeler.GetChild(sayiAdet).GetChild(1).GetComponent<RectTransform>().DOScale(0, .3f);

            vagonTxtler.GetChild(sayiAdet).GetComponent<CanvasGroup>().DOFade(0, .3f);
            vagonTxtler.GetChild(sayiAdet).GetComponent<RectTransform>().DOScale(0, .3f);

            yeniDropObjeler.GetChild(sayiAdet).GetComponent<CanvasGroup>().DOFade(0, .3f);
            yeniDropObjeler.GetChild(sayiAdet).GetComponent<RectTransform>().DOScale(0, .3f);

            sayiAdet--;
            yield return new WaitForSeconds(0.2f);
        }


        if (yeniDropObjeler.transform.childCount > 0)
        {
            for (var i = yeniDropObjeler.transform.childCount - 1; i >= 0; i--)
            {
                Destroy(yeniDropObjeler.transform.GetChild(i).gameObject);
            }

        }

        yield return new WaitForSeconds(0.2f);

       
        sayiUretManager.SayilariUret();
    }

   

    IEnumerator TrueIconlariAcRoutine()
    {
        yield return new WaitForSeconds(.1f);

        trueFalseAdet = 0;      

        while (trueFalseAdet < trueFalseObjeler.childCount)
        {

            trueFalseObjeler.GetChild(trueFalseAdet).GetChild(0).GetComponent<CanvasGroup>().DOFade(1, .3f);
            trueFalseObjeler.GetChild(trueFalseAdet).GetChild(0).GetComponent<RectTransform>().DOScale(1, .3f).SetEase(Ease.OutBack);
            trueFalseAdet++;
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator TrueFalseIconlariAcRoutine()
    {
        yield return new WaitForSeconds(.1f);

        trueFalseAdet = 0;


        while (trueFalseAdet < trueFalseObjeler.childCount)
        {
            if(gelenList[trueFalseAdet]==dropList[trueFalseAdet])
            {
                trueFalseObjeler.GetChild(trueFalseAdet).GetChild(0).GetComponent<CanvasGroup>().DOFade(1, .3f);
                trueFalseObjeler.GetChild(trueFalseAdet).GetChild(0).GetComponent<RectTransform>().DOScale(1, .3f).SetEase(Ease.OutBack);
            } else
            {
                trueFalseObjeler.GetChild(trueFalseAdet).GetChild(1).GetComponent<CanvasGroup>().DOFade(1, .3f);
                trueFalseObjeler.GetChild(trueFalseAdet).GetChild(1).GetComponent<RectTransform>().DOScale(1, .3f).SetEase(Ease.OutBack);
            }           
            trueFalseAdet++;
            yield return new WaitForSeconds(0.2f);
        }

    }
    

    bool DizileriKarsilastir()
    {
        if (gelenList.Count != dropList.Count)
            return false;

        for (int i = 0; i < dropList.Count; i++)
        {
            if (gelenList[i] != dropList[i])
                return false;
        }

        return true;
    }



    public void YenidenOyna()
    {
        SceneManager.LoadScene("GamePlay");
    }


}
