using UnityEngine;
using System.Collections;

public class GridItem : MonoBehaviour {

    public int x {
		get;
		private set;
	}

	public int y {
		get;
		private set;
	}

	[HideInInspector]
	public int id;

	public void OnItemPositionChanged(int newX, int newY)
	{
		x = newX;
		y = newY;
		gameObject.name = string.Format("Sprite [{0}] [{1}]",x,y);
	}

    //Cuando se presiona la pantalla y se suelta
	void OnMouseDown()
	{
        if (OnMouseOverItemEventHandler != null) 
		{
			OnMouseOverItemEventHandler (this);
        }
	}

    //Cuando se arrastra el mouse dando clic
    void OnMouseDrag()
    {
        
        if (OnMouseDragOverItemEventHandler != null)
        {
            OnMouseDragOverItemEventHandler(this);
        }
    }


    public delegate void OnMouseOverItem(GridItem item);
	public static event OnMouseOverItem OnMouseOverItemEventHandler;

    public delegate void OnMouseDragOverItem(GridItem item);
    public static event OnMouseDragOverItem OnMouseDragOverItemEventHandler;
}
