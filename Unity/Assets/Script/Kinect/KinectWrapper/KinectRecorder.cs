using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using Kinect;
using UnityEngine.UI;
using System;

public class KinectRecorder : MonoBehaviour {
	
	public DeviceOrEmulator devOrEmu;
	private KinectInterface kinect;
	
	public string outputFile = "Assets/Kinect/Recordings/";
	
	
	private bool isRecording = false;
	private ArrayList currentData = new ArrayList();
	

	//add by lxjk
	static public int fileCount = 0;
	//end lxjk

    public GameObject confirmationWindow = null;
    public GameObject textArea = null;
    public GameObject submit = null;
    public GameObject cancel = null;

    /// <summary>
    /// Database connection class 
    /// </summary>
    public GlobalFunctions db;
	// Use this for initialization
	void Start () {
		kinect = devOrEmu.getKinect();
	}
	
	// Update is called once per frame
	void Update () {
		if(!isRecording){
			if(Input.GetKeyDown(KeyCode.F10)){
				StartRecord();
			}
		} else {
			if(Input.GetKeyDown(KeyCode.F10)){
				StopRecord();
			}
			if (kinect.pollSkeleton()){
				currentData.Add(kinect.getSkeleton());
			}
		}
	}
    public void toggleRecord()
    {
        if (isRecording)
            StopRecord();
        else
            StartRecord();
    }
	void StartRecord() {
		isRecording = true;
		Debug.Log("start recording");
	}

    public void CheckInput()
    {
        String filename = textArea.gameObject.GetComponent<Text>().text;
        if(!filename.Equals(""))
        {
            string filePath = outputFile + filename;
            FileStream output = new FileStream(@filePath, FileMode.Create);

            BinaryFormatter bf = new BinaryFormatter();

            SerialSkeletonFrame[] data = new SerialSkeletonFrame[currentData.Count];
            for (int ii = 0; ii < currentData.Count; ii++)
            {
                data[ii] = new SerialSkeletonFrame((NuiSkeletonFrame)currentData[ii]);
            }
            bf.Serialize(output, data);
            output.Close();
            db.CreateSign(filePath, filename);
            Debug.Log("stop recording\nFile saved at: " + filePath);
            
            HideWindow();
        }

    }

    public void HideWindow()
    {
        // close file name window
        confirmationWindow.gameObject.active = false;
    }
	void StopRecord() {

        // stop recording
		isRecording = false;
        // open file name window
        confirmationWindow.gameObject.active = true;

	}
}
