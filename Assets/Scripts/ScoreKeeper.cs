using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;

public class ScoreKeeper : MonoBehaviourPunCallbacks
{
	[SerializeField]
	private TMPro.TextMeshProUGUI scoreboardUI;
	[SerializeField]
	private Collider2D bottomFloor;

	private bool initialized = false;

    void Start()
    {
		Debug.Log("populate");
		addNewPlayer(PhotonNetwork.LocalPlayer.UserId,true);
		initialized = true;
    }

    void FixedUpdate()
    {
		updateScores();
    }

	public void logKill(string killer){
		for (int x = 0; x < PhotonNetwork.PlayerList.Length; x++){
			if (PhotonNetwork.PlayerList[x].UserId == killer){
				PhotonNetwork.PlayerList[x].AddScore(1);
			}
			Debug.Log("Score of " + PhotonNetwork.PlayerList[x].UserId + ": " + PhotonNetwork.PlayerList[x].GetScore());
		}
	}

	private void addNewPlayer(string newUserID, bool isLocal){
		//Debug.Log(bottomFloor.bounds);
		float xPosition = (
			(Utils.GetRandomRange(0,Utils.MAXIMUM_PLAYERS_PER_ROOM)) * 
		(bottomFloor.bounds.size.x / (Utils.MAXIMUM_PLAYERS_PER_ROOM)));
		//Debug.Log("xposition: " + xPosition);
		GameObject newPlayerGameObject = PhotonNetwork.Instantiate("Player", new Vector3(bottomFloor.bounds.min.x + xPosition,-3,0),Quaternion.identity);
		newPlayerGameObject.GetComponent<PlayerController>().setUserID(newUserID);
		newPlayerGameObject.GetComponent<PlayerController>().isLocalPlayer = isLocal;
		newPlayerGameObject.GetComponent<SpriteRenderer>().color = new Color(
			Random.Range(0f, 1f), 
      		Random.Range(0f, 1f), 
      		Random.Range(0f, 1f)
		);
		newPlayerGameObject.GetComponent<PlayerController>().setScoreKeeper(this);
	}

	private void updateScores(){
		if (initialized) {
			string scores = "";
			for (int x = 0; x < PhotonNetwork.PlayerList.Length; x++) {
				scores += ( System.Environment.NewLine + PhotonNetwork.PlayerList[x].NickName + ": " + PhotonNetwork.PlayerList[x].GetScore() + " kills");
			}
			if (scores == "") scores = "No Scores";
			scoreboardUI.text = scores;
		} 
	}

}
