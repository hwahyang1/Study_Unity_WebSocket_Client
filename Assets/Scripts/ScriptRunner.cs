using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Action = System.Action;

namespace SocketNetworkingTest_Client
{
	/// <summary>
	/// 특정한 코드를 Unity Tread에서 실행 할 수 있도록 합니다.
	/// </summary>
	public class ScriptRunner : MonoBehaviour
	{
		public Action executeUpdate = null;
		public Action executeLateUpdate = null;
		public Action executeFixedUpdate = null;

		private void Update()
		{
			executeUpdate?.Invoke();
			executeUpdate = null;
		}

		private void LateUpdate()
		{
			executeLateUpdate?.Invoke();
			executeLateUpdate = null;
		}

		private void FixedUpdate()
		{
			executeFixedUpdate?.Invoke();
			executeFixedUpdate = null;
		}
	}
}
