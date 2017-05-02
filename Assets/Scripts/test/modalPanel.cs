using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;

public class modalPanel : MonoBehaviour {
	public Text question;
	//public Image iconImage;
	public Button quitGame;
	public Button backtoGame;

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

	public void Choice(string question, UnityAction newcapEvent,UnityAction newpathEvent){
		modalPanelObject.SetActive (true);

		quitGame.onClick.RemoveAllListeners ();
		quitGame.onClick.AddListener (newcapEvent);
		quitGame.onClick.AddListener (closePanel);

		backtoGame.onClick.RemoveAllListeners ();
		backtoGame.onClick.AddListener (newpathEvent);
		backtoGame.onClick.AddListener (closePanel);

		this.question.text = question;

		quitGame.gameObject.SetActive (true);
		backtoGame.gameObject.SetActive (true);
	}

	void closePanel(){
		modalPanelObject.SetActive (false);
	}

}
	
