//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//using System;
//using System.IO;
////using Newtonsoft.Json;
////using Newtonsoft.Json.Linq;
//using SimpleJSON;

//public class DataLogger : MonoBehaviour
//{

//    private string urlBase = "http://107.21.26.163/secphp/json_to_server_overwrite.php?user=nugs&pass=7dc2110243bfbd86f83bbeb4d412e1ce";
//    private string urlFull;

//    public static DataLogger logger;

//    // Dictionary that contains the specific entity data
//    // Key is the string representation of the entity (e.g. "session_begin")
//    // Value is a Dictionary containing the entity data (e.g. "session_id":"12345678")
//    public Dictionary<string, Dictionary<string, string>> entityData;

//    // Dictionary containing the data for all entities
//    // Key is the game version information (e.g. 0616 = June 2016)
//    // Value is a list of entityData (see above)
//    //public Dictionary<string, List<Dictionary<string, Dictionary<string, string>>>> fullTable;

//    //spublic List<Dictionary<string, Dictionary<string, string>>> dataList;

//    public string playerID;

//    public int version = 0616;

//    public int condition = 1;

//    public string currentSessionID;

//    public int sessionCount = 0;

//    public int runCount = 0;

//    public string currentRunID;

//    public int actionCount = 0;

//    public int actionIndex = 0;

//    public int runIndex = 0;

//    public DateTime epochStart;

//    public double timestamp;

//    public double startTimestamp;

//    // public DataTable table;

//    // Use this for initialization
//    void Start()
//    {
//        // If a DataLogger instance has yet to be created
//        if (logger == null)
//        {
//            // Ensure this instance is preserved across scenes
//            DontDestroyOnLoad(gameObject);
//            // Set the single DataLogger instance to this one
//            logger = this;
//        }
//        // If a DataLogger instance exists and this is NOT it
//        else if (logger != this)
//        {
//            // Destroy this GameObject
//            Destroy(gameObject);
//            return;
//        }

//        // If the playerID has yet to be set
//        if (playerID == "")
//        {
//            // Set it to a new GUID
//            playerID = Guid.NewGuid().ToString();
//        }

//        entityData = new Dictionary<string, Dictionary<string, string>>();

//        epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
//        timestamp = (System.DateTime.UtcNow - epochStart).TotalSeconds;
//        startTimestamp = timestamp;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        timestamp = (System.DateTime.UtcNow - epochStart).TotalSeconds;
//    }

//    void OnApplicationQuit()
//    {
//        // If the player has begun a run
//        if (entityData.ContainsKey("run_begin" + runIndex.ToString()))
//        {
//            // Get the end run details
//            string runDetails = "RemainingWater:" + GameObject.Find("WaterCounter").GetComponent<WaterCounter>().GetRemainingWater() + "," +
//                                   "RemainingTurns:" + GameObject.Find("TurnCounter").GetComponent<TurnCounter>().GetRemainingTurns();
//            // End the run and save
//            EndRun(runDetails);
//            SaveData();
//        }

//        // If the player has begun a session
//        if (entityData.ContainsKey("session_begin" + sessionCount.ToString()))
//        {
//            // End the session and save
//            DataLogger.logger.TakeAction("Player Closed Game", "Player Closed Game");
//            EndSession("Player closed game");
//            SaveData();
//        }
//    }

//    public void StartSession()
//    {

//        StartSession(playerID, version, 1, "game_start", "local_file");
//    }

//    public void StartSession(string player_id, int version, int condition, string details, string logging_type)
//    {
//        currentSessionID = Guid.NewGuid().ToString();
//        runCount = 0;
//        sessionCount += 1;
//        Dictionary<string, string> entity = new Dictionary<string, string>();
//        entity.Add("session_id", currentSessionID);
//        entity.Add("player_id", player_id);
//        entity.Add("client_time", timestamp.ToString());
//        entity.Add("version", version.ToString());
//        entity.Add("condition", condition.ToString());
//        entity.Add("details", details);
//        entityData.Add("session_begin" + sessionCount.ToString(), entity);
//        SaveData();
//    }

//    public void EndSession(string details)
//    {
//        Dictionary<string, string> entity = new Dictionary<string, string>();
//        entity.Add("session_id", currentSessionID);
//        entity.Add("client_time", timestamp.ToString());
//        entity.Add("run_count", runCount.ToString());
//        entity.Add("details", details);
//        if (!entityData.ContainsKey("session_end" + sessionCount.ToString()))
//        {
//            entityData.Add("session_end" + sessionCount.ToString(), entity);
//        }
//        //DataLogger.logger.TakeAction ("Return to Main Menu", "Return to Main Menu");
//        SaveData();
//    }

//    public void StartRun(string definition, string details)
//    {
//        currentRunID = Guid.NewGuid().ToString();
//        runCount += 1;
//        runIndex += 1;
//        actionCount = 0;
//        Dictionary<string, string> entity = new Dictionary<string, string>();
//        entity.Add("run_id", currentRunID);
//        entity.Add("session_id", currentSessionID);
//        entity.Add("run_seqnum", runCount.ToString());
//        entity.Add("client_time", timestamp.ToString());
//        entity.Add("definition", definition);
//        entity.Add("details", details);
//        entityData.Add("run_begin" + runIndex.ToString(), entity);
//        SaveData();
//    }

//    public void EndRun(string details)
//    {
//        Dictionary<string, string> entity = new Dictionary<string, string>();
//        entity.Add("run_id", currentRunID);
//        entity.Add("client_time", timestamp.ToString());
//        entity.Add("action_count", actionCount.ToString());
//        entity.Add("details", details);
//        if (!entityData.ContainsKey("run_end" + runIndex.ToString()))
//        {
//            entityData.Add("run_end" + runIndex.ToString(), entity);
//        }
//        SaveData();
//    }

//    public void TakeAction(string type, string details)
//    {
//        //table.TakeAction(type, details);
//        actionCount += 1;
//        actionIndex += 1;
//        Dictionary<string, string> entity = new Dictionary<string, string>();
//        entity.Add("type", type);
//        entity.Add("run_id", currentRunID);
//        entity.Add("action_seqnum", actionCount.ToString());
//        entity.Add("client_time", timestamp.ToString());
//        entity.Add("details", details);
//        entityData.Add("action" + actionIndex.ToString(), entity);
//        SaveData();
//    }

//    // Saves the logged information to a JSON file
//    public void SaveData()
//    {
//        //#if UNITY_EDITOR

//        //#endif
//        SaveLocally();
//        StartCoroutine(SaveToServer());
//    }

//    // Saves the logged information to a JSON file on the server
//    // The file is saved in the "/usr/share/nginx/html/secphp/analytics/json/grace/" directory
//    IEnumerator SaveToServer()
//    {
//        string serialized = WWW.EscapeURL(JsonConvert.SerializeObject(entityData, Formatting.Indented));
//        urlFull = urlBase + "&json=" + serialized + "&file=grace/player_data_" + playerID + ".json";

//        WWWForm form = new WWWForm();
//        form.AddField("data", serialized);

//        WWW request = new WWW(urlFull, form);

//        yield return request;


//        // Print the error to the console
//        if (request.error != null)
//        {
//            //	    Debug.Log("Request error: " + request.error);
//        }
//        else
//        {
//            //	    Debug.Log("Request success: " + request.data);
//        }
//    }

//    // Saves the logged information to a JSON file locally
//    // The file is saved in the "/Assets/StreamingAssets/PlayData/" directory
//    public void SaveLocally()
//    {
//        string serialized = JsonConvert.SerializeObject(entityData, Formatting.Indented);
//        File.WriteAllText(System.IO.Path.Combine(Application.streamingAssetsPath + "/PlayData", "player_data_" + playerID) + ".json", serialized);

//        // Refresh the editor to reflect the change and show the new file
//#if UNITY_EDITOR
//        UnityEditor.AssetDatabase.Refresh();
//#endif
//    }
//}