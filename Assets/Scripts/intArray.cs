using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class intArray:MonoBehaviour  {
	public int[] genericArray{ get{ return genericArray; } set{genericArray = value; }  }
	public intArray(int[] arr){
		this.genericArray = arr;

	}

	public intArray GenericMethod<intArray>(intArray param) where intArray : Component
	{
		return param;
	}

}
