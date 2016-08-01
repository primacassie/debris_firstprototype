using UnityEngine;
using System.Collections;

public class StoreTruckClick : MonoBehaviour {

	void OnMouseDown(){
		if (!(gameControll.redTruck || gameControll.greenTruck || gameControll.blueTruck)) {
			Debug.Log (this.gameObject.name);
		}
	}
}
