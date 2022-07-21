using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
public class DragDrop : MonoBehaviour,  IBeginDragHandler, IEndDragHandler, IDragHandler
{
   
  


    RectTransform rectTransform;

   public CanvasGroup canvasGroup;

    [SerializeField]
    AudioClip daireBirakmaClip;



    Vector3 startPos;

    public  bool yerlestimi;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        yerlestimi = false;        
    }

    private void Start()
    {
        startPos = rectTransform.anchoredPosition;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
       //canvasGroup.blocksRaycasts = false;
        yerlestimi = false;

        int index = transform.GetSiblingIndex();
        transform.SetSiblingIndex(index + 100);


    }

    public void OnDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        Vector3 vec = Camera.main.WorldToScreenPoint(rectTransform.position);
        vec.x += eventData.delta.x;
        vec.y += eventData.delta.y;
        rectTransform.position = Camera.main.ScreenToWorldPoint(vec);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
       if (!yerlestimi)
        {
            canvasGroup.blocksRaycasts = true;
            rectTransform.anchoredPosition = startPos;
            AudioSource.PlayClipAtPoint(daireBirakmaClip, Camera.main.transform.position);
            this.transform.SetParent(GameObject.Find("dragObjeler").transform);
            yerlestimi = false; 
        }
    }

    

    


}



