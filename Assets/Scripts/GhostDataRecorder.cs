using UnityEngine;

public class GhostDataRecorder : MonoBehaviour
{
    public GhostData ghostData = new GhostData();
    public bool recording;

    public void StartRecording()
    {
        recording = true;
    }
    private void FixedUpdate()
    {
        if (!recording)
        {
            return;
        }
        ghostData.AddFrame(transform.position, transform.rotation.eulerAngles);
    }
}
