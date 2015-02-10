using Mono.Data.SqliteClient;
using System;
using System.Data;
using UnityEngine;

public class GlobalFunctions : MonoBehaviour
{

    /// <summary>
    /// Database Connector 
    /// </summary>
    private IDbConnection dbcon;
    
    public IDbConnection DBConnection
    {
        get { return dbcon; }
        set { dbcon = value; }
    }

    /// <summary>
    /// Close the Database Connection 
    /// </summary>
    public void CloseDBConnection()
    {
        dbcon.Close();
        dbcon = null;
    }

    /// <summary>
    /// Open the Database Connection 
    /// </summary>
    public void OpenDBConnection()
    {
        //The location of database file
        string connectionString = "URI=file:Assets/Database/ISLDatabase";
        dbcon = (IDbConnection)new SqliteConnection(connectionString);
        dbcon.Open();
    }

    private void executeQuery(string query)
    {
        try
        {
            // Init database
            OpenDBConnection();
            //Database Query
            IDbCommand dbcmd = DBConnection.CreateCommand();
            dbcmd.CommandText = query;
            dbcmd.ExecuteNonQuery();
           
            // clean up 
            dbcmd.Dispose();
            dbcmd = null;
            CloseDBConnection();
        }
        catch(Exception e)
        {
            Debug.LogError("There was a problem executing the query.  \n\n\nError: " + e + "\n\n\nStack Trace: " + e.StackTrace);
        }
        
    }

    public string GetGestureLocation(string text)
    {
        try
        {
            // Init database
            OpenDBConnection();
            //Database Query
            IDbCommand dbcmd = DBConnection.CreateCommand();
            string query = "Select gestureVideoLoc from Gesture where gestureID = (" +
                                "Select gestureID from GestureHasText where textID = " +
                                "(Select textID from Text where text = '" + text + "')" +
                           ");";
            dbcmd.CommandText = query;
            IDataReader reader = dbcmd.ExecuteReader();
            String gesturePath = "";
            while (reader.Read())
            {
                // Get the image path 
                gesturePath = reader.GetString(0);
                //Debug.Log(imagePath);
            }
            // clean up 
            dbcmd.Dispose();
            dbcmd = null;
            CloseDBConnection();
            return gesturePath;
        }
        catch (Exception e)
        {
            Debug.LogError("There was a problem executing the query.  \n\n\nError: " + e + "\n\n\nStack Trace: " + e.StackTrace);
            return null;
        }
        return null;
    }
    public void CreateSign(string gestureLoc, string textTranslation)
    {
        
        try
        {
            
            //Database Query
           // string query = 
           executeQuery("Insert into Gesture(gestureVideoLoc) Values('"+gestureLoc+"');");
           executeQuery("Insert into Text(text) Values('"+textTranslation+"');");
           executeQuery("Insert into GestureHasText(gestureID, textID) Values ((Select gestureID from Gesture where gestureVideoLoc = '" + gestureLoc + "'),(Select textID from Text where text = '" + textTranslation + "'));");
    

            //dbcmd.CommandText = query;
           // IDataReader reader = dbcmd.ExecuteReader();
            //string imagePath = "";
            //while (reader.Read())
            //{
            //    // Get the image path 
            //    imagePath = reader.GetString(0);
            //    //Debug.Log(imagePath);
            //}
            //StartCoroutine(LoadFile(imagePath));
            // clean up 
            //reader.Close();
            //reader = null;
            
        }
        catch (Exception e)
        {
            Debug.LogError("There was a problem inserting the sign into the database. \nError: " + e + "\nStack Trace: "+ e.StackTrace);
        }
        
    }
}