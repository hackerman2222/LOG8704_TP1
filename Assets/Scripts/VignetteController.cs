using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Oculus.Interaction.Locomotion;

public class VignetteController : MonoBehaviour
{
    public Volume volume;
    private Vignette vignette;

    public Oculus.Interaction.Locomotion.CharacterController character;
    public float maxIntensity = 0.45f;
    public float fadeSpeed = 5f;

    private Vector3 lastPos;

    void Start()
    {
        if (volume.profile.TryGet(out Vignette v))
        {
            vignette = v;
            vignette.intensity.Override(0f);
        }

        if (character != null)
            lastPos = character.transform.position;
    }

    void Update()
    {
        if (vignette == null || character == null) return;

        Vector3 currentPos = character.transform.position;
        float speed = (currentPos - lastPos).magnitude / Time.deltaTime;
        lastPos = currentPos;

        Debug.Log($"Speed: {speed}");

        float target = speed > 0.05f ? maxIntensity : 0f;

        vignette.intensity.value = Mathf.Lerp(
            vignette.intensity.value,
            target,
            Time.deltaTime * fadeSpeed
        );
    }
}
