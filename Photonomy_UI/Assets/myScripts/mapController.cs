// 
//  TestMap.cs
//  
//  Author:
//       Jonathan Derrough <jonathan.derrough@gmail.com>
//  
//  Copyright (c) 2012 Jonathan Derrough
// 
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
// 
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using UnityEngine;
using System;

using UnitySlippyMap.Map;
using UnitySlippyMap.Markers;
using UnitySlippyMap.Layers;
using ProjNet.CoordinateSystems;
using ProjNet.CoordinateSystems.Transformations;
using ProjNet.Converters.WellKnownText;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class mapController : MonoBehaviour
{
	public MapBehaviour		map;
	
	public Texture	LocationTexture;
	public Texture	MarkerTexture;
	
	public float	guiXScale;
	public float	guiYScale;
	public Rect	guiRect;
	
	public bool 	isPerspectiveView = false;
	public float	perspectiveAngle = 30.0f;
	public float	destinationAngle = 0.0f;
	public float	currentAngle = 0.0f;
	public float	animationDuration = 0.5f;
	public float	animationStartTime = 0.0f;
	
	public List<LayerBehaviour> layers;
	public int     currentLayerIndex = 0;
		

//	bool Toolbar(MapBehaviour map)
//	{
//		GUI.matrix = Matrix4x4.Scale(new Vector3(guiXScale, guiXScale, 1.0f));
//		
//		GUILayout.BeginArea(guiRect);
//		
//		GUILayout.BeginHorizontal();
//		
//		//GUILayout.Label("Zoom: " + map.CurrentZoom);
//		
//		bool pressed = false;
//		if (GUILayout.RepeatButton("+", GUILayout.ExpandHeight(true)))
//		{
//			map.Zoom(1.0f);
//			pressed = true;
//		}
//		if (Event.current.type == EventType.Repaint)
//		{
//			Rect rect = GUILayoutUtility.GetLastRect();
//			if (rect.Contains(Event.current.mousePosition))
//				pressed = true;
//		}
//		
//		if (GUILayout.Button("2D/3D", GUILayout.ExpandHeight(true)))
//		{
//			if (isPerspectiveView)
//			{
//				destinationAngle = -perspectiveAngle;
//			}
//			else
//			{
//				destinationAngle = perspectiveAngle;
//			}
//			
//			animationStartTime = Time.time;
//			
//			isPerspectiveView = !isPerspectiveView;
//		}
//		if (Event.current.type == EventType.Repaint)
//		{
//			Rect rect = GUILayoutUtility.GetLastRect();
//			if (rect.Contains(Event.current.mousePosition))
//				pressed = true;
//		}
//		
//		if (GUILayout.Button("Center", GUILayout.ExpandHeight(true)))
//		{
//			map.CenterOnLocation();
//		}
//		if (Event.current.type == EventType.Repaint)
//		{
//			Rect rect = GUILayoutUtility.GetLastRect();
//			if (rect.Contains(Event.current.mousePosition))
//				pressed = true;
//		}
//		
//		string layerMessage = String.Empty;
//		if (map.CurrentZoom > layers[currentLayerIndex].MaxZoom)
//			layerMessage = "\nZoom out!";
//		else if (map.CurrentZoom < layers[currentLayerIndex].MinZoom)
//			layerMessage = "\nZoom in!";
//		if (GUILayout.Button(((layers != null && currentLayerIndex < layers.Count) ? layers[currentLayerIndex].name + layerMessage : "Layer"), GUILayout.ExpandHeight(true)))
//		{
//			#if UNITY_3_0 || UNITY_3_1 || UNITY_3_2 || UNITY_3_3 || UNITY_3_4 || UNITY_3_5 || UNITY_3_6 || UNITY_3_7 || UNITY_3_8 || UNITY_3_9
//			layers[currentLayerIndex].gameObject.SetActiveRecursively(false);
//			#else
//			layers[currentLayerIndex].gameObject.SetActive(false);
//			#endif
//			++currentLayerIndex;
//			if (currentLayerIndex >= layers.Count)
//				currentLayerIndex = 0;
//			#if UNITY_3_0 || UNITY_3_1 || UNITY_3_2 || UNITY_3_3 || UNITY_3_4 || UNITY_3_5 || UNITY_3_6 || UNITY_3_7 || UNITY_3_8 || UNITY_3_9
//			layers[currentLayerIndex].gameObject.SetActiveRecursively(true);
//			#else
//			layers[currentLayerIndex].gameObject.SetActive(true);
//			#endif
//			map.IsDirty = true;
//		}
//		
//		if (GUILayout.RepeatButton("-", GUILayout.ExpandHeight(true)))
//		{
//			map.Zoom(-1.0f);
//			pressed = true;
//		}
//		if (Event.current.type == EventType.Repaint)
//		{
//			Rect rect = GUILayoutUtility.GetLastRect();
//			if (rect.Contains(Event.current.mousePosition))
//				pressed = true;
//		}
//		
//		GUILayout.EndHorizontal();
//		
//		GUILayout.EndArea();
//		
//		return pressed;
//	}

	public bool Loadmap = false;
	public double loadmapLatitude;
	public double loadmapLongitude;

	public IEnumerator loadmap(double Latitude,double Longitude)
	{
		Debug.Log("loading map");

		// setup the gui scale according to the screen resolution
		//guiXScale = (Screen.orientation == ScreenOrientation.Landscape ? Screen.width : Screen.height) / 480.0f;
		//guiYScale = (Screen.orientation == ScreenOrientation.Landscape ? Screen.height : Screen.width) / 640.0f;
		// setup the gui area
		//guiRect = new Rect(16.0f * guiXScale, 4.0f * guiXScale, Screen.width / guiXScale - 32.0f * guiXScale, 32.0f * guiYScale);
		
		// create the map singleton
		map = MapBehaviour.Instance;
		map.CurrentCamera = Camera.main;
		map.InputDelegate += UnitySlippyMap.Input.MapInput.BasicTouchAndKeyboard;
		map.CurrentZoom = 15.0f;
		// 9 rue Gentil, Lyon
		map.CenterWGS84 = new double[2] { Latitude, Longitude}; //HERE! STARTiNG CENTER.
		map.UsesLocation = true;
		map.InputsEnabled = true;
		map.ShowsGUIControls = true;
		
		//map.GUIDelegate += Toolbar;

		GameObject.Find ("Controll").GetComponent<sendData> ().initializeMap ();
		//Debug.Log("asdas");
		
		layers = new List<LayerBehaviour>();
		
		// create an OSM tile layer
		OSMTileLayer osmLayer = map.CreateLayer<OSMTileLayer>("OSM");
		osmLayer.BaseURL = "http://a.tile.openstreetmap.org/";
		
		layers.Add(osmLayer);
		
		// create a WMS tile layer
		WMSTileLayerBehaviour wmsLayer = map.CreateLayer<WMSTileLayerBehaviour>("WMS");
		wmsLayer.BaseURL = "http://129.206.228.72/cached/osm?"; // http://www.osm-wms.de : seems to be of very limited use
		wmsLayer.Layers = "osm_auto:all";

		wmsLayer.gameObject.SetActive(false);

		
		layers.Add(wmsLayer);
		
		// create a VirtualEarth tile layer
		VirtualEarthTileLayerBehaviour virtualEarthLayer = map.CreateLayer<VirtualEarthTileLayerBehaviour>("VirtualEarth");
		// Note: this is the key UnitySlippyMap, DO NOT use it for any other purpose than testing
		virtualEarthLayer.Key = "ArgkafZs0o_PGBuyg468RaapkeIQce996gkyCe8JN30MjY92zC_2hcgBU_rHVUwT";

		virtualEarthLayer.gameObject.SetActive(false);

		
		layers.Add(virtualEarthLayer);
		
		// FIXME: SQLite won't work in webplayer except if I find a full .NET 2.0 implementation (for free)
		// create an MBTiles tile layer
		bool error = false;
		// on iOS, you need to add the db file to the Xcode project using a directory reference
		string mbTilesDir = "MBTiles/";
		string filename = "UnitySlippyMap_World_0_8.mbtiles";
		string filepath = null;
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			filepath = Application.streamingAssetsPath + "/" + mbTilesDir + filename;
		}
		else if (Application.platform == RuntimePlatform.Android)
		{
			// Note: Android is a bit tricky, Unity produces APK files and those are never unzip on the device.
			// Place your MBTiles file in the StreamingAssets folder (http://docs.unity3d.com/Documentation/Manual/StreamingAssets.html).
			// Then you need to access the APK on the device with WWW and copy the file to persitentDataPath
			// to that it can be read by SqliteDatabase as an individual file
			string newfilepath = Application.temporaryCachePath + "/" + filename;
			if (File.Exists(newfilepath) == false)
			{
				Debug.Log("DEBUG: file doesn't exist: " + newfilepath);
				filepath = Application.streamingAssetsPath + "/" + mbTilesDir + filename;
				// TODO: read the file with WWW and write it to persitentDataPath
				WWW loader = new WWW(filepath);
				yield return loader;
				if (loader.error != null)
				{
					Debug.LogError("ERROR: " + loader.error);
					error = true;
				}
				else
				{
					Debug.Log("DEBUG: will write: '" + filepath + "' to: '" + newfilepath + "'");
					File.WriteAllBytes(newfilepath, loader.bytes);
				}
			}
			else
				Debug.Log("DEBUG: exists: " + newfilepath);
			filepath = newfilepath;
		}
		else
		{
			filepath = Application.streamingAssetsPath + "/" + mbTilesDir + filename;
		}
		
		if (error == false)
		{
			Debug.Log("DEBUG: using MBTiles file: " + filepath);
			MBTilesLayerBehaviour mbTilesLayer = map.CreateLayer<MBTilesLayerBehaviour>("MBTiles");
			mbTilesLayer.Filepath = filepath;

			mbTilesLayer.gameObject.SetActive(false);
			
			layers.Add(mbTilesLayer);
		}
		else
			Debug.LogError("ERROR: MBTiles file not found!");


	}
	
	void OnApplicationQuit()
	{
		map = null;
	}
	
	void Update()
	{
		if (Loadmap) 
		{
			Loadmap = false;
			StartCoroutine(loadmap(loadmapLatitude,loadmapLongitude));
		}

		if (getGpsCoordinates) 
		{
			getGpsCoordinates = false;
			StartCoroutine(GetGpsCoordidates());
		}

		if (addMarker) 
		{
			addMarker = false;
			AddMarker("test",addMarkerLatitude,addMarkerLongitude,"Test");
		}

		if (Input.GetAxis("Mouse ScrollWheel") > 0f ) // forward
		{
			map.Zoom(1.0f);
		}
		else if (Input.GetAxis("Mouse ScrollWheel") < 0f ) // backwards
		{
			map.Zoom(-1.0f);
		}


		if (destinationAngle != 0.0f)
		{
			Vector3 cameraLeft = Quaternion.AngleAxis(-90.0f, Camera.main.transform.up) * Camera.main.transform.forward;
			if ((Time.time - animationStartTime) < animationDuration)
			{
				float angle = Mathf.LerpAngle(0.0f, destinationAngle, (Time.time - animationStartTime) / animationDuration);
				Camera.main.transform.RotateAround(Vector3.zero, cameraLeft, angle - currentAngle);
				currentAngle = angle;
			}
			else
			{
				Camera.main.transform.RotateAround(Vector3.zero, cameraLeft, destinationAngle - currentAngle);
				destinationAngle = 0.0f;
				currentAngle = 0.0f;
				map.IsDirty = true;
			}
			
			map.HasMoved = true;
		}
	}

	public bool addMarker = false;
	public double addMarkerLatitude;
	public double addMarkerLongitude;
	public float clickRadius = 0.15f;

	public void AddMarker(string unique,double latitude, double Longitude,string price)
	{
		// create some test 2D markers
		GameObject go = TileBehaviour.CreateTileTemplate(TileBehaviour.AnchorPoint.BottomCenter).gameObject;
		go.GetComponent<Renderer>().material.mainTexture = MarkerTexture;
		go.GetComponent<Renderer>().material.renderQueue = 4001;
		go.transform.localScale = new Vector3(0.70588235294118f, 1.0f, 1.0f);
		go.transform.localScale /= 7.0f;
		go.AddComponent<CameraFacingBillboard>().Axis = Vector3.up;

		GameObject g = new GameObject ();
		TextMesh tm = g.AddComponent<TextMesh> ();
		tm.fontSize = 100;
		tm.text = price;
		tm.color = Color.red;

		g.transform.position = new Vector3 (-0.12f,0f,0.09f); 
		//g.transform.rotation = new Quaternion (90f,308.8336f,0f,0f);
		g.transform.localEulerAngles = new Vector3(90f,308.8336f,0f);
		g.transform.localScale = new Vector3 (0.01f,0.01f,0f);
		//g.layer = 8;
		g.transform.SetParent (go.transform);
		g.GetComponent<Renderer> ().material.renderQueue = 4002;

		GameObject markerGO;
		markerGO = Instantiate(go) as GameObject;
		map.CreateMarker<MarkerBehaviour>(unique, new double[2] { latitude, Longitude }, markerGO);
		GameObject.Find(unique).AddComponent<SphereCollider>();
		GameObject.Find(unique).GetComponent<SphereCollider>().radius = clickRadius;

		DestroyImmediate(go);


		//{ 4.83527, 45.76487 }
	 	
//		go = TileBehaviour.CreateTileTemplate(TileBehaviour.AnchorPoint.BottomCenter).gameObject;
//		TextToTexture tt;
////		tt.CreateTextToTexture ("sample", 0, 0, 10, 10, 10);
//		go.GetComponent<Renderer>().material.mainTexture = tt.CreateTextToTexture ("sample", 0, 0, 10, 10, 10);
//
//		go.GetComponent<Renderer>().material.renderQueue = 4001;
//		go.transform.localScale = new Vector3(0.70588235294118f, 1.0f, 1.0f);
//		go.transform.localScale /= 7.0f;
//		go.AddComponent<CameraFacingBillboard>().Axis = Vector3.up;
//		
//	
//		markerGO = Instantiate(go) as GameObject;
//		map.CreateMarker<MarkerBehaviour>(unique, new double[2] { latitude, Longitude }, markerGO);
//		GameObject.Find(unique).AddComponent<SphereCollider>();
//		GameObject.Find(unique).GetComponent<SphereCollider>().radius = clickRadius;
//		
//		DestroyImmediate(go);
		
		
	}

	public bool getGpsCoordinates = false;

	public IEnumerator GetGpsCoordidates()
	{
		Debug.Log("getting gps");

		// First, check if user has location service enabled
		if (!Input.location.isEnabledByUser)
			yield break;
		
		// Start service before querying location
		Input.location.Start();
		
		// Wait until service initializes
		int maxWait = 20;
		while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
		{
			yield return new WaitForSeconds(1);
			maxWait--;
		}
		
		// Service didn't initialize in 20 seconds
		if (maxWait < 1)
		{
			print("Timed out");
			yield break;
		}
		
		// Connection has failed
		if (Input.location.status == LocationServiceStatus.Failed)
		{
			print("Unable to determine device location");
			yield break;
		}
		else
		{
			// Access granted and location value could be retrieved
			print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
			StartCoroutine(loadmap(Input.location.lastData.latitude,Input.location.lastData.longitude));
		}
		
		// Stop service if there is no need to query location updates continuously
		Input.location.Stop();
		
	}

}











