
﻿using SimpleJSON;
using System.IO;
using UnityEngine;
using System.Collections;


// Adapter for logging to a file.
public class FileAdapter : BaseAdapter
{
	public string logfilePrefix;

	private StreamWriter m_out = null;
	private bool m_inited = false; // has init been called?
	private string m_error = null; // a string describing any error

    // Try to initialize logging to the given file.  If logging has already been initialized, do nothing.
    private void Start()
    {
        DontDestroyOnLoad(this);
    }


    public
	override
	void
	Init()
	{
		if (m_inited) {
			Debug.Log("Re-initializing logger");
			return;
		}

		m_inited = true;

		try {

          Manager.Instance.sessionID= System.Guid.NewGuid().ToString(); 
          m_out = new StreamWriter(logfilePrefix + "-" +  Manager.Instance.sessionID + ".json");

			Debug.Log("logger"+logfilePrefix);
			
			m_out.Write("# version:1\n");
			m_out.Flush();
		} catch (System.Exception ex) {
			m_error = ex.Message;
			Debug.Log(m_error);
		}
	}
	
	// Finish file logging.
	public
	override
	void
	Fini()
	{
		if (m_error != null) {
			return;
		}
		if (!m_inited) {
			Debug.Log("Finalizing uninitialized logger");
			return;
		}

		try {
			m_out.Flush();
			m_out.Close();
			m_out = null;
		} catch (System.Exception ex) {
			m_error = ex.Message;
			Debug.Log(m_error);
		}
	}
	
	public
	override
	void
	Handle(JSONClass node)

	{	if (m_error != null) {
			return;
		}
		if (!m_inited) {
			Debug.Log("Logger not initialized");
			return;
		}

		try {
          string node_output =WWW.EscapeURL(node.ToString().Trim().Replace("\n", " ") + "\n"+",");

		//	m_out.Write(node_output);
		//	m_out.Flush();
          string urlBase="http://107.21.26.163/secphp/json_to_server.php?user=nugs&pass=7dc2110243bfbd86f83bbeb4d412e1ce";
        
         
         // string node_output = WWW.EscapeURL(node.ToString().Trim());
          string url = urlBase + "&json=" + node_output + "&file=optimization/"+ Manager.Instance.sessionID+".json";
          WWW www = new WWW(url);
          StartCoroutine(WaitForRequest(www));
        }
        catch (System.Exception ex) {
          m_error = ex.Message;
          Debug.Log(m_error);
        }
      
    }
      IEnumerator WaitForRequest(WWW www)
        {
          yield return www;

          // check for errors
          if (www.error == null)
            {
              Debug.Log("WWW Ok!: " + www.data);
            } else {
              Debug.Log("WWW Error: "+ www.error);
            }    
        }

  public
  override
  string
  Error()
    {
      return m_error;
    }
}
  