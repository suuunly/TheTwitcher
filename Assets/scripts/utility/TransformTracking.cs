using UnityEngine;

public class TransformTracking : MonoBehaviour
{
    public Transform Target;
    public float Speed;

    private Vector3 _offset;

    private void Start()
    {
        this._offset = transform.position - Target.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 pos = transform.position;
        Vector3 target = Target.position + this._offset;
        transform.position = target;//Vector3.Lerp(pos, target, Speed * Time.deltaTime);
    }
}
