using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoginButton : MonoBehaviour {


	public void ButtonPress()
	{
		string Email = GameObject.Find ("Login_Email").GetComponent<InputField> ().text;
		string Password = GameObject.Find ("Login_Password").GetComponent<InputField> ().text;

		sendData dm = GameObject.Find ("Controll").GetComponent<sendData> ();


		StartCoroutine (dm.Login(Email,Password));
		
	}



}
