using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartNewTrail : MonoBehaviour {

	void OnMouseDown(){
		if (!gameControll.redTruck && !gameControll.greenTruck && !gameControll.blueTruck) {
            int[,] arr = gameControll.capArray;
            int sum = 0;
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    sum += arr[i, j];
                }
            }
            if (sum == 0)
            {
                SceneManager.LoadScene("start");
            }
		}
	}
}
