using UnityEngine;
using System.Collections;

public class stickMap : MonoBehaviour {
	private GameObject rm;   //get the right mini map panel
	private Camera cam;
	Vector3 screenPos;
	public Material m_Mat = null;
	[Range(0.01f, 0.2f)] public float m_DarkRange = 0.02f;
	[Range(-2.5f, -1f)] public float m_Distortion = -1.4f;
	[Range(0.05f, 0.3f)] public float m_Form = 0.2f;


	void OnRenderImage (RenderTexture sourceTexture, RenderTexture destTexture)
	{
		m_Mat.SetVector ("_Center", new Vector4 (0.5f, 0.6f, 0f, 0f));
		m_Mat.SetFloat ("_DarkRange", m_DarkRange);
		m_Mat.SetFloat ("_Distortion", m_Distortion);
		m_Mat.SetFloat ("_Form", m_Form);
		Graphics.Blit (sourceTexture, destTexture, m_Mat);
	}

	void Start(){
		//rm= GameObject.Find("rightMap");
		//cam = GetComponent<Camera> ();
		//Debug.Log (rm.transform.position);
		//screenPos=cam.WorldToScreenPoint(rm.transform.position);
	}

	void Update(){
		//try to fix the right map bugs if we change the size of screen during the game, but in fact we won't do it;
//		if (Screen.width == 1155) {
//			screenPos=cam.WorldToScreenPoint(rm.transform.position);
//		}
//		if (Screen.width == 480) {
//			screenPos=cam.WorldToScreenPoint(rm.transform.position);
//		}
		//stick ();
	}

//	private void stick(){
//		int camSize = (int) cam.orthographicSize;
//		rm.transform.position = cam.ScreenToWorldPoint (screenPos);
//		switch (camSize) {
//		case 4:
//			rm.transform.localScale = new Vector2 (0.8f, 0.8f);
//			//rm.SetActive (false);
//			break;
//		case 3:
//			rm.transform.localScale = new Vector2 (0.6f, 0.6f);
//			//rm.SetActive (false);
//			break;
//		case 2:
//			rm.transform.localScale = new Vector2 (0.4f, 0.4f);
//			//rm.SetActive (false);
//			break;
//		case 1:
//			rm.transform.localScale = new Vector2 (0.2f, 0.2f);
//			//rm.SetActive (false);
//			break;
//		default:
//			rm.transform.localScale = new Vector2 (1f, 1f);
//			//rm.SetActive (false);
//			break;
//		}
//	}
}