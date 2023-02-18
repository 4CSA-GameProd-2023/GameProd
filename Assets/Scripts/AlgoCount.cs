using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class AlgoCount : MonoBehaviour
{

    public GameObject[] points;
    public float Distance;
    public float TotalDistanceInMeters;
    private LineRenderer line;
    
    public string fileName;

    // Start is called before the first frame update
    void Start()
    { line = GetComponent<LineRenderer>();
        
        fileName = Application.persistentDataPath + "/data2.csv";
    }
    

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.V)){
            RenderLine();
        }else if(Input.GetKeyDown(KeyCode.B)){
            CalculateDistance();
        }else if(Input.GetKeyDown(KeyCode.G)){
            DownloadDistance();
        }
            
    }

    public void RenderLine(){
        int num_of_path = points.Length;
        line.positionCount = num_of_path+1;

        for(int x = 0; x < num_of_path; x++){
            line.SetPosition(x, new Vector3(points[x].transform.position.x,0,points[x].transform.position.z));
        }

        line.SetPosition(num_of_path, line.GetPosition(1));

        
        line.startWidth = 1f;
        line.endWidth = 1f;
    }
    
    public void CalculateDistance(){
        for(int i = 0; i < points.Length-1; i++){
            if(i == points.Length - 1){
                return;
            } else{
                Debug.Log("Adding" + points[i].name + " | " + points[i + 1].name);  
                
                Debug.Log(i);
                float x1 = points[i].transform.position.x;
                float y1 = points[i].transform.position.z;

                float x2 = points[i+1].transform.position.x;
                float y2 = points[i+1].transform.position.z;

                float xdifference = x2 - x1;
                float ydifference = y2 - y1;

                float xsquared = xdifference *xdifference;
                float ysquared = ydifference * ydifference;

                float xydifference = xsquared+ysquared;

                Distance = Mathf.Sqrt(xydifference);
                Debug.Log("Distance: " + Distance);
                TotalDistanceInMeters = TotalDistanceInMeters + Distance;
            }
        }
    }
    public void DownloadDistance(){
         TextWriter tw = new StreamWriter(fileName,false);
        tw.WriteLine("Status, x1, y1, x2, y2,x2-x1,y2-y1,x^2,y^2,sum of X and Y, Distance Between,Total Length");

         for(int i = 0; i < points.Length-1; i++){
            int next = i + 1;
            if(i == points.Length - 1){
                return;
            } else{
                Debug.Log("Adding" + points[i].name + " | " + points[i + 1].name);  
                
                Debug.Log(i);
                float x1 = points[i].transform.position.x;
                float y1 = points[i].transform.position.z;

                float x2 = points[i+1].transform.position.x;
                float y2 = points[i+1].transform.position.z;

                float xdifference = x2 - x1;
                float ydifference = y2 - y1;

                float xsquared = xdifference *xdifference;
                float ysquared = ydifference * ydifference;

                float xysum = xsquared+ysquared;

                Distance = Mathf.Sqrt(xysum);
                Debug.Log("Distance: " + Distance);
                TotalDistanceInMeters = TotalDistanceInMeters + Distance;
                tw.WriteLine("From point " + i +" To Point " + next+","
                            + x1 +","
                            + y1 +","
                            + x2 +","
                            + y2 +","
                            + xdifference +","
                            + ydifference +","
                            + xsquared +","
                            + ysquared +","
                            + xysum +","
                            + Distance +","
                            + TotalDistanceInMeters);
            }
        }
        
        tw.Close();
    }
}
