using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace SocketNetworkingTest_Client
{
	/// <summary>
	/// 서버와 통신할 메시지의 규격을 지정합니다.
	/// 헤당 코드는 항상 서버와 동일해야 합니다. 
	/// </summary>
	/// <remarks>
	/// SerializeField, JsonConstructor와 같은 Attribute는 상황에 따라 사용합니다. (서버와 다른 값일수도 있습니다.)
	/// </remarks>
	[System.Serializable]
	public class SocketMessage
	{
		[SerializeField]
		public string socketEvent;

		[SerializeField]
		public string data;

		public SocketMessage(string socketEvent, string data)
		{
			this.socketEvent = socketEvent;
			this.data = data;
		}
	}
}
