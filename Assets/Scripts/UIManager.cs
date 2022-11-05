using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace SocketNetworkingTest_Client
{
	/// <summary>
	/// UI 노출과 이벤트를 관리합니다.
	/// </summary>
	public class UIManager : MonoBehaviour
	{
		[SerializeField]
		private WebSocketClient webSocketClient;
		[SerializeField]
		private ScriptRunner scriptRunner;

		[Header("UI")]
		[SerializeField]
		private Text connectionStatusText;

		[SerializeField]
		private InputField dataInputField;

		[SerializeField]
		private Text logsText;

		private void Start()
		{
			webSocketClient.onMessage += OnMessage;
		}

		private void Update()
		{
			connectionStatusText.text = "Connected: " + webSocketClient.Connected;
		}

		public void SendButtonClicked()
		{
			webSocketClient.SendMessage(new SocketMessage("Message", dataInputField.text));
		}

		public void DisconnectButtonClicked()
		{
			webSocketClient.DisconncectServer(WebSocketSharp.CloseStatusCode.Normal, "Disconnected by User");
		}

		public void OnMessage(SocketMessage socketMessage)
		{
			scriptRunner.executeUpdate += () =>
			{
				logsText.text += string.Format("{0} | {1}\n", socketMessage.socketEvent, socketMessage.data);
			};
		}
	}
}
