using UnityEngine;

[CreateAssetMenu(
    fileName = "New OverflowCharacterSprite",
    menuName = "Game/SpriteCharacter/OverflowCharacterSprite"
)]
public class OverflowCharacterSprite : CharacterSprite
{
    public float TransparencyPercentage = 1f;

    public void SetTransparency(float value, SpriteRenderer spriteRenderer)
    {
        TransparencyPercentage = Mathf.Clamp01(value);
        Color color = spriteRenderer.color;
        color.a = TransparencyPercentage;
        spriteRenderer.color = color;
    }
}
