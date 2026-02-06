using UnityEngine;

public class VehicleCheckPointTracker : MonoBehaviour
{
    public CheckPointControllerScript CheckPointController;
    public int currentCheckPoint;
    public RaceControllerScript raceController;


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("CheckPoint") && currentCheckPoint < CheckPointController.checkPoints.Count && other.gameObject == CheckPointController.checkPoints[currentCheckPoint])
        {
            currentCheckPoint++;
            
        }
        if(other.CompareTag("FinishLine") && currentCheckPoint >= CheckPointController.checkPoints.Count)
        {
            raceController.RaceFinish();
        }
    }
}
