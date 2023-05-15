using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class BulletSpawnerBehaviour : MonoBehaviour
{
    [SerializeField]
    private List<PatternDataSet> _patternDataSets;

    [Expandable]
    public PatternData pattern;

    [NonSerializedAttribute]
    public List<Vector3> bulletVelocities;
    public Vector3 zeroAngleVector =>
        Quaternion.Euler(_zeroAngleEuler) * Vector3.up;
    public Vector3 maxAngleVector =>
        Quaternion.Euler(_maxAngleEuler) * Vector3.up;

    private Vector3 _zeroAngleEuler =>
        new Vector3(0, 0, -pattern.angleOffset + transform.localEulerAngles.z);
    private Vector3 _maxAngleEuler =>
        new Vector3(0, 0, -pattern.angle -
                            pattern.angleOffset + transform.localEulerAngles.z);
    private Vector3 _normalVector =>
        pattern.angle == 360 ? zeroAngleVector : maxAngleVector;
    private PatternDataSet _currentPatternDataSet;
    private int _radialToggle => pattern.angle == 360 ? 0 : 1;
    private bool _impulsePatternPlayed => pattern.isImpulse && _playedImpulse;
    private float _anglePerBullet;
    private int _currentPatternSetIndex;
    private int _currentPatternIndex;
    private bool _playedImpulse;

    private void Start()
    {
        bulletVelocities = new List<Vector3>();

        _currentPatternIndex = 0;
        _currentPatternSetIndex = 0;
        _currentPatternDataSet = _patternDataSets[_currentPatternSetIndex];
        pattern = _currentPatternDataSet.patterns[_currentPatternIndex];
    }

    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 0, -pattern.rotationSpeed));
        if (pattern.rotationSpeed == 0) transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void SwitchPattern()
    {
        if (_currentPatternIndex == _currentPatternDataSet.patterns.Count - 1)
        {
            _currentPatternDataSet = _patternDataSets[++_currentPatternSetIndex];
            _currentPatternIndex = -1;
        }
        pattern = _currentPatternDataSet.patterns[++_currentPatternIndex];

        transform.rotation = Quaternion.Euler(0, 0, 0);
        _playedImpulse = false;
    }

    public void Shoot()
    {
        if (pattern.disable || _impulsePatternPlayed) return;

        SpawnNewBullet(_normalVector);

        if (pattern.bulletCount == 1) return;

        _anglePerBullet = pattern.angle / (pattern.bulletCount - _radialToggle);
        for (int i = 0; i < pattern.bulletCount; i++)
            SpawnNewArrayBullet(i);

        if (pattern.isImpulse) _playedImpulse = true;
    }

    private void SpawnNewArrayBullet(int bulletIndex)
    {
        float changedAngle = pattern.angleOffset - transform.localEulerAngles.z;
        float calculatedAngle = -_anglePerBullet * bulletIndex - changedAngle;
        Vector3 bulletVelocityAngle = new Vector3(0, 0, calculatedAngle);
        Vector3 bulletVelocity = Quaternion.Euler(bulletVelocityAngle) * Vector3.up;

        SpawnNewBullet(bulletVelocity);
    }

    private void SpawnNewBullet(Vector3 velocity)
    {
        bulletVelocities.Add(velocity);

        GameObject bulletGO = Instantiate(pattern.bulletPrefab);
        Vector3 newPosition =
            new Vector3(pattern.spawnOffset.x, pattern.spawnOffset.y, 0);
        bulletGO.transform.position = transform.position + newPosition;

        BulletBehaviour bullet = bulletGO.GetComponent<BulletBehaviour>();
        bullet.Instantiate(velocity, pattern.bulletSpeed);
        bullet.SetAccellerationRate(pattern.bulletAccellerationRate);
        bullet.SetDecellerationRate(pattern.bulletDecellerationRate);
    }
}