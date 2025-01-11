using UnityEngine;
using Mirror;

public class PlayerMove : NetworkBehaviour
{
    public float moveSpeed = 5f;

    [SyncVar(hook = nameof(OnColorChanged))]
    public Color playerColor;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        OnColorChanged(Color.white, playerColor);
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        playerColor = GetColorByID(connectionToClient.connectionId + 1);
    }

    private Color GetColorByID(int id)
    {
        Color[] colors = { Color.red, Color.green, Color.blue, Color.yellow, Color.magenta, Color.cyan };
        Color selectedColor = colors[(id - 1) % colors.Length];
        return new Color(selectedColor.r, selectedColor.g, selectedColor.b, 1f); // Ensure alpha is 1
    }

    private void OnColorChanged(Color oldColor, Color newColor)
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        if (spriteRenderer != null)
        {
            spriteRenderer.color = newColor;
        }
    }

    void Update()
    {
        if (!isLocalPlayer)
            return;

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(moveX, moveY, 0) * moveSpeed * Time.deltaTime;
        transform.position += movement;
    }
}