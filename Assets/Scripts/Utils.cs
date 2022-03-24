using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Utils 
{
	public const string LABEL_CREATE_NEW_ROOM = "Create a new room";
	public const string LABEL_BUTTON_CREATE_ROOM = "Create Room";
	public const string LABEL_BUTTON_JOIN_ROOM = "Join Room";

	public const int MAXIMUM_PLAYERS_PER_ROOM = 6;


	// taken from https://stackoverflow.com/a/1344242
	// licensed under CC BY-SA 4.0
	// the only modification is the inclusion of System.Random instead of just Random (to differentiate from UnityEngine.Random)
	private static System.Random random = new System.Random();
	public static string RandomString(int length)
	{
		const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
		return new string(Enumerable.Repeat(chars, length)
			.Select(s => s[random.Next(s.Length)]).ToArray());
	}

	public static int GetRandomRange(int sizeMin, int sizeMax){
		System.Random rand = new System.Random();
		return rand.Next(sizeMin,sizeMax);
	}
}
