using UnityEngine;

public class GravityFlip : MonoBehaviour
{
    [Header("Hologram Setup")]
    public GameObject hologram;


    private Transform player;
    private CharacterController controller;
    private Vector3 playerVelocity = Vector3.zero;
    private float gravityMultiplier = 1f;
    private bool isFlipping = false;

    void Start()
    {
        player = this.transform;
        controller = player.GetComponent<CharacterController>();
        hologram.SetActive(false);
    }

    void Update()
    {
        HandleHologramActivation();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            ApplyGravityAndRotation();
        }

        ApplyGravity();
        controller.Move(playerVelocity * Time.deltaTime);
    }

    void HandleHologramActivation()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            ActivateHologram(Quaternion.Euler(0, 0, 90));
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            ActivateHologram(Quaternion.Euler(0, 0, -90));
        else if (Input.GetKeyDown(KeyCode.UpArrow))
            ActivateHologram(Quaternion.Euler(90, 0, 0));
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            ActivateHologram(Quaternion.Euler(-90, 0, 0));
    }

    void ActivateHologram(Quaternion rotation)
    {
        hologram.SetActive(true);
        hologram.transform.rotation = rotation;
    }

    void ApplyGravityAndRotation()
    {
        if (!hologram.activeSelf) return;

        isFlipping = true;
        player.rotation = hologram.transform.rotation;
        hologram.SetActive(false);
    }

    void ApplyGravity()
    {
        if (!isFlipping) return;

        Vector3 localGravity = Vector3.down * 9.81f * gravityMultiplier;

        Vector3 rotatedGravity = player.rotation * localGravity;
        playerVelocity += rotatedGravity * Time.deltaTime;
    }
}
