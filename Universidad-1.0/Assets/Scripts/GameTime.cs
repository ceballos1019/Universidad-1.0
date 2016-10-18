using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameTime : MonoBehaviour {

	[HideInInspector]
	public Text textTime; //texto que aparece en la pantalla.
	public int time;  //tiempo en segundos

	//Inicialización de variables
	void Start () {
		textTime = GetComponent<Text> ();
		time = 180;
		textTime.text = formatTime (time);
		startTime ();
	}

	void startTime(){
		InvokeRepeating ("decreaseTime", 1, 1);
	}

	/*Reducir el tiempo cada segundo*/
	void decreaseTime(){		
		textTime.text = formatTime (--time);
	}

	/*Toma el tiempo actual en segundos y lo formatea para ser mostrado en la pantalla como min:sec*/
	string formatTime(int currenTime){
		int minutes, seconds;

		/*Conversion a minutos y segundos*/
		minutes = currenTime / 60;
		seconds = currenTime % 60;

		/*Crear un string con el tiempo a mostrar*/
		string formattedText = string.Format("{0}:{1}",minutes.ToString ("D2"),seconds.ToString ("D2"));
		return formattedText;
	}

	/*Pausar el tiempo*/
	public void pauseTime(){
		CancelInvoke ();
	}

	/*Reanudar el tiempo*/
	public void restartTime(){
		InvokeRepeating ("decreaseTime", 0, 1);
	}
}
