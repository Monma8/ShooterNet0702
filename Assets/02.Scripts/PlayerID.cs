using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerID : NetworkBehaviour {
	[SyncVar]
	private string playerUniqueIdentity;
	
	private NetworkInstanceId playerNetID;
	private Transform myTransform;
	
	public override void OnStartLocalPlayer()
	{
		Debug.Log ("OnStartLocalPlayer");
		GetNetIdentity();
		SetIdentity();
	}
	
	void Awake()
	{
		Debug.Log("Awake");
		myTransform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		if(myTransform.name == ""||myTransform.name == "Player(Clone)")
		{
			SetIdentity();
		}
	}
	
	[Client]
	void GetNetIdentity()
	{
		playerNetID = GetComponent<NetworkIdentity>().netId;
		CmdTellServerMyIdentity(MakeUniqueIdentity());
	}
	
	void SetIdentity()
	{
		if(!isLocalPlayer)
		{
			myTransform.name = playerUniqueIdentity;
		}else{
			myTransform.name = MakeUniqueIdentity();
		}
	}
	
	string MakeUniqueIdentity()
	{
		string uniqueName = "Player" + playerNetID.ToString();
		return uniqueName; 
	}
	
	//Command: SyncVar変数を変更し、変更結果を全クライアントに送る
	[Command]
	void CmdTellServerMyIdentity(string name)
	{
		playerUniqueIdentity = name;
	}
}
