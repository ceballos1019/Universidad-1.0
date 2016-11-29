using UnityEngine;
using System.Collections;
using  UnityEngine.UI;
using  UnityEngine.EventSystems;

public class VirtualJoystic : MonoBehaviour,IDragHandler,IPointerUpHandler, IPointerDownHandler {
	private Image img;
	private Image Joysticimg;

	public Vector3 InputDirection{ set; get;}

	private void Start(){
		img= GetComponent<Image>();
		Joysticimg= transform.GetChild(0).GetComponent<Image>();
		InputDirection = Vector3.zero;
	}


	public virtual void OnDrag(PointerEventData ped){
	
		Vector2 pos = Vector2.zero;
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle 
			(img.rectTransform,ped.position,ped.pressEventCamera,out pos))
		{
			pos.x = (pos.x / img.rectTransform.sizeDelta.x);
				pos.y=(pos.y/img.rectTransform.sizeDelta.y); 
			float x = (img.rectTransform.pivot.x == 1) ? pos.x * 2 + 1 : pos.x * 2 - 1;
			float y = (img.rectTransform.pivot.y == 1) ? pos.y * 2 + 1 : pos.y * 2 - 1;

			InputDirection = new Vector3 (x, 0, y);

			InputDirection = (InputDirection.magnitude > 1) ? InputDirection.normalized : InputDirection;
			Joysticimg.rectTransform.anchoredPosition= new Vector3(InputDirection.x * (img.rectTransform.sizeDelta.x/3),InputDirection.z*(img.rectTransform.sizeDelta.y/3) );
		}

	}
	public virtual void OnPointerDown (PointerEventData ped){
		
	
		OnDrag(ped);
	}
	public virtual void OnPointerUp(PointerEventData ped){
		InputDirection = Vector3.zero;
		Joysticimg.rectTransform.anchoredPosition = Vector3.zero;
	}
}
