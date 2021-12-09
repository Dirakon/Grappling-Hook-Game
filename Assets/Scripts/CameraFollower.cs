using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    private const int VISIBILITY_MODIFIER_FOR_ROTATING_AROUND_OBJECTS = 2;
    private float standartSize;
    [SerializeField]
    private Character whoToFollow;
    [SerializeField] private float speed, zoomSpeed;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float desiredSize = 5f;
    [SerializeField] private new Camera camera;
    void initCharacter(Character character)
    {

        if (whoToFollow == null)
        {
            whoToFollow = character;
            transform.position = whoToFollow.transform.position + offset;
        }
    }

    void Awake()
    {
        standartSize = desiredSize;
        float aspectRatio = GetNormalizedAspectRatio();
        Camera.main.orthographicSize = standartSize / aspectRatio;
    }
    void Start()
    {
        GameMaster.singleton.onCharacterChosen += initCharacter;
    }
    void Update()
    {
        if (whoToFollow == null)
            return;
        MoveCloserToObjectBeingFollowed();
        ManageZooming();
    }

    private void ManageZooming()
    {
        float aspectRatio = GetNormalizedAspectRatio();
        float currentSize = camera.orthographicSize * aspectRatio;

        desiredSize = GetDesiredOrthographicSize();
        currentSize = MoveOrthographicSizeTowardsDesiredSize(currentSize);

        Camera.main.orthographicSize = currentSize / aspectRatio;
    }

    private float MoveOrthographicSizeTowardsDesiredSize(float currentSize)
    {
        float initialSideForApproachingDesiredSizeFrom = Mathf.Sign(desiredSize - currentSize);
        currentSize += Time.deltaTime * zoomSpeed * initialSideForApproachingDesiredSizeFrom;
        float newSideForApproachingDesiredSizeFrom = Mathf.Sign(desiredSize - currentSize);
        if (newSideForApproachingDesiredSizeFrom != initialSideForApproachingDesiredSizeFrom)
        {
            // Overshot a bit
            currentSize = desiredSize;
        }

        return currentSize;
    }

    private float GetDesiredOrthographicSize()
    {
        return whoToFollow.IsRotatingAroundSomeObject() ?
                    Mathf.Max(standartSize, whoToFollow.currentRotatingDistance * VISIBILITY_MODIFIER_FOR_ROTATING_AROUND_OBJECTS)
                    : standartSize;
    }

    private void MoveCloserToObjectBeingFollowed()
    {
        Transform objectToFocusOn = whoToFollow.IsRotatingAroundSomeObject() ?
                    whoToFollow.transform
                    : whoToFollow.objectToRotateAround.transform;
        Vector3 desiredPosition = objectToFocusOn.position + offset;
        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * speed);
    }

    private static float GetNormalizedAspectRatio()
    {
        float aspectRatio = Screen.width / (float)Screen.height;
        if (aspectRatio > 1)
            aspectRatio = 1 / aspectRatio;
        return aspectRatio;
    }
}
