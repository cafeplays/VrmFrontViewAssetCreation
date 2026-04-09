using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
[RequireComponent(typeof(AudioSource))]
public class ParticleCollisionSound : MonoBehaviour
{
    private AudioSource _audioSource;
    public AudioClip collisionSound;

    [Tooltip("Minimum time between sound plays in seconds to avoid audio overlapping too heavily.")]
    public float timeThreshold = 0.05f;
    
    private float _lastPlayTime;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogWarning("No AudioSource found on " + gameObject.name);
        }
    }

    void OnParticleCollision(GameObject other)
    {
        if (collisionSound != null && _audioSource != null && Time.time - _lastPlayTime >= timeThreshold)
        {
            _audioSource.PlayOneShot(collisionSound);
            _lastPlayTime = Time.time;
        }
    }
}
