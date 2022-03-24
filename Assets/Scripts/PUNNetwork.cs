using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PUNNetwork : MonoBehaviourPunCallbacks
{
	[SerializeField]
	private TMPro.TMP_Dropdown dropDown;

	[SerializeField]
	private TMPro.TMP_InputField nameBox;

    // Start is called before the first frame update
    void Start()
    {
		PhotonNetwork.GameVersion = "0.0.1";
		PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public override void OnConnectedToMaster() {
		PhotonNetwork.AutomaticallySyncScene = true;
		PhotonNetwork.JoinLobby();
		Debug.Log("Connected to Master");
	}

	public override void OnRoomListUpdate(List<RoomInfo> roomList)
	{
		base.OnRoomListUpdate(roomList);
		List<string> roomNames = new List<string>();
		roomNames.Add(Utils.LABEL_CREATE_NEW_ROOM);
		foreach (RoomInfo room in roomList){
			bool alreadyContainsName = false;
			foreach (TMPro.TMP_Dropdown.OptionData option in dropDown.options){
				if (option.text == room.Name) alreadyContainsName = true;
			}
			if (!alreadyContainsName) roomNames.Add(room.Name);
		}
		if (dropDown.options.Count > 0) dropDown.ClearOptions();
		dropDown.AddOptions(roomNames);
	}

	public override void OnCreatedRoom()
	{
		base.OnCreatedRoom();
	}

	public override void OnJoinedRoom()
	{
		base.OnJoinedRoom();
		PhotonNetwork.LoadLevel(1);
	}

	public override void OnCreateRoomFailed(short returnCode, string message)
	{
		base.OnCreateRoomFailed(returnCode, message);
	}

	public void joinOrCreateRoom(){
		if (!PhotonNetwork.IsConnected) {
			Debug.Log("Not connected; cannot create room");
			return;
		}
		RoomOptions roomOptions = new RoomOptions();
		roomOptions.PublishUserId = true;
		roomOptions.BroadcastPropsChangeToAll = true;
		string roomName;
		if (dropDown.options[dropDown.value].text == Utils.LABEL_CREATE_NEW_ROOM){
			roomName = Utils.RandomString(8);
			roomOptions.MaxPlayers = 6;
		} else if (dropDown.options[dropDown.value].text == null){
			roomName = Utils.RandomString(8);
			roomOptions.MaxPlayers = 6;
		} else {
			roomName = dropDown.options[dropDown.value].text;
		}
		PhotonNetwork.LocalPlayer.NickName = nameBox.text;
		PhotonNetwork.JoinOrCreateRoom(roomName,roomOptions,TypedLobby.Default);
	}

}
