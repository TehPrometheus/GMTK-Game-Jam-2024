using UnityEngine;

public class AdenoProngRotator : MonoBehaviour
{
    public bool isEnabled = true;
    public Transform prongTransform;
    // Update is called once per frame
    void Update()
    {
        if (isEnabled)
            prongTransform.RotateAround(transform.position, Vector3.back, 1f);
    }
}
