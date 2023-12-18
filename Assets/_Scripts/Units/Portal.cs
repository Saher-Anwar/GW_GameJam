using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField]
    private Scene linkedScene;
    [SerializeField]
    private Vector3 portalDetectionSize = new(5, 5, 10);

    public event Action<string> OnPortalEntered;
    public event Action<string> OnPortalExited;

    private new BoxCollider collider;
    private static int portalId = -1;

    private void Awake()
    {
        collider = GetComponent<BoxCollider>();
        portalId++;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (linkedScene == null) Debug.LogWarning($"Portal with ID {portalId} is not linked with another scene!");

        collider.size = portalDetectionSize;
    }

    private void OnTriggerEnter(Collider other)
    {
        // TODO: check for player script
        OnPortalEntered?.Invoke($"Portal {portalId} entered");
        SceneManager.LoadScene(linkedScene.buildIndex);
    }

    private void OnTriggerExit(Collider other)
    {
        // TODO: check for player script
        OnPortalExited?.Invoke($"Portal {portalId} exited");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, portalDetectionSize);
    }
}
