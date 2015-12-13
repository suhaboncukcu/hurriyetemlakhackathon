using UnityEngine;
using System.Collections;

public class BookNowButton : MonoBehaviour {


	public void ButtonPress()
	{
		sendData dm = GameObject.Find ("Controll").GetComponent<sendData>();
		StartCoroutine (dm.BookNow());
	}


}
