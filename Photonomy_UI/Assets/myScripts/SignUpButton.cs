using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class SignUpButton : MonoBehaviour {


	public void ButtonPress()
	{
		string Email = GameObject.Find ("SignUp_Email").GetComponent<InputField> ().text;
		string Password = GameObject.Find ("SignUp_Password").GetComponent<InputField> ().text;
		string IdentityNumber = GameObject.Find ("SignUp_IdentityNumber").GetComponent<InputField> ().text;
		string IBAN = GameObject.Find ("SignUp_IBAN").GetComponent<InputField> ().text;
		string PhoneNumber = GameObject.Find ("SignUp_PhoneNumber").GetComponent<InputField> ().text;

		sendData dm = GameObject.Find ("Controll").GetComponent<sendData> ();

		StartCoroutine (dm.SignUp(Email,Password,IdentityNumber,IBAN,PhoneNumber));

	}

}
