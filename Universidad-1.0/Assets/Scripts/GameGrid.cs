using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameGrid : MonoBehaviour
{

	// Variables
	public int xSize, ySize;
	public float fruitWidth = 1f;
	private GameObject[] fruits;
	private GridItem[,] items;
	private GridItem currentlySelectedItem;
	public static int minItemsForMatch = 3;
	public float delayBetweenMatches = 0.2f;
	public bool canPlay;

	// Use this for initialization
	void Start ()
	{
		canPlay = true;
		GetImageResources ();	
		FillGrid ();
		ClearGrid ();
		GridItem.OnMouseOverItemEventHandler += OnMouseOverItem;
	}

	void OnDisable ()
	{
		GridItem.OnMouseOverItemEventHandler -= OnMouseOverItem;
	}

	/*Cargar las imagenes*/
	void GetImageResources ()
	{
		//fruits = Resources.LoadAll<GameObject> ("Prefabs/Other");
		//fruits = Resources.LoadAll<GameObject> ("Prefabs");
		fruits = Resources.LoadAll<GameObject> ("Prefabs/Food");

		/*Asignar un id a cada imagen*/
		for (int i = 0; i < fruits.Length; i++) {
			fruits [i].GetComponent<GridItem> ().id = i;
		}
	}

	/*Llenar la cuddricula inicial sin generar combinaciones*/
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

	//Instanciar un item dada su posición en (x,y)
	GridItem InstantiateFruit (int x, int y)
	{
		bool condition = false; //Saber si el item a crear genera una combinación
		int randomId = 0; 
		do {

			//Seleccionar un tipo aleatorio de alimento
			randomId = Random.Range (0, fruits.Length);
			/*
			// Validar que el proximo objeto a crear no genere una combinación
			if (x >= 2 && y >= 2) {
				condition = (items [x - 1, y].id == randomId && items [x - 2, y].id == randomId) || 
					(items [x, y - 1].id == randomId && items [x, y - 2].id == randomId);
			} else if (y >= 2) {
				condition = (items [x, y - 1].id == randomId && items [x, y - 2].id == randomId);
			} else if (x >= 2) {
				condition = (items [x - 1, y].id == randomId && items [x - 2, y].id == randomId);
			}*/

		} while(condition);

		// Crear el GridItem
		GameObject randomFruit = fruits [randomId];	
		GridItem newFruit = ((GameObject)Instantiate (randomFruit, new Vector3 (x + fruitWidth, y), Quaternion.identity)).GetComponent<GridItem> ();
		newFruit.OnItemPositionChanged (x, y);
		return newFruit;
	}

	/*Resolver las combinaciones que se generan a partir de un movimiento*/
	void ClearGrid()
	{
		for (int x = 0; x < xSize; x++) {
			for (int y = 0; y < ySize; y++) {
				/*Buscar combinación en cada item*/
				MatchInfo matchInfo = GetMatchInformation (items [x, y]);
				if (matchInfo.validMatch) {
					Destroy (items[x,y].gameObject);
					items[x,y]=InstantiateFruit (x, y);
					y--; //Se debe analizar tambien el item que se acaba de crear
				}
			}
		}
	}

	/*Obtener informacion de posibles combinaciones alrededor de un item*/
	MatchInfo GetMatchInformation (GridItem item)
	{
		MatchInfo info = new MatchInfo ();
		info.match = null;

		/*Buscar combinaciones horizontal y verticalmente*/
		List<GridItem> hMatch = SearchHorizontally (item);
		List<GridItem> vMatch = SearchVertically (item);

		/*Chequear si hay combinaciones y escoger la mas larga*/
		if (hMatch.Count >= minItemsForMatch && hMatch.Count > vMatch.Count) {
			/*Definir información para combinación horizontal*/
			info.matchStartingX = GetMinimumX (hMatch);
			info.matchEndingX = GetMaximumX (hMatch);
			info.matchStartingY = info.matchEndingY = hMatch [0].y;
			info.match = hMatch;
		} else if (vMatch.Count >= minItemsForMatch) {
			/*Definir informacion para combinación vertical*/
			info.matchStartingY = GetMinimumY (vMatch);
			info.matchEndingY = GetMaximumY (vMatch);
			info.matchStartingX = info.matchEndingX = vMatch [0].x;
			info.match = vMatch;
		}
		return info;
	}

	/*Buscarle combinaciones horizontales a un item*/
	List<GridItem> SearchHorizontally (GridItem item)
	{
		List<GridItem> hItems = new List<GridItem>{ item };
		int left = item.x - 1;
		int right = item.x + 1;

		/*Buscar por la izquierda*/
		while (left >= 0 && items [left, item.y].id == item.id) {
			hItems.Add (items [left, item.y]);
			left--;
		}

		/*Buscar por la derecha*/
		while (right < xSize && items [right, item.y].id == item.id) {
			hItems.Add (items [right, item.y]);
			right++;
		}
		return hItems;
	}

	/*Buscarle combinaciones verticales a un item*/
	List<GridItem> SearchVertically (GridItem item)
	{
		List<GridItem> vItems = new List<GridItem>{ item };
		int up = item.y + 1;
		int down = item.y - 1;

		/*Buscar por abajo*/
		while (down >= 0 && items [item.x, down].id == item.id) {
			vItems.Add (items [item.x, down]);
			down--;
		}

		/*Buscar por arriba*/
		while (up < ySize && items [item.x, up].id == item.id) {
			vItems.Add (items [item.x, up]);
			up++;
		}
		return vItems;
	}

	/*Retorna el indice menor en X de la lista*/
	int GetMinimumX (List<GridItem> items)
	{		
		float[] indices = new float[items.Count];
		for (int i = 0; i < indices.Length; i++) {
			indices [i] = items [i].x;
		}
		return (int)Mathf.Min (indices);
	}

	/*Retorna el indice mayor en X de la lista*/
	int GetMaximumX (List<GridItem> items)
	{
		float[] indices = new float[items.Count];
		for (int i = 0; i < indices.Length; i++) {
			indices [i] = items [i].x;
		}
		return (int)Mathf.Max (indices);
	}

	/*Retorna el indice menor de Y de la lista*/
	int GetMinimumY (List<GridItem> items)
	{
		float[] indices = new float[items.Count];
		for (int i = 0; i < indices.Length; i++) {
			indices [i] = items [i].y;
		}
		return (int)Mathf.Min (indices);
	}

	/*Retorna el indice mayor de Y de la lista*/
	int GetMaximumY (List<GridItem> items)
	{
		float[] indices = new float[items.Count];
		for (int i = 0; i < indices.Length; i++) {
			indices [i] = items [i].y;
		}
		return (int)Mathf.Max (indices);
	}

	/*Capturar evento click(item) sobre algun elemento de la cuadricula*/
	void OnMouseOverItem (GridItem item)
	{
		print (canPlay);
		/*Si el segundo item seleccionado es igual al primero y si puede jugar*/
		if (currentlySelectedItem == item || !canPlay) {
			return;
		}

		/*Si es el primer item a seleccionar*/
		if (currentlySelectedItem == null) {
			currentlySelectedItem = item;
		} else {
			/*Si es el segundo item a seleccionar*/
			float xDiff = Mathf.Abs (item.x - currentlySelectedItem.x);	//Diferencia entre los dos items en el eje X
			float yDiff = Mathf.Abs (item.y - currentlySelectedItem.y); //Diferencia entre los dos items en el eje Y

			/*Permitir swap si es valido*/
			if (xDiff + yDiff == 1) {				
				StartCoroutine (TryMatch (currentlySelectedItem, item));
			} else {
				/*Negar swap*/
				Debug.LogError ("Esos items a mas de 1 unidad de distancia uno del otro");
			}
			currentlySelectedItem = null; 
		}
		print (canPlay);
	}

	/*Intentar una jugada*/
	IEnumerator TryMatch (GridItem a, GridItem b)
	{
		//canPlay = false;
		yield return StartCoroutine (Swap (a, b));  //Hacer el swap

		/*Buscar combinaciones con ambos items*/
		MatchInfo matchA = GetMatchInformation (a); 
		MatchInfo matchB = GetMatchInformation (b); 

		/*El swap no genera ninguna combinación*/
		if (!matchA.validMatch && !matchB.validMatch) {
			yield return StartCoroutine (Swap (a, b));
			yield break;
		}

		/*El swap genera una combinación con alguno de los dos items*/
		if (matchA.validMatch) {
			yield return StartCoroutine (DestroyItems (matchA.match));
			yield return new WaitForSeconds (delayBetweenMatches);
			yield return StartCoroutine(UpdateGridAfterMatch(matchA));	
		}else if (matchB.validMatch) {
			yield return StartCoroutine (DestroyItems (matchB.match));
			yield return new WaitForSeconds (delayBetweenMatches);
			yield return StartCoroutine(UpdateGridAfterMatch(matchB));
		}	
		//canPlay = true;
	}

	/*Destruir los items de una combinación*/
	IEnumerator DestroyItems (List<GridItem> items)
	{
		foreach (GridItem i in items) {
			yield return StartCoroutine (i.transform.Scale (Vector3.zero, 0.05f)); //Reducir tamaño (efecto visual)
			Destroy (i.gameObject); //Destruir
		}
	}

	/*Generar nuevos items cada que se realiza una combinación*/
	IEnumerator UpdateGridAfterMatch(MatchInfo match)
	{
		if (match.matchStartingY == match.matchEndingY) {
			//Combinación horizontal
			for (int x = match.matchStartingX; x <= match.matchEndingX; x++) {
				for(int y = match.matchStartingY; y < ySize - 1; y++){
					/*Actualizar los indices en la matriz de items, de los items por arriba de la combinación*/
					GridItem upperIndex = items [x, y + 1];
					GridItem current = items [x, y];
					items [x, y] = upperIndex;
					items [x, y + 1] = current;
					items [x, y].OnItemPositionChanged (items[x,y].x, items[x,y].y-1); //Settearle la nueva posición
				}
				items[x,ySize -1]=InstantiateFruit (x, ySize - 1); //Crear el nuevo item en la ultima fila
			}
		}
		else if(match.matchEndingX==match.matchStartingX){
			//Combinación vertical
			int matchHeight = 1+(match.matchEndingY - match.matchStartingY); //tamaño de la combinación
			for(int y=match.matchStartingY + matchHeight; y <= ySize -1;y++){
				/*Actualizar los indices en la matriz de items, de los items por arriba de la combinación*/
				GridItem lowerIndex = items [match.matchStartingX, y - matchHeight];
				GridItem current = items [match.matchStartingX, y];
				items [match.matchStartingX, y - matchHeight] = current;
				items [match.matchStartingX, y] = lowerIndex;
				//items [match.matchStartingX, y - matchHeight].OnItemPositionChanged (items [match.matchStartingX, y - matchHeight].x, items [match.matchStartingX, y - matchHeight].y-matchHeight);
			}
			/* ????????????????*/
			for (int y = 0; y < ySize - matchHeight; y++) {
				items [match.matchStartingX, y].OnItemPositionChanged (match.matchStartingX, y);
			}

			/*Crear los items para rellenar la cuadricula*/
			for (int i = 0; i < matchHeight; i++) {
				items [match.matchStartingX, (ySize - 1) - i] = InstantiateFruit (match.matchStartingX, (ySize - 1) - i);
			}
		}

		/*Cada que se rellene la cuadricula con nuevos items, se debe verificar si se formaron mas combinaciones
		  ¿Se puede mejorar?*/
		for (int x = 0; x < xSize; x++) {
			for (int y = 0; y < ySize; y++) {
				/*Buscar combinación en cada item*/
				MatchInfo matchInfo = GetMatchInformation (items [x, y]);
				if (matchInfo.validMatch) {
					//yield return new WaitForSeconds (delayBetweenMatches);
					yield return StartCoroutine(DestroyItems(matchInfo.match));
					yield return new WaitForSeconds (delayBetweenMatches);
					yield return StartCoroutine (UpdateGridAfterMatch (matchInfo));
				}
			}
		}
	}




	IEnumerator Swap (GridItem a, GridItem b)
	{
		ChangeRigidBodyStatus (false); //Desactivar todos los cuerpos rigidos

		/*Swap entre los dos items*/
		float movDuration = 0.1f; //Duración del efecto de movimiento
		Vector3 aPosition = a.transform.position;
		//Vector3 bPosition = b.transform.position;
		StartCoroutine (a.transform.Move (b.transform.position, movDuration));
		StartCoroutine (b.transform.Move (aPosition, movDuration));
		yield return new WaitForSeconds (movDuration); //Esperar que se realice el movimiento

		SwapIndices (a, b); //Hacer el swap entre los indices de la grid 
		ChangeRigidBodyStatus (true); //Activar de nuevo todos los cuerpos rigidos
	}

	/*Hacer el intercambio de los indices de los items en la matriz*/
	void SwapIndices (GridItem a, GridItem b)
	{
		/*Intercambiar los items en la matriz*/
		GridItem tempA = items [a.x, a.y];
		items [a.x, a.y] = b;
		items [b.x, b.y] = tempA;

		/*Setterale a cada item las nuevas posiciones*/
		int bOldX = b.x;
		int bOldY = b.y;
		b.OnItemPositionChanged (a.x, a.y);
		a.OnItemPositionChanged (bOldX, bOldY);
	}











	void ChangeRigidBodyStatus (bool status)
	{
		foreach (GridItem g in items) {			
			g.GetComponent<Rigidbody2D> ().isKinematic = !status;		
		}
	}

	void printItems ()
	{
		for (int x = 0; x < xSize; x++) {
			for (int y = 0; y < ySize; y++) {	
				
				if (items [x, y] == null) {
					Debug.Log (string.Format("{0} {1}",x,y));
				}
			}
		}
	}
}
