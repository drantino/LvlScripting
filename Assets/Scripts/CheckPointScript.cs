using UnityEngine;

public class CheckPointScript : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position,0.5f);
    }
}
