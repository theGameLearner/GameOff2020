/*
 * Copyright (c) The Game Learner
 * https://connect.unity.com/u/rishabh-jain-1-1-1
 * https://www.linkedin.com/in/rishabh-jain-266081b7/
 * 
 * created on - #CREATIONDATE#
 */

using System.Collections.Generic;
using System.Linq;
using FullSerializer;
using Proyecto26;

public static class DBCommunicator
{
    private const string projectId = "GameOff2020"; // You can find this in your Firebase project settings
    private static readonly string databaseURL = $"https://gameoff2020-fe105.firebaseio.com/";
    private static fsSerializer serializer = new fsSerializer();

    //public delegate void GetAuthorizedUserCallback(bool authorised);
    public delegate void GetLastUserIdCallback(LastUId LastUIdObj);
    public delegate void PetLastUserIdCallback();
    public delegate void PostNewRegisterCallback(int newUID);
    public delegate void GetRegisteredUserCallback(UserDetails userDetails);
    public delegate void GetAllUserCallback(List<UserDetails> userList);
    public delegate void StoreLevelCallback();
    public delegate void GetAllLevelsCallback(List<LevelDetails> allLevels);
    public delegate void GetLevelCallback(LevelDetails level);


    public static void PostLastUserID(LastUId uIData, PetLastUserIdCallback callback)
    {
        RestClient.Put<UserDetails>($"{databaseURL}LastUID.json", uIData).Then(response => { callback(); }).Catch(err => {
            UnityEngine.Debug.Log(err);
        });
    }
    
    public static void GetLastUserID(GetLastUserIdCallback callback)
	{
        RestClient.Get<LastUId>($"{databaseURL}LastUID.json").Then(response => { callback(response); }).Catch(err => {
            UnityEngine.Debug.Log(err);
        });
    }

    public static void PutUserRegister(UserDetails newUserDetails, PostNewRegisterCallback callback)
    {
        DBCommunicator.GetLastUserID(uID =>
        {
            uID.LastUserID += 3;
            DBCommunicator.PostLastUserID(uID, () => { });
            newUserDetails.uId = uID.LastUserID;
            RestClient.Put<UserDetails>($"{databaseURL}Users/{newUserDetails.uId}.json", newUserDetails).Then(response => 
            {
                callback(newUserDetails.uId);
            }).Catch(err => {
                UnityEngine.Debug.Log(err);
            });
        });
    }

    public static void GetRegisteredUser(int uID, GetRegisteredUserCallback callback)
	{
        RestClient.Get<UserDetails>($"{databaseURL}Users/{uID}.json").Then(response => { callback(response); }).Catch(err => {
            UnityEngine.Debug.Log(err);
        });
    }

    public static void GetAllUser(GetAllUserCallback callback)
	{
        RestClient.Get($"{databaseURL}Users.json").Then(response =>
        {
            var responseJson = response.Text;
			//UnityEngine.Debug.Log("response : " + response);

			// Using the FullSerializer library: https://github.com/jacobdufault/fullserializer
			// to serialize more complex types (a Dictionary, in this case)
			var data = fsJsonParser.Parse(responseJson);

            UnityEngine.Debug.Log("user data : " + data);
            object deserialized = null;
            serializer.TryDeserialize(data, typeof(Dictionary<string, UserDetails>), ref deserialized);

            var users = deserialized as Dictionary<string, UserDetails>;
            List<UserDetails> userList = users.Values.ToList();
            callback(userList);
        });
    }

    public static void StoreLevel(LevelDetails lDetails, StoreLevelCallback callback)
	{
        RestClient.Put<LevelDetails>($"{databaseURL}Levels/{lDetails.levelName}.json", lDetails).Then(response =>
        {
            UnityEngine.Debug.Log("Response as "+response);
            callback();
        }).Catch(err => {
            UnityEngine.Debug.Log(err);
        });
    }

    public static void GetAllLevels(GetAllLevelsCallback callback)
	{
        RestClient.Get($"{databaseURL}Levels.json").Then(response =>
        {
            var responseJson = response.Text;
            //UnityEngine.Debug.Log("response : " + response);

            // Using the FullSerializer library: https://github.com/jacobdufault/fullserializer
            // to serialize more complex types (a Dictionary, in this case)
            var data = fsJsonParser.Parse(responseJson);

            UnityEngine.Debug.Log("level data : " + data);
            object deserialized = null;
            serializer.TryDeserialize(data, typeof(Dictionary<string, LevelDetails>), ref deserialized);

            var levels = deserialized as Dictionary<string, LevelDetails>;
            UnityEngine.Debug.Log("levels : " + levels);
            List<LevelDetails> levelList = levels.Values.ToList();
            callback(levelList);
        });
    }

    public static void GetLevel(string levelName, GetLevelCallback callback)
    {
        RestClient.Get<LevelDetails>($"{databaseURL}Levels/{levelName}.json").Then(response => { callback(response);});
    }
}