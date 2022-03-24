using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropdownButton : MonoBehaviour
{
	[SerializeField]
	private TMPro.TMP_Dropdown dropdown;

	[SerializeField]
	private TMPro.TextMeshProUGUI buttonText;

    void FixedUpdate()
    {
		if (dropdown.options.Count > 0){
			if (buttonText.text != dropdown.options[dropdown.value].text){
				if (dropdown.options[dropdown.value].text == Utils.LABEL_CREATE_NEW_ROOM)
					buttonText.SetText(Utils.LABEL_BUTTON_CREATE_ROOM);
				else 
					buttonText.SetText(Utils.LABEL_BUTTON_JOIN_ROOM);
			}
		} else {
			buttonText.SetText(Utils.LABEL_BUTTON_CREATE_ROOM);
		}
    }
}
