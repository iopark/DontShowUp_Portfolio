using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    //Pertaining to Path Managing 
    Queue<PathRequest> pathRequests = new Queue<PathRequest>();
    PathRequest currentPath;
    PathDefiner pathDefiner; 
    private bool isProcessing; 
    public bool IsProcessing
    {
        get { return isProcessing; }
    }

    public void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callback)
    {
        PathRequest newRequest = new PathRequest(pathStart, pathEnd, callback);
        pathRequests.Enqueue(newRequest);
        TryProcessNext();
    }

    void TryProcessNext()
    {
        if (!isProcessing && pathRequests.Count > 0)
        {
            currentPath = pathRequests.Dequeue();
            isProcessing = true;
            pathDefiner.StartDefiningPath(currentPath.pathStart, currentPath.pathEnd);
        }
    }

    public void FinishedProcessingPath(Vector3[] path, bool success)
    {
        if (success) // path 를 찾았을때만 해당 Path 를 전달한다. 
            currentPath.callback(path, success);
        //How do we Deliever this path to the Requestee? 

        isProcessing = false;
        TryProcessNext();
    }

    struct PathRequest
    {
        public Vector3 pathStart;
        public Vector3 pathEnd;
        public Action<Vector3[], bool> callback;

        public PathRequest(Vector3 _start, Vector3 _end, Action<Vector3[], bool> _callback)
        {
            pathStart = _start;
            pathEnd = _end;
            callback = _callback;
        }
    }
}
