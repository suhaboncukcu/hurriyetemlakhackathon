using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnitySlippyMap.Map;
using UnityEngine.UI;
using System.Text;

public class sendData : MonoBehaviour {

	public GameObject mapObj;
	public MapBehaviour mapCmp;
	public static string UserID;
	public static string AdressUnique;
	public static string PhotographerUnique;
	public GameObject DetailsMenu;


	public IEnumerator showDetails(string Unique)
	{
		AdressUnique = Unique;

		Debug.Log("http://pieorpi.com/api/getAdress/" + Unique);

		WWWForm form = new WWWForm();

		form.headers.Add ("mobilekey", "hurriyetemlakhackathon");
		
		form.AddField("apikey","hurriyetemlakhackathon");



		WWW www = new WWW("http://pieorpi.com/api/getAddress/" + Unique,form);

		yield return www;

		//string jsonData = System.Text.Encoding.UTF8.GetString(www.bytes);//www.bytes, 3, www.bytes.Length - 3
		//string jsonData = Encoding.UTF8.GetString (www.bytes);

		//string jsonData = DecodeFromUtf8(www.text);
		//Debug.Log (jsonData);
		//Debug.Log (www.text);
		JSONObject js = new JSONObject(www.text);

		Adress a = new Adress();
		//Debug.Log(adr.keys[0]);
		//Debug.Log(adr.list[0].str);
		a.adress = js.list[0].str;
		//Debug.Log(a.adress);
		a.declinedPhotoCount = js.list[1].str;
		//Debug.Log(a.declinedPhotoCount);
		a.latitude = Convert.ToDouble(js.list[3].str);
		//Debug.Log(a.latitude);
		a.longitude = Convert.ToDouble(js.list[2].str);
		//Debug.Log(a.longitude);
		a.needPhoto = js.list[4].str;
		//Debug.Log(a.needPhoto);
		a.ownerPhone = js.list[5].str;
		//Debug.Log(a.ownerPhone);
		a.price = js.list[6].i.ToString();
		//Debug.Log(a.price);
		a.title = js.list[7].str;
		//Debug.Log(a.title);
		a.unique = js.list[8].str;
		//Debug.Log(a.unique);
			
		Camera.main.cullingMask = 2;
		DetailsMenu.SetActive (true);
		GameObject.Find ("DetailsMenu_Title").GetComponent<Text> ().text = a.title;
		GameObject.Find ("DetailsMenu_Price").GetComponent<Text> ().text = a.price +" TL kazanabilirsiniz";
		GameObject.Find ("DetailsMenu_Adress").GetComponent<Text> ().text = a.adress;
		GameObject.Find ("DetailsMenu_OwnerPhone").GetComponent<Text> ().text = a.ownerPhone;
		//GameObject.Find ("DetailsMenu_Experience").GetComponent<Text> ().text = (a.declinedPhotoCount * 5).ToString();

		PhoneNumber = a.ownerPhone;

	}

	public string DecodeFromUtf8(string utf8String)
	{
		// copy the string as UTF-8 bytes.
		byte[] utf8Bytes = new byte[utf8String.Length];
		for (int i=0;i<utf8String.Length;++i) {
			//Debug.Assert( 0 <= utf8String[i] && utf8String[i] <= 255, "the char must be in byte's range");
			utf8Bytes[i] = (byte)utf8String[i];
		}
		
		return Encoding.UTF8.GetString(utf8Bytes,0,utf8Bytes.Length);
	}

	public void initializeMap(){
		//Debug.Log("asdad");
		mapObj = GameObject.Find("[Map]");
		mapCmp = mapObj.GetComponent<MapBehaviour> ();

	}

	public bool lastframeismoved;

	// Update is called once per frame
	void Update () {
		if (send) {
			send = false;
			StartCoroutine(Send());
		}

		if (search) {
			search = false;
			StartCoroutine(Search());
		}


		//if dropped;
		if (lastframeismoved && !mapCmp.hasMoved) {
			StartCoroutine(Search());

		}
		lastframeismoved = mapCmp.hasMoved; 



	}


	public bool send = false;

	public IEnumerator Send()
	{

	
		WWWForm form = new WWWForm();
		form.AddField("apikey","hurriyetemlakhackathon");

		WWW www = new WWW("http://pieorpi.com/api", form);

		yield return www;
		//.. process results from WWW request here...

		Debug.Log (www.text);


		foreach (var h in www.responseHeaders) {
			Debug.Log(h.Key + " , " + h.Value);
		}



		this.gameObject.name = www.text.ToLower ();

	}


	public bool search = false;

	public IEnumerator Search()
	{



		var j = new JSONObject ();
		//j.AddField ("lat","41.032");
		//j.AddField ("lng","28.983");
		j.AddField ("lat",mapCmp.CenterWGS84[1].ToString());
		j.AddField ("lng",mapCmp.CenterWGS84[0].ToString());

		j.AddField ("parameter",2f);
		//Debug.Log (j.Print());
		
		WWWForm form = new WWWForm();
		form.headers.Add ("mobilekey", "hurriyetemlakhackathon");
		
		form.AddField("apikey","hurriyetemlakhackathon");
		
		form.AddField("search", j.Print());


		WWW www = new WWW("http://pieorpi.com/api/search", form);
		
		yield return www;
		//.. process results from WWW request here...
		
		
		//Debug.Log (www.text);

		JSONObject js = new JSONObject(www.text);
		//accessData(js);
		//access data (and print it)
		//Debug.Log(js.list.Count);

		//var Adresses = new List<Adress>();
		GameObject gm;
		gm = GameObject.Find ("Master");
		var map = gm.GetComponent<mapController> ();
		//map.AddMarker (Adresses [0].latitude, Adresses [0].longitude);


		//for every adress
		foreach (var adr in js.list) {
			Adress a = new Adress();
			//Debug.Log(adr.keys[0]);
			//Debug.Log(adr.list[0].str);
			a.adress = adr.list[0].str;
			//Debug.Log(a.adress);
			a.declinedPhotoCount = adr.list[1].str;
			//Debug.Log(a.declinedPhotoCount);
			a.latitude = Convert.ToDouble(adr.list[3].str);
			//Debug.Log(a.latitude);
			a.longitude = Convert.ToDouble(adr.list[2].str);
			//Debug.Log(a.longitude);
			a.needPhoto = adr.list[4].str;
			//Debug.Log(a.needPhoto);
			a.ownerPhone = adr.list[5].str;
			//Debug.Log(a.ownerPhone);
			a.price = adr.list[6].i.ToString();
			//Debug.Log(a.price);
			a.title = adr.list[7].str;
			//Debug.Log(a.title);
			a.unique = adr.list[8].str;
			//Debug.Log(a.unique);

			if (GameObject.Find(a.unique.ToString()) == null){
				map.AddMarker (a.unique.ToString() ,a.latitude, a.longitude,a.price);
			}

		}

	
	}

	public GameObject SignUpForm;
	public GameObject LoginSuccessForm;
	public GameObject LoginErrorForm;
	public GameObject SignupSuccessForm;
	public GameObject SignupErrorForm;

	public IEnumerator SignUp(string Email, string Password,string IdentityNumber,string IBAN,string PhoneNumber)
	{
		
		Debug.Log("I try to sign up");

		var j = new JSONObject ();
		j.AddField ("email",Email);
		j.AddField ("password",Password);
		j.AddField ("IdentityNumber",IdentityNumber);
		j.AddField ("IBAN",IBAN);
		j.AddField ("PhoneNumber",PhoneNumber);

		//Debug.Log (j.Print());
		
		WWWForm form = new WWWForm();
		form.headers.Add ("mobilekey", "hurriyetemlakhackathon");
		
		form.AddField("apikey","hurriyetemlakhackathon");
		form.AddField ("photographer", j.Print ());
//		form.AddField ("email",Email);
//		form.AddField ("password",Password);
//		form.AddField ("IdentityNumber",IdentityNumber);
//		form.AddField ("IBAN",IBAN);
//		form.AddField ("PhoneNumber",PhoneNumber);
		
		WWW www = new WWW("http://pieorpi.com/api/newPhotographer", form);
		
		yield return www;
		//.. process results from WWW request here...

		JSONObject js = new JSONObject(www.text);

		Debug.Log (js.list [0].str);

		if (js.list [0].type == JSONObject.Type.STRING) {
			if (js.list [0].str != "007") {
				Debug.Log("success");
				PhotographerUnique = js.list [0].str;
				SignUpForm.SetActive(false);
				SignupSuccessForm.SetActive(true);
			}else{
				PhotographerUnique = null;
				SignUpForm.SetActive(false);
				SignupErrorForm.SetActive(true);
				Debug.Log("login error");
			}

		} else {
			PhotographerUnique = null;
			SignUpForm.SetActive(false);
			SignupErrorForm.SetActive(true);
			Debug.Log("login error");
		}

		
	}

	public GameObject LoginForm;


	public IEnumerator Login(string Email, string Password)
	{
		var j = new JSONObject ();
		j.AddField ("email",Email);
		j.AddField ("password",Password);

		WWWForm form = new WWWForm();
		form.headers.Add ("mobilekey", "hurriyetemlakhackathon");
		
		form.AddField("apikey","hurriyetemlakhackathon");
		form.AddField ("photographer", j.Print ());

		WWW www = new WWW("http://pieorpi.com/api/newPhotographer", form);
		
		yield return www;
		//.. process results from WWW request here...

		JSONObject js = new JSONObject(www.text);

		if (js.list [0].type == JSONObject.Type.STRING) {
			if (js.list [0].str != "007") {
				Debug.Log("Login Success");
				PhotographerUnique = js.list [0].str;
				LoginForm.SetActive(false);
				LoginSuccessForm.SetActive(true);
				Debug.Log(PhotographerUnique);
			}else
			{
				PhotographerUnique = null;
				LoginForm.SetActive(false);
				LoginErrorForm.SetActive(true);
				Debug.Log("login error");
			}


		} else {
			PhotographerUnique = null;
			LoginForm.SetActive(false);
			LoginErrorForm.SetActive(true);
			Debug.Log("login error");
		}


	}
	public GameObject SignoutSuccessForm;

	public void SignOut()
	{
		PhotographerUnique = null;
		SignoutSuccessForm.SetActive (true);
		Debug.Log ("signout success");
	}


	public GameObject booknowPopup;
	public GameObject loginMenu;
	public string PhoneNumber;

	public IEnumerator BookNow()
	{


		if (PhotographerUnique == null) {
			Debug.Log("you didnt log in please log in!");

			DetailsMenu.SetActive (false);
			loginMenu.SetActive (true);

			return true;
		}

		Debug.Log("Trying to book");

//		var j = new JSONObject ();
//		j.AddField ("email",Email);
//		j.AddField ("password",Password);
		
		WWWForm form = new WWWForm();
		form.headers.Add ("mobilekey", "hurriyetemlakhackathon");
		
		form.AddField("apikey","hurriyetemlakhackathon");
//		form.AddField ("photographer", j.Print ());
		
		WWW www = new WWW("http://pieorpi.com/api/pickAddressForPhotographer/"+PhotographerUnique+"/"+AdressUnique+"/hurriyetemlakhackathon", form);

		yield return www;
		//.. process results from WWW request here...


		//successfully booked
		DetailsMenu.SetActive (false);
		booknowPopup.SetActive (true);
		//GameObject.Find ("DetailsMenu_Adress").GetComponent<Text> ().text = a.adress;
		GameObject.Find("BookNowPopupPhone").GetComponent<Text> ().text = PhoneNumber;
		GameObject.Find (AdressUnique).transform.GetChild (0).gameObject.SetActive (false);


		//JSONObject js = new JSONObject(www.text);

		//PhotographerUnique = js.list [0].str;

		//Debug.Log (js.list[0].str);
		//Debug.Log (www.text);


	}



}
