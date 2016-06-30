using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;

public class modalPanel : MonoBehaviour {
	public Text question;
	//public Image iconImage;
	public Button newCap;
	//public Button newPath;

	public GameObject modalPanelObject;
	private static modalPanel modalpanel;

	public static modalPanel Instance () {
		if (!modalpanel) {
			modalpanel = FindObjectOfType(typeof (modalPanel)) as modalPanel;
			if (!modalpanel)
				Debug.LogError ("There needs to be one active ModalPanel script on a GameObject in your scene.");
		}

		return modalpanel;
	}

	public void Choice(string question, UnityAction newcapEvent){
		modalPanelObject.SetActive (true);

		newCap.onClick.RemoveAllListeners ();
		newCap.onClick.AddListener (newcapEvent);
		newCap.onClick.AddListener (closePanel);

//		newPath.onClick.RemoveAllListeners ();
//		newPath.onClick.AddListener (newpathEvent);
//		newPath.onClick.AddListener (closePanel);

		this.question.text = question;

		newCap.gameObject.SetActive (true);
		//newPath.gameObject.SetActive (true);
	}

	void closePanel(){
		modalPanelObject.SetActive (false);
	}

}
	
