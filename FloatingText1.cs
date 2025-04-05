using UnityEngine;

public class FloatingText1 : MonoBehaviour
{
    public float destroyTime = .2f;
    public Vector3 offset = new Vector3(0, 2, 0);

    void Awake()
    {
        Destroy(gameObject, destroyTime);
        transform.localPosition += offset;
    }
}