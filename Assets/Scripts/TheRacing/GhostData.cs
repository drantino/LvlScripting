using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GhostData
{
    public List<GhostDataFrame> ghostDataFrames = new List<GhostDataFrame>();

    public void AddFrame(Vector3 position, Vector3 rotation)
    {
        ghostDataFrames.Add(new GhostDataFrame(position, rotation));
    }
}
[Serializable]
public class GhostDataFrame
{
    public Vector3 Position;
    public Vector3 Rotation;
    public GhostDataFrame(Vector3 _position, Vector3 _rotation)
    {
        Position = _position;
        Rotation = _rotation;
    }
}

