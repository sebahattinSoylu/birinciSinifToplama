using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Drop : MonoBehaviour, IDropHandler
{

    [SerializeField]
    Transform yeniDropObjeler;

    GameManager gameManager;

    public int gelenDeger;

    [SerializeField]
    AudioClip daireBirakmaClip;

    Vector3 newPos;



    private void Awake()
    {
        gameManager = Object.FindObjectOfType<GameManager>();
       
    }

    public void OnDrop(PointerEventData eventData)
    {


        if (eventData.pointerDrag != null && !eventData.pointerDrag.GetComponent<DragDrop>().yerlestimi)
        {
           // AudioSource.PlayClipAtPoint(daireBirakmaClip, Camera.main.transform.position);
            eventData.pointerDrag.GetComponent<DragDrop>().yerlestimi = true;

            eventData.pointerDrag.transform.SetParent(yeniDropObjeler);

            AudioSource.PlayClipAtPoint(daireBirakmaClip, Camera.main.transform.position);


            gelenDeger = int.Parse(eventData.pointerDrag.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text);

            gameManager.BirakilanSayiyiKontrolEt();


            newPos = this.GetComponent<RectTransform>().anchoredPosition;
            newPos.y += 60;

            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = newPos;


            eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;


            
        }
    }
}
