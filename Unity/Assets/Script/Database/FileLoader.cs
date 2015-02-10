using System;
using System.Collections;
using System.Data;
using UnityEngine;
using System.Collections.Generic;

public class FileLoader : MonoBehaviour
{
    /// <summary>
    /// Database connection class 
    /// </summary>
    public GlobalFunctions db;

    /// <summary>
    /// The image to loaded from db 
    /// </summary>
    public Texture2D image;

    // Create your list variable using this construct:
    Dictionary<int, String> textList = new Dictionary<int, String>();
    public Component[] textPressedList;
    private GameObject t;
    public KinectEmulator KinectEmu;

    void Start()
    {
        t = GameObject.Find("TextList");
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            getSelectedText();
        }
    }

    /// <summary>
    /// Fetches an image file location from the database and calls the LoadFile method. 
    /// </summary>
    /// TODO: Add functionality for selecting which image path to load currently only loads the
    ///       image that was last found
    private void getImageFromDB()
    {
        try
        {

            //Database Query
            string query = "SELECT gestureImageRight " + "FROM Gesture";
            IDbCommand dbcmd = db.DBConnection.CreateCommand();
            dbcmd.CommandText = query;
            IDataReader reader = dbcmd.ExecuteReader();
            String imagePath = "";
            while (reader.Read())
            {
                // Get the image path 
                imagePath = reader.GetString(0);
                //Debug.Log(imagePath);
            }

            //StartCoroutine(LoadFile(imagePath));
            // clean up 
            reader.Close();
            reader = null;

        }
        catch (Exception e)
        {
            Debug.LogError("There was a problem loading the image from the database. \nError: " + e);
        }
    }
    private void getSelectedText()
    {

        textPressedList = t.GetComponentsInChildren<InstantGuiElement>();
        foreach (InstantGuiElement txt in textPressedList)
        {
            if (txt.pressed)
                KinectEmu.LoadPlaybackFile(db.GetGestureLocation(txt.text));
                
        }
    }
    /// <summary>
    /// Fetches text from the database.
    /// </summary>
    private void getTextFromDB()
    {
        try
        {

            //Database Query
            string query = "SELECT textID, text " + "FROM Text";
            IDbCommand dbcmd = db.DBConnection.CreateCommand();
            dbcmd.CommandText = query;
            IDataReader reader = dbcmd.ExecuteReader();

            while (reader.Read())
            {
                // Get the image path 
                textList.Add(reader.GetInt32(0), reader.GetString(1));
                //Debug.Log(imagePath);
            }
            /*
            // Print all elements in the list
            foreach (String letter in textList)
            {
                Debug.Log(letter);
            }
             */

            // Copy database elements to gui list
            t = GameObject.Find("TextList");
            List<String> li = new List<String>(textList.Values);
            li.Sort();
            t.GetComponent<InstantGuiList>().labels = li.ToArray();
            // clean up 
            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;

        }
        catch (Exception e)
        {
            Debug.LogError("There was a problem loading the image from the database. \nError: " + e);
        }
    }

    /// <summary>
    /// Loads an image into a Texture2D type 
    /// </summary>
    /// <param name="fileLocation"> The file location in the assets folder </param>
    /// <returns></returns>
    private IEnumerator LoadFile(String fileLocation)
    {

        if (fileLocation != null)
        {
            if (fileLocation.Equals(""))
            {
                Debug.LogWarning("File location is empty.");
                yield return null;
            }
            // location of the asset folder on drive 
            string assetFolderLocation = @"file:///" + Application.dataPath;
            // create the full file path 
            string fullFilePath = assetFolderLocation + fileLocation;
            WWW www = new WWW(fullFilePath);
            yield return www;
            // create the image 
            Texture2D texTmp = new Texture2D(1024, 1024, TextureFormat.DXT5, false);
            //compresses JPGs by DXT1 and PNGs by DXT5
            www.LoadImageIntoTexture(texTmp);
            // apply our image 
            image = texTmp;
        }

        else
        {
            Debug.LogError("There was a problem loading the image from the database. The File Location returned null.");
        }

    }

    // Use this for initialization 
    private void Awake()
    {

        // Init database
        db.OpenDBConnection();


        // Functions
        getImageFromDB();
        getTextFromDB();


        db.CloseDBConnection();

    }

}