using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using SimpleJSON;

public class StartNewTrail : MonoBehaviour {
    private DisplayManager display;
    public static int numOfTrail;

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
            if (sum == 0 && submitButton.submitAndNewScene!=0 && numOfTrail<5)
            {
                numOfTrail++;
                SceneManager.LoadScene("start");
            }
            else
            {
                JSONClass details = new JSONClass();
                details["Incorrect Operation"] = "Start New Trail before submit";
                TheLogger.instance.TakeAction(10, details);
                display = DisplayManager.Instance();
                display.DisplayMessage("Pls submit before start new!");
            }
        }else
        {
            JSONClass details = new JSONClass();
            details["Incorrect Operation"] = "Start New Trail before submit";
            TheLogger.instance.TakeAction(10, details);
            display = DisplayManager.Instance();
            display.DisplayMessage("Pls submit before start new!");
        }
	}
}
