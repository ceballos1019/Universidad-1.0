using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameGrid : MonoBehaviour {

	// Variables
	public int xSize, ySize;
	public float fruitWidth = 1f;
	private GameObject[] fruits;
	private GridItem[,] items;
	private GridItem currentlySelectedItem;

	// Use this for initialization
	void Start ()
	{
		GetFruits ();	
		FillGrid ();
		GridItem.OnMouseOverItemEventHandler += OnMouseOverItem;
		List<GridItem> matchesForItem = SearchHorizontally (items [3, 3]);
		if (matchesForItem.Count >= 3) {
			Debug.Log ("Hay un match en el elemento 3,3");
		} else {
			Debug.Log ("No hay match");
		}
		List<GridItem> matchesForItemV = SearchVertically (items [3, 3]);
		if (matchesForItemV.Count >= 3) {
			Debug.Log ("Hay un match V en el elemento 3,3");
		} else {
			Debug.Log ("No hay match V");
		}
		//Destroy (items [5, 1].gameObject);
	}

	void OnDisable()
	{
		GridItem.OnMouseOverItemEventHandler -= OnMouseOverItem;
	}


	// Llenar la cuddricula inicial sin generar combinaciones
	void FillGrid ()
	{	
		items = new GridItem[xSize, ySize];

		for (int x = 0; x < xSize; x++) {
			for (int y = 0; y < ySize; y++) {
				// Instanciar el item en la posicion (x,y)
				items [x, y] = InstantiateFruit (x, y);
			}
		}


	}


	GridItem InstantiateFruit (int x, int y)
	{
		bool condition = false;
		int randomId = 0;
		do {

			//Seleccionar un tipo aleatorio de alimento
			randomId = Random.Range (0, fruits.Length);

			// Validar que el proximo objeto a crear no genere una combinación
			if (x >= 2 && y >= 2) {
				condition = (items [x - 1, y].id == randomId && items [x - 2, y].id == randomId) ||
					(items [x, y - 1].id == randomId && items [x, y - 2].id == randomId);
			}else if(y >= 2){
				condition = (items [x, y - 1].id == randomId && items [x, y - 2].id == randomId);
			}else if(x >=2){
				condition = (items [x - 1, y].id == randomId && items [x - 2, y].id == randomId);
			}

		} while(condition);

		// Crear el GridItem
		GameObject randomFruit = fruits [randomId];	
		GridItem newFruit = ((GameObject)Instantiate (randomFruit, new Vector3 (x+fruitWidth, y), Quaternion.identity)).GetComponent<GridItem> ();
		newFruit.OnItemPositionChanged (x, y);
		return newFruit;
	}

	void OnMouseOverItem(GridItem item)
	{
		if (currentlySelectedItem == item) {
			return;
		}
		if (currentlySelectedItem == null) {
			currentlySelectedItem = item;
		}
		else
		{
			float xDiff = Mathf.Abs (item.x - currentlySelectedItem.x);	
			float yDiff = Mathf.Abs (item.y - currentlySelectedItem.y);

			if (xDiff + yDiff == 1) {
				// Permitir swap
				StartCoroutine(Swap(currentlySelectedItem,item));
			} 
			else
			{
				// Negar swap	
				Debug.LogError("Esos items a mas de 1 unidad de distancia uno del otro");
			}
			currentlySelectedItem = null;
		}
	}

	IEnumerator Swap(GridItem a, GridItem b)
	{
		ChangeRigidBodyStatus (false); //Desactivar todos los cuerpos rigidos
		//Swap entre items
		float movDuration = 0.1f;
		Vector3 aPosition = a.transform.position;
		StartCoroutine(a.transform.Move(b.transform.position,movDuration));
		StartCoroutine(b.transform.Move(aPosition,movDuration));
		yield return new WaitForSeconds (movDuration);
		SwapIndices (a, b); 
		ChangeRigidBodyStatus(true); //Activarla de nuevo
	}

	void SwapIndices(GridItem a, GridItem b)
	{
		GridItem tempA = items [a.x, a.y];
		items [a.x, a.y] = b;
		items [b.x, b.y] = tempA;
		int bOldX = b.x;
		int bOldY = b.y;
		b.OnItemPositionChanged (a.x, a.y);
		a.OnItemPositionChanged (bOldX, bOldY);
	}

	List<GridItem> SearchHorizontally(GridItem item)
	{
		List<GridItem> hItems = new List<GridItem>{ item };
		int left = item.x - 1;
		int right = item.x + 1;
		while (left >= 0 && items [left, item.y].id == item.id) {
			hItems.Add(items[left,item.y]);
			left--;
		}
		while (right < xSize && items [right, item.y].id == item.id) {
			hItems.Add(items[right,item.y]);
			right++;
		}
		return hItems;
	}

	List<GridItem> SearchVertically(GridItem item)
	{
		List<GridItem> vItems = new List<GridItem>{ item };
		int up = item.y + 1;
		int down = item.y - 1;
		while (down >= 0 && items [item.x, down].id == item.id) {
			vItems.Add(items[item.x,down]);
			down--;
		}
		while (up < ySize && items [item.x, up].id == item.id) {
			vItems.Add(items[item.x,up]);
			up++;
		}
		return vItems;
	}

	void GetFruits ()
	{

		fruits = Resources.LoadAll<GameObject> ("Prefabs");
		for (int i = 0; i < fruits.Length; i++) {
			fruits [i].GetComponent<GridItem> ().id = i;
		}
	}


	void ChangeRigidBodyStatus(bool status){
		foreach (GridItem g in items) {
			g.GetComponent<Rigidbody2D>().isKinematic = !status;
		}
	}

	// Update is called once per frame
	void Update ()
	{

	}
}
