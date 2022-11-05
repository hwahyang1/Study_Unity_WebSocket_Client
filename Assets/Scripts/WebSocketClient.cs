using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using WebSocketSharp;

namespace SocketNetworkingTest_Client
{
	/// <summary>
	/// 서버와의 연결을 관리합니다.
	/// </summary>
	public class WebSocketClient : MonoBehaviour
	{
		private readonly string IP = "127.0.0.1";
		private readonly string PORT = "6000";
		private readonly string SERVICE_NAME = "/Server";

		/// <summary>
		/// 서버로부터 데이터가 왔을 때 반환받을 Callback을 지정합니다.
		/// </summary>
		public System.Action<SocketMessage> onMessage;

		public bool Connected { get { return socket != null && socket.IsAlive; } }

		private WebSocket socket;

		private void Start()
		{
			socket = new WebSocket("ws://" + IP + ":" + PORT + SERVICE_NAME);
			socket.SetCredentials("nobita", "password", true);

			socket.OnMessage += OnMessage;
			socket.OnError += (sender, e) => { Debug.LogError(e.Message); };
			socket.OnClose += (sender, e) => { DisconncectServer(); };
		}

		/// <summary>
		/// 서버에 접속합니다.
		/// </summary>
		public void Connect()
		{
			if (socket == null || !socket.IsAlive) socket.Connect();
		}

		/// <summary>
		/// 서버 연결을 해제합니다.
		/// </summary>
		/// <param name="statusCode">서버에 반환할 상태 코드를 입력합니다.</param>
		/// <param name="reason">서버에 반환할 상태 설명을 입력합니다.</param>
		public void DisconncectServer(CloseStatusCode statusCode = CloseStatusCode.Undefined, string reason = null)
		{
			if (socket == null) return;

			if (socket.IsAlive) socket.Close(statusCode, reason);
		}

		[System.Obsolete("Using 'SendMessage(SocketMessage socketMessage)' instead.")]
		public new void SendMessage(string message)
		{
			return;
		}

		/// <summary>
		/// 서버에 데이터를 전송합니다.
		/// </summary>
		/// <param name="socketMessage">전송할 데이터를 작성합니다.</param>
		public void SendMessage(SocketMessage socketMessage)
		{
			if (!socket.IsAlive) return;

			string data = JsonUtility.ToJson(socketMessage);

			print(data);

			socket.Send(data);
		}

		// Event
		public void OnMessage(object sender, MessageEventArgs e)
		{
			//Debug.Log(e.Data); // string 데이터
			//Debug.Log(e.RawData); // byte 데이터

			onMessage?.Invoke(JsonUtility.FromJson<SocketMessage>(e.Data));
		}

		private void OnDestroy()
		{
			DisconncectServer(CloseStatusCode.Normal, "Process Exit");
		}
	}

}