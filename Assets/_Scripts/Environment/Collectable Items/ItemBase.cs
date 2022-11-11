using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    [SerializeField] private AudioClip _sound;
    private void Start()
    {
        GetComponent<Collider>();
    }
    void Update()
    {
        transform.localRotation = Quaternion.Euler(0, Time.time * 100f, 0);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioSystem.Instance.PlaySound(_sound);
            Destroy(gameObject);
        }
    }
}