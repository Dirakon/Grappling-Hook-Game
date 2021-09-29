using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Character : MonoBehaviour
{
    // Start is called before the first frame update
    public float currentRotatingDistance;
    [SerializeField]
    private float minRotatingDistance;
    private float maxRotatingDistance;
    [SerializeField]
    private HookVisuals hook;
    [SerializeField]
    private Rigidbody2D rigidbody2D;
    [SerializeField]
    private LayerMask layersForHook;
    [SerializeField]
    private float speed = 0, rotationSpeed = 0, zoomSpeed = 0;
    [SerializeField]
    private float hookDistance = 0;
    public Transform objectToRotateAround = null;

    void Start()
    {

    }
    public void ZoomChange(float delta){
        currentRotatingDistance = Mathf.Clamp(currentRotatingDistance+ delta*zoomSpeed*Time.deltaTime,minRotatingDistance,maxRotatingDistance);

    }
    void Awake()
    {
        hook.SetRenderState(false);
        maxRotatingDistance = hookDistance;
    }
    public void RealeaseHook(){
        objectToRotateAround = null;
        hook.SetRenderState(false);
    }
    public bool ThrowHook(Vector2 coords)
    {
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, coords - (Vector2)transform.position, hookDistance, layersForHook);
        Debug.DrawRay(transform.position, coords - (Vector2)transform.position, Color.red, 0.2f);
        if (raycast.collider == null)
        {
            return false;
        }
        Obstacle obstacle = raycast.collider.GetComponent<Obstacle>();
        if (obstacle == null || !obstacle.isGrabbable){
            return false;
        }
        hook.SetRenderState(true);
        objectToRotateAround = raycast.collider.transform;
        hook.UpdateEndPosition(objectToRotateAround.position);
        currentRotatingDistance = (transform.position - raycast.collider.transform.position).magnitude;
        return true;
    }

    void SetNewRight(Vector2 newRight)
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.Cross(Vector3.forward, newRight));
    }

    void TakeGravity()
    {
        SetNewRight(Vector2.MoveTowards(transform.right, Vector2.down, Time.deltaTime * rotationSpeed).normalized);
    }

    void OnDrawGizmos()
    {

        Debug.DrawRay(transform.position, transform.right * speed, Color.cyan, 0.1f);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, hookDistance);
    }

    void RotateToBeATangentOf(Vector3 centerOfCircle)
    {
        centerOfCircle.z = transform.position.z;
        transform.position = centerOfCircle + (transform.position - centerOfCircle).normalized * currentRotatingDistance;
        Vector3 newRightUnderQuestion = Vector3.Cross(Vector3.back, transform.position - centerOfCircle);
        newRightUnderQuestion.z = 0;
        newRightUnderQuestion.Normalize();
        var angle1 = Mathf.Abs(Vector3.Angle(newRightUnderQuestion, transform.right));
        var angle2 = Mathf.Abs(Vector3.Angle(-newRightUnderQuestion, transform.right));
        SetNewRight(angle1 < angle2 ? newRightUnderQuestion : -newRightUnderQuestion);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        TakeGravity();
        if (objectToRotateAround != null)
        {
            RotateToBeATangentOf(objectToRotateAround.position);
            hook.UpdateEndPosition(objectToRotateAround.position);
        }
        rigidbody2D.velocity = transform.right * speed;
    }
}
