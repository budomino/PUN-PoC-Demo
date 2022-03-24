using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
	public BoxCollider2D groundContact;
    // Start is called before the first frame update
    void Start()
    {
		groundContact = this.gameObject.AddComponent<BoxCollider2D>() as BoxCollider2D;
		groundContact.isTrigger = true;
        groundContact.offset.Set(0,0.25F);
		groundContact.size.Set(this.GetComponent<Collider2D>().bounds.size.x,0.14F);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
