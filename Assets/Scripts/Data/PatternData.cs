using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "PatternData", menuName = "ProjectForgotten/PatternData")]
public class PatternData : ScriptableObject
{
    [BoxGroup("General settings")]
    public bool disable;

    [BoxGroup("General settings")]
    public bool isImpulse;

    [BoxGroup("Bullet settings")]
    [ShowAssetPreview]
    public GameObject bulletPrefab;

    [BoxGroup("Bullet settings")]
    [Range(1, 15)]
    public int bulletCount = 1;

    [BoxGroup("Bullet settings")]
    [Range(1, 100)]
    public float bulletSpeed = 10;

    [BoxGroup("Bullet settings")]
    [Range(0, 1)]
    public float bulletAccellerationRate;

    [BoxGroup("Bullet settings")]
    [Range(0, 0.1f)]
    public float bulletDecellerationRate;

    [BoxGroup("Angle settings")]
    [Range(0, 360)]
    public float angle;

    [BoxGroup("Angle settings")]
    [Range(-360, 360)]
    public float angleOffset;

    [BoxGroup("Rotation settings")]
    [Range(-5, 5)]
    public float rotationSpeed;

    [BoxGroup("Offset settings")]
    public Vector2 spawnOffset;
}