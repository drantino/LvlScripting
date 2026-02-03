using System;
using UnityEngine;

public class CircleOverlap : MonoBehaviour
{
    public float radius;
    public string tagToCheck;
    public Color setColor;

    public event Action<Vector2> OnOverLap;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    public bool CircleOverLapCheck()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject.CompareTag(tagToCheck))
            {
                OnOverLap?.Invoke(hit.gameObject.transform.position);
                return true;
            }
        }
        return false;
    }
    private void OnDrawGizmos()
    {
        CustomDebug.DrawDebugCircle(transform.position, radius, setColor, 50);
    }
}
