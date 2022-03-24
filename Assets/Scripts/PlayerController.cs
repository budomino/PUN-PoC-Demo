using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

public class PlayerController : MonoBehaviour
{
	private ScoreKeeper scoreKeeper;
	public bool isLocalPlayer = false;

	[SerializeField]
	private float horizontalSpeed = 5;
	[SerializeField]
	private float jumpSpeed = 25;
	[SerializeField]
	private float jumpHeight = 1;

	private bool jumpable = true;
	private float currentJumpBasePosition = 0;
	private bool notDead = true;
	private bool spawned = false;

	private Rigidbody2D rigidBody;
	private Collider2D contactBox;
	private Collider2D hitBox;
	private Collider2D squishBox;
	private AudioSource soundEffects;

	private string _userID;
	public string userID => _userID;

	public void setScoreKeeper(ScoreKeeper newScoreKeeper){
		this.scoreKeeper = newScoreKeeper;
	}

    // Start is called before the first frame update
    void Start()
    {
		Collider2D[] colliders = this.GetComponents<Collider2D>();
		for (int i = 0; i < colliders.Length; i++){
			if (colliders[i].isTrigger == false){
				contactBox = colliders[i];
			}
		}
        rigidBody = this.GetComponent<Rigidbody2D>();
		hitBox = this.GetComponent<Collider2D>();
		soundEffects = this.GetComponent<AudioSource>();
		spawned = true;
    }

    // Update is called once per frame
    void Update()
    {
		if (notDead && isLocalPlayer) {
			if (!jumpable && (currentJumpBasePosition + jumpHeight < transform.position.y)){
				rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
				rigidBody.velocity = new Vector2(rigidBody.velocity.x,jumpSpeed * -1);
			}
			if (Input.GetKey("left")) {
				rigidBody.velocity = new Vector2(horizontalSpeed * -1, rigidBody.velocity.y);
			}
			if (Input.GetKey("right")) {
				rigidBody.velocity = new Vector2(horizontalSpeed, rigidBody.velocity.y);
			}
			if (Input.GetKeyDown("up") && jumpable) {
				currentJumpBasePosition = transform.position.y;
				rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
			}
			if (Input.GetKeyUp("left")) {
				rigidBody.velocity = new Vector2(0,rigidBody.velocity.y);
			}
			if (Input.GetKeyUp("right")) {
				rigidBody.velocity = new Vector2(0,rigidBody.velocity.y);
			}
		}
    }

	private void OnCollisionEnter2D(Collision2D other) {
		if (isLocalPlayer) {
			//Debug.Log("Jumpable");
			jumpable = true;
			PhotonView collidedPlayer = other.gameObject.GetComponent<PhotonView>();
			if (collidedPlayer != null && notDead && this.spawned){
				if(other.transform.position.y > this.transform.position.y){
					// get squished and die
					Debug.Log("squished");
					this.notDead = false;
					this.gameObject.transform.localScale = new Vector3(this.transform.localScale.x,this.transform.localScale.y/4,this.transform.localScale.z);
					soundEffects.Play();
					for (int x = 0; x < PhotonNetwork.PlayerList.Length; x++){
						Debug.Log("userID: " + PhotonNetwork.PlayerList[x].UserId + ", other: " + collidedPlayer.Owner.UserId);
						if (PhotonNetwork.PlayerList[x].UserId == collidedPlayer.Owner.UserId){
							PhotonNetwork.PlayerList[x].AddScore(1);
							Debug.Log("Set");
						}
						Debug.Log("Score of " + PhotonNetwork.PlayerList[x].UserId + ": " + PhotonNetwork.PlayerList[x].GetScore());
					}
				}
			}
		}
	}
	private void OnCollisionExit2D(Collision2D other) {
		if (isLocalPlayer) {
			//Debug.Log("Not Jumpable");
			jumpable = false;
		}
	}

	private void OnCollisionStay2D(Collision2D other) {
		if (other.gameObject.GetComponent<Ground>() != null && isLocalPlayer) {
			if (other.gameObject.GetComponent<Ground>().groundContact.IsTouching(hitBox)) {
				//Debug.Log("Jumpable");
				jumpable = true;
			}
		}
	}

	public void setUserID(string newUserID){
		_userID = newUserID;
	}

}
