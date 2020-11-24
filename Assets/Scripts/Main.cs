using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class Main : MonoBehaviour
{
	private void Start()
	{
		Debug.Log("Start Called");
		UserDetails newUser = new UserDetails( "newDeveloper", "gameOff2020");

		//Register the new User
		RegisterClicked(newUser);

		string json = File.ReadAllText("E:/Unity/GameOff2020/Assets/StreamingAssets/saveFile.json");
		//Create a new Level and Store it
		LevelDetails levelData = new LevelDetails("Level A", json);
		StoreLevel(levelData);

		//Verify the UserName is Unique
		//VerifyUniqueUserName(newUser);
	}

	public void VerifyUniqueUserName(UserDetails newUser)
	{
		Debug.Log("get all users");
		//List<UserDetails> allUsers = new List<UserDetails>();
		DBCommunicator.GetAllUser(allUsers =>
		{
			bool uniqueName = true;
			foreach (UserDetails user in allUsers)
			{
				if (user.userName.ToLower() == newUser.userName.ToLower())
				{
					uniqueName = false;
					break;
				}
			}
			if (uniqueName)
			{
				RegisterClicked(newUser);
			}
			else
			{
				Debug.LogError("The UserName is already in use");
			}
		});
	}

	public void RegisterClicked(UserDetails newUserDetails)
	{
		DBCommunicator.PutUserRegister(newUserDetails, newUID => 
		{
			Debug.Log("new user's id is "+ newUID);
			DBCommunicator.GetRegisteredUser(newUID, userDetails => 
			{
				RegisterToPlayerPref(userDetails);
			});
		});
	}

	public void RegisterToPlayerPref(UserDetails newUserDetails)
	{
		PlayerPrefs.SetInt("pp_u_id", newUserDetails.uId);
		PlayerPrefs.SetString("pp_u_username", newUserDetails.userName);
		PlayerPrefs.SetString("pp_u_password", newUserDetails.uPass);

		//Debug.Log("in player prefs, newUserDetails.uId : "+ newUserDetails.uId);
		//Debug.Log("in player prefs, newUserDetails.uFirstName : " + newUserDetails.uFirstName);
		//Debug.Log("in player prefs, newUserDetails.uSurname : " + newUserDetails.uSurname);
		//Debug.Log("in player prefs, newUserDetails.userName : " + newUserDetails.userName);
		//Debug.Log("in player prefs, newUserDetails.uPass : " + newUserDetails.uPass);
	}

	public void StoreLevel(LevelDetails levelD)
	{
		Debug.Log("calling store Level");
		Debug.Log(levelD.levelJson);
		Debug.Log(levelD.levelName);
		DBCommunicator.StoreLevel(levelD, () =>
		{
			Debug.Log("Level Stored");
		});
	}

	public void GetLevel(string levelName)
	{
		DBCommunicator.GetLevel(levelName, levelDetails => {
			Debug.Log("levelDetails: " + levelDetails.levelJson);
		});
	}
}
