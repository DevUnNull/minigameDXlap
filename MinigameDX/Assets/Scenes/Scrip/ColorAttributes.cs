using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public enum ColorType
{
    ColorA, // Bên A / Left
    ColorB  // Bên B / Right
}

public class ColorAttributes : MonoBehaviour
{
    public ColorType colorType;
    private ColorType currentColorType;
    public SpriteRenderer spriteRenderer;

    [Header("Random Color")]
    public bool randomOnStart = true;

    public bool isPlayer = true ;

    private void Awake()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    private void Start()
    {
        currentColorType = colorType;
        if (randomOnStart)
        {
            RandomizeColorType();
        }

        UpdateColorVisual();
    }

    void RandomizeColorType()
    {
        if(isPlayer)
        {
            colorType = currentColorType; // Player luôn là màu A
            return;
        }
        colorType = Random.value < 0.5f
            ? ColorType.ColorA
            : ColorType.ColorB;
    }

    public void SetColorType(ColorType type)
    {
        colorType = type;
        UpdateColorVisual();
    }

    public void UpdateColorVisual()
    {
        if (spriteRenderer == null || GameManager.Instance == null) return;

        switch (colorType)
        {
            case ColorType.ColorA:
                spriteRenderer.color = GameManager.Instance.activeColorA;
                break;

            case ColorType.ColorB:
                spriteRenderer.color = GameManager.Instance.activeColorB;
                break;
        }
    }
}
