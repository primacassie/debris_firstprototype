using UnityEngine;
using System.Collections;

public class truck  {
	public int capacity;

	public truck(){
		this.capacity = 100;
		Debug.Log ("this is a car with 100 cap");
	}
}

public class RedTruck:truck{
	public bool isActiveRed;
	public RedTruck(){
		isActiveRed = true;
	}
}

public class BlueTruck:truck{
	public bool isActiveBlue;
	public BlueTruck(){
		isActiveBlue = true;
	}
}

public class GreenTruck:truck{
	public bool isActiveGreen;
	public GreenTruck(){
		isActiveGreen = true;
	}
}
