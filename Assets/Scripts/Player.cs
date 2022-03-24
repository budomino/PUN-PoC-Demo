using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayer 
{
	private string playerID;
	private List<string> kills = new List<string>();
	private Color playerColor;
	private string playerName;

	public void addKill(string victim){
		kills.Add(victim);
	}

	public int getNumberOfKills(){
		return kills.Count;
	}

	public string getPlayerID(){
		return playerID;
	}

	public void setPlayerID(string newID){
		playerID = newID;
	}

	public void setPlayerColor(Color newColor){
		playerColor = newColor;
	}

	public Color getPlayerColor(){
		return this.playerColor;
	}

	public void setPlayerName(string newName){
		playerName = newName;
	}

	public string getPlayerName(){
		return playerName;
	}

	public GamePlayer(string playerIDNew, string nickname){
		playerID = playerIDNew;
		playerName = nickname;
		playerColor = new Color(
			Random.Range(0f, 1f), 
      		Random.Range(0f, 1f), 
      		Random.Range(0f, 1f)
		);
	}
}
