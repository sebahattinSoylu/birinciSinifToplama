using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TrenManager : MonoBehaviour
{
    [SerializeField]
    GameObject tren;

    public bool tekerlerDonsunmu;

    GameManager gameManager;

    private void Awake()
    {
        gameManager = Object.FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        StartCoroutine(SahneyiAcRoutine());
        tekerlerDonsunmu=true;
    }

     IEnumerator SahneyiAcRoutine()
    {
       // yield return new WaitForSeconds(.1f);

        tren.GetComponent<RectTransform>().DOAnchorPosX(0,2.02f);
       
         yield return new WaitForSeconds(2f);
         tekerlerDonsunmu=false;

        gameManager.OyunuBaslat();

    }


}
