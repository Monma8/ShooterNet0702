using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class DisableNetworkUnlocalplayerBehaviour : NetworkBehaviour {
	//[SerializeField]
	//Behaviour[] behaviours;
	public Behaviour cntrlScript;
	//public AudioSource ausioS;
	public Camera camera;
	// Use this for initialization
	void Start () {
		if(!isLocalPlayer)
		{
			cntrlScript.enabled = false;
			//ausioS.enabled = false;
			camera.enabled = false;
		}
	}
	
	void OnApplicationFocus(bool focusStatus)
	{
		if(isLocalPlayer)
		{
			cntrlScript.enabled = focusStatus;
			//ausioS.enabled = focusStatus;
			camera.enabled = true;
		}
	}
	
}
