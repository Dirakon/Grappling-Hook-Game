using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookVisuals : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private MeshRenderer meshRenderer;
    [SerializeField]
    private AudioSource hookSounds;
    public void SetRenderState(bool toWhat, Material[] colors= null){
        meshRenderer.enabled = toWhat;
        if (meshRenderer.enabled){
            hookSounds.Play();
            meshRenderer.materials=new Material[]{colors[0],colors[1]};
        }else{
            hookSounds.Stop();
        }
    }
    public void UpdateEndPosition(Vector3 newEndPosition){
        newEndPosition.z = transform.position.z;
        Vector3 difference = (newEndPosition - transform.position);
        float differenceMagnitude = difference.magnitude;
        transform.localScale = new Vector3(1,differenceMagnitude,1);
        difference.Normalize();
        transform.rotation = Quaternion.LookRotation(Vector3.back,difference);
        if ((transform.position + transform.up -newEndPosition).magnitude > differenceMagnitude){
            // Wrong direction
            transform.rotation = Quaternion.LookRotation(Vector3.forward,difference);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
