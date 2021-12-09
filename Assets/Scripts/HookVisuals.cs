using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookVisuals : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private AudioSource hookSounds;
    [SerializeField] private float speed;
    IEnumerator HookAnimation(Material[] materials)
    {
        float t = 0;
        meshRenderer.enabled = true;
        meshRenderer.materials = materials;
        while (true)
        {
            t += Time.deltaTime * speed / scaleGoal;
            if (t > 1)
                t = 1;
            transform.localScale = new Vector3(1, t * scaleGoal, 1);
            yield return null;
        }
    }
    public void SetRenderState(bool toWhat, Material[] colors = null)
    {
        if (toWhat)
        {
            StartCoroutine(HookAnimation(new Material[] { colors[0], colors[1] }));
            hookSounds.Play();
        }
        else
        {
            meshRenderer.enabled = false;
            StopAllCoroutines();
            hookSounds.Stop();
        }
    }
    float scaleGoal;
    public void UpdateEndPosition(Vector3 newEndPosition)
    {

        newEndPosition.z = transform.position.z;
        Vector3 difference = (newEndPosition - transform.position);
        float differenceMagnitude = difference.magnitude;
        scaleGoal = differenceMagnitude;
        difference.Normalize();
        transform.rotation = Quaternion.LookRotation(Vector3.back, difference);
        if ((transform.position + transform.up - newEndPosition).magnitude > differenceMagnitude)
        {
            // Wrong direction
            transform.rotation = Quaternion.LookRotation(Vector3.forward, difference);
        }
    }
}
