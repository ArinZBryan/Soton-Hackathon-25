using UnityEngine;
using UnityEngine.Rendering;
public class Task : MonoBehaviour
{
    public Vector3 position;
    public float duration;
    public bool willInfect;

    private void Start()
    {
        position = transform.position;
    }
}