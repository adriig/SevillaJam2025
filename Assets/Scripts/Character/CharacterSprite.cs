using UnityEngine;

public enum CharacterSpriteType
{
    Alone,
    Overflow,
}

public abstract class CharacterSprite : ScriptableObject
{
    [SerializeField]
    public Sprite sprite;

    [SerializeField]
    public CharacterSpriteType type;
}
