This script is created using the reference of 'http://wiki.unity3d.com/index.php/Advanced_CSharp_Messenger'

Any changes from the original is to help me make a better use of it. 
If I am breaking any licence or permission by doing so, please reach out to me on linkedIn or UnityConnect on the below mentioned urls:
 * https://connect.unity.com/u/rishabh-jain-1-1-1
 * https://www.linkedin.com/in/rishabh-jain-266081b7/


 To use call functions with no setup needed.



To Add Messenger to your desired GameObject and you can use 
	Messenger.AddListener<GameObject>("prop collected", PropCollected);
	this adds the listner for string "prop collected" which when broadcasted will be redirected to PropCollected() method that takes a single argument of dataType - GameObject.

To Broadcast a Message you can call 
	Messenger.Broadcast<GameObject>("prop collected", prop);
	this will broadcast a string "prop collected" to all listeeners that are listening for it and receives a single argument of GameObject type and pass prop to it as a argument.

To remove a messenger listener, we use
	Messenger.RemoveListener<GameObject>("prop collected", PropCollected);
	this removes the listner for string "prop collected" which when broadcasted will no longer be redirected to PropCollected() method that takes a single argument of dataType - GameObject.


you can use 'MessengerUser' to use as demo if needed