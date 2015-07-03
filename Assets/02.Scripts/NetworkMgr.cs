using UnityEngine;
using System.Collections;

public class NetworkMgr : MonoBehaviour {
	//接続IP
	private const string ip = "127.0.0.1";
	//接続ポート
	private const int port = 30000;
	//NAT接続の使用の有無
	private bool _useNat = false;
	
	public GameObject player;
	
	void OnGUI()
	{
		//現在のユーザーのネットワーク接続の有無を判断
		if(Network.peerType == NetworkPeerType.Disconnected)
		{
			//サーバー生成ボタン
			if(GUI.Button (new Rect(20,20,200,25),"Start Server"))
			{
				//サーバー生成
				Network.InitializeServer(20,port,_useNat);
			}
			//ゲームへに接続ボタン
			if(GUI.Button (new Rect(20,50,200,25),"Connect to Server"))
			{
				//ゲームサーバーへの接続
				Network.Connect (ip,port);
			}
		}else{
			//サーバーの場合はメッセージを表示
			if(Network.peerType == NetworkPeerType.Server)
			{
				GUI.Label (new Rect(20,20,200,25),"Initializstion Server...");
				GUI.Label(new Rect(20,50,200,25),"Client Count = "+ Network.connections.Length.ToString());
			}
			//クライアントに接続したときのメッセージを出力
			if(Network.peerType == NetworkPeerType.Client)
			{
				GUI.Label (new Rect(20,20,200,25),"Connect to Server");
			}
		}
	}
	//サーバーとして起動し、さーばーの初期化が正常に完了したときに呼び出される
	void OnServerInitialized()
	{
		StartCoroutine(this.CreatePlayer());
	}
	//クライアントとしてゲームサーバーに接続したときに呼び出される
	void OnConnectedToServer()
	{
		StartCoroutine(this.CreatePlayer());
	}
	
	//プレイヤーを生成させるコルーチン関数
	IEnumerator CreatePlayer()
	{
		Vector3 pos = new Vector3(Random.Range (-20.0f,20.0f),0.0f,(-20.0f,20.0f));
		//ネットワーク上のプレイヤーを動的に生成
		Network.Instantiate(player,pos,Quaternion.identity,0);
		yield return null;
	}
	
}
