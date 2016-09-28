using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*Clase para almacenar la información de una combinación de un item*/
public class MatchInfo
{
	public List<GridItem> match;
	public int matchStartingX;
	public int matchEndingX;
	public int matchStartingY;
	public int matchEndingY;

	public bool validMatch {
		get { return match != null; }
	}

}
