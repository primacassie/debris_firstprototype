using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;

public class testWindow : MonoBehaviour {
	private modalPanel modalPanel;
	//private DisplayManager displayManger;

	private UnityAction mycapAction;
	private UnityAction mypathAction;

	void Awake(){
		//displayManger = DisplayManager.Instance ();
		mycapAction = new UnityAction (testCap);
		mypathAction = new UnityAction (testPath);
	}

	//add this function in gameControl inputIsRight function.
	public void takeAction(string dialog){
		modalPanel = modalPanel.Instance ();
		modalPanel.Choice (dialog, mycapAction,mypathAction);
	}

	void testCap(){
		Application.Quit ();
	}

	void testPath(){
		//displayManger.DisplayMessage ("this is new path!");
	}

		
}
