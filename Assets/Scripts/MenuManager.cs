using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject background,parlaklik,logo,bulutlar, ogretmenimYazi,ilkAdimYazi,abcImg,rakamImg,okumayazmaBtn,matematikBtn;
    

    [SerializeField]
    GameObject birinciAyak,ikinciAyak,ucuncuAyak,dorduncuAyak;


    

    
    private void Start()
    {
        StartCoroutine(SahneyiAcRoutine());
    }
   IEnumerator SahneyiAcRoutine()
   {
       yield return new WaitForSeconds(.1f);

        background.GetComponent<CanvasGroup>().DOFade(1,.5f);

        yield return new WaitForSeconds(.2f);

        parlaklik.GetComponent<CanvasGroup>().DOFade(1,.5f);

         yield return new WaitForSeconds(.2f);

        logo.GetComponent<RectTransform>().DOScale(1,.5f).SetEase(Ease.OutBack);
        logo.GetComponent<CanvasGroup>().DOFade(1,.5f);

        yield return new WaitForSeconds(.2f);

         birinciAyak.GetComponent<CanvasGroup>().DOFade(1,.5f);

         yield return new WaitForSeconds(.4f);

         ikinciAyak.GetComponent<CanvasGroup>().DOFade(1,.5f);

         yield return new WaitForSeconds(.4f);

         ucuncuAyak.GetComponent<CanvasGroup>().DOFade(1,.5f);

         yield return new WaitForSeconds(.4f);

         dorduncuAyak.GetComponent<CanvasGroup>().DOFade(1,.5f);

         yield return new WaitForSeconds(.4f);


         ogretmenimYazi.GetComponent<RectTransform>().DOScale(1,.5f).SetEase(Ease.OutBack);
         ogretmenimYazi.GetComponent<CanvasGroup>().DOFade(1,.5f);

        ilkAdimYazi.GetComponent<RectTransform>().DOScale(1,.5f).SetEase(Ease.OutBack);
         ilkAdimYazi.GetComponent<CanvasGroup>().DOFade(1,.5f);

         okumayazmaBtn.GetComponent<RectTransform>().DOLocalMoveY(140,1f).SetEase(Ease.OutBack);
          yield return new WaitForSeconds(.4f);

          matematikBtn.GetComponent<RectTransform>().DOLocalMoveY(140,1f).SetEase(Ease.OutBack);

            yield return new WaitForSeconds(.4f);

        abcImg.GetComponent<RectTransform>().DOScale(1,.5f).SetEase(Ease.OutBack);
         abcImg.GetComponent<CanvasGroup>().DOFade(1,.5f);

         rakamImg.GetComponent<RectTransform>().DOScale(1,.5f).SetEase(Ease.OutBack);
         rakamImg.GetComponent<CanvasGroup>().DOFade(1,.5f);

   }


    
}
