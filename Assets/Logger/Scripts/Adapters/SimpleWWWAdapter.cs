using SimpleJSON;
using UnityEngine;
using System.Collections;

// Adapter for logging through HTTP POST using Unity's WWW API.
public class SimpleWWWAdapter : BaseAdapter
{
	private string urlBase="http://107.21.26.163/secphp/json_to_server.php?user=nugs&pass=7dc2110243bfbd86f83bbeb4d412e1ce";
	private WWW data;

	public
	override
	void
	Init()
	{
	}
	
	public
	override
	void
	Fini()
	{
	}
	
	public
	override
	void
	Handle(JSONClass node)
	{

		string node_output = WWW.EscapeURL(node.ToString().Trim());
		string url = urlBase + "&json=" + node_output + "&file=optimization/"+node["data"]["session_id"]+".json";
		WWWForm form = new WWWForm();
		form.AddField("data", node_output);
		data = new WWW(url, form);
		Debug.Log("Sending data to: " + url);
        if (DoWWW(data) != null)
        {
            StartCoroutine(DoWWW(data));
        }
	}


	IEnumerator DoWWW(WWW www)
	{
		yield return www;
		//Debug.Log("Web returned: " + www.text);
		if (www.error != null)
		{
			Debug.Log("Web error: " + www.error);
		}
	}

	// If there has been any error, return a description of that error, otherwise, null.
	public
	override
	string
	Error()
	{
		return null;
	}
}
