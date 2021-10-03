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
    private float speed = 0, rotationSpeed = 0,gravityStrenght=0, zoomSpeed = 0;
    [SerializeField]
    private float hookDistance = 0;
    [SerializeField]
    private GameObject deathEffect;
    public Obstacle objectToRotateAround = null;

    void Start()
    {

    }
    public void Die(){
            Instantiate(deathEffect,transform.position,Quaternion.identity);
            GameMaster.singleton.LevelRestart(0.5f);
            Destroy(gameObject);
    }
    public void ZoomChange(float delta)
    {
        if (delta > 0 && !objectToRotateAround.allowsZoomingOut)
            return;
        if (delta < 0 && !objectToRotateAround.allowsZoomingIn)
            return;
        currentRotatingDistance = Mathf.Clamp(currentRotatingDistance + delta * zoomSpeed * Time.deltaTime, minRotatingDistance, maxRotatingDistance);

    }
    void Awake()
    {
        hook.SetRenderState(false);
        StartCoroutine(autoRotate());
        maxRotatingDistance = hookDistance;
    }
    public void RealeaseHook()
    {
        objectToRotateAround = null;
        hook.SetRenderState(false);
    }
    public bool ThrowHook(Obstacle obstacle)
    {
        if (obstacle == null || !obstacle.isGrabbable)
        {
            return false;
        }
        Vector3 obstaclePosition = obstacle.transform.position;
        obstaclePosition.z=transform.position.z;
        float saveCur = currentRotatingDistance;
        currentRotatingDistance = (transform.position - obstaclePosition).magnitude;
        if (currentRotatingDistance > maxRotatingDistance){
            currentRotatingDistance=saveCur;
            return false;
        }
        Material[] obstacleMaterials = obstacle.GetComponentInChildren<MeshRenderer>().materials;
        
        if (obstacleMaterials.Length == 4){
            // 2 colors
            obstacleMaterials = new Material[]{obstacleMaterials[0],obstacleMaterials[3]};
        }
        hook.SetRenderState(true,obstacleMaterials);
        objectToRotateAround = obstacle;
        hook.UpdateEndPosition(objectToRotateAround.transform.position);
        return CheckEyeContact();
    }
    Quaternion rightGoal = Quaternion.identity;
    IEnumerator autoRotate(){
        while (true){
            transform.rotation = Quaternion.RotateTowards(transform.rotation,rightGoal,Time.deltaTime*rotationSpeed);
            yield return new WaitForFixedUpdate();
        }
    }
    void SetNewRight(Vector2 newRight)
    {
        rightGoal = Quaternion.LookRotation(Vector3.forward, Vector3.Cross(Vector3.forward, newRight));
    }
    float curGravity = 0f;
    public float maxGravity;
    void TakeGravity()
    {
        curGravity += gravityStrenght;
        if (curGravity > maxGravity)
            curGravity=maxGravity;
        SetNewRight(Vector2.MoveTowards(rightGoal*Vector3.right, Vector2.down, Time.deltaTime * curGravity).normalized);
    }
    void RemoveGravity(){
        curGravity=0f;
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
        Vector3 prevPosition = transform.position;
        transform.position = centerOfCircle + (transform.position - centerOfCircle).normalized * currentRotatingDistance;
        Vector3 newRightUnderQuestion = Vector3.Cross(Vector3.back, transform.position - centerOfCircle);
        newRightUnderQuestion.z = 0;
        newRightUnderQuestion.Normalize();
        var angle1 = Mathf.Abs(Vector3.Angle(newRightUnderQuestion, transform.right));
        var angle2 = Mathf.Abs(Vector3.Angle(-newRightUnderQuestion, transform.right));
        SetNewRight(angle1 < angle2 ? newRightUnderQuestion : -newRightUnderQuestion);
    }

    bool CheckEyeContact()
    {
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, (Vector2)objectToRotateAround.transform.position - (Vector2)transform.position,
        hookDistance , layersForHook);
        if (raycast.collider == null || raycast.collider.gameObject != objectToRotateAround.gameObject)
        {
            RealeaseHook();
            return false;
        }
        return true;
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    void FixedUpdate()
    {
        if (objectToRotateAround != null)
        {
            RemoveGravity();
            if (!CheckEyeContact())
                return;
            RotateToBeATangentOf(objectToRotateAround.transform.position);
            hook.UpdateEndPosition(objectToRotateAround.transform.position);
        }else{
            TakeGravity();
        }
        rigidbody2D.velocity = transform.right * speed;
    }
}
