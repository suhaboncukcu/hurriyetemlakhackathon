using UnityEngine;
using System.Collections;

public class MButton : MonoBehaviour {

	bool EnabledNow = false;

	public void SwitchEnable(GameObject g)
	{
		if (EnabledNow) {
			EnabledNow = false;
			g.SetActive (false);
		} else {
			EnabledNow = true;
			g.SetActive(true);
		}

	}

}
