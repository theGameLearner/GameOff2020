using UnityEngine;

class MessengerUser : MonoBehaviour
{
	private void Start()
	{
		Messenger.AddListener("game_start", GameStarted);
		Messenger.Broadcast("game_start");
	}

	void GameStarted()
	{
		Messenger.RemoveListener("game_start", GameStarted);
		Debug.Log("game has started");
	}
}