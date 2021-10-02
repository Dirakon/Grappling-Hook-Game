using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SpawnPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public IEnumerator Spawn(GameObject characterToSpawn)
    {
        Character character = Instantiate(characterToSpawn,transform.position+Vector3.up*startingDistance,Quaternion.identity).GetComponent<Character>();
        Player player = character.gameObject.GetComponent<Player>();
        Obstacle obstacle = gameObject.GetComponent<Obstacle>();
        Camera.main.gameObject.GetComponent<CameraFollower>().MakeFollowX(character);
        
        while (!character.ThrowHook(obstacle)){
            yield return new WaitForEndOfFrame();
        }
        while (character.currentRotatingDistance <= desiredDistance && character.objectToRotateAround == obstacle){
            character.ZoomChange(Mathf.Clamp(Time.fixedTime * spawnSpeed,-1.5f,1.5f));
            yield return new WaitForEndOfFrame();
        }
    }
    // Update is called once per frame
    void Update()
    {

    }

    [SerializeField] private float desiredDistance, spawnSpeed, startingDistance;
    void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, desiredDistance);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, startingDistance);
    }

}
