using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class GizmosDrawer : MonoBehaviour
{
    [SerializeField]
    [BoxGroup("Visibility settings")]
    private bool _hideAllGizmos;

    [SerializeField]
    [BoxGroup("Visibility settings")]
    private bool _hideAngleGizmos;

    [SerializeField]
    [BoxGroup("Visibility settings")]
    private bool _hidePositionOffsetGizmos;

    [SerializeField]
    [BoxGroup("Visibility settings")]
    private bool _hideBulletVelocitiesGizmos;

    [SerializeField]
    [BoxGroup("Visibility settings")]
    [Range(1, 3)]
    private int _refreshRateInSec = 1;

    [SerializeField]
    [BoxGroup("Draw settings")]
    private Color _angleColor;
    
    [SerializeField]
    [BoxGroup("Draw settings")]
    private Color _positionOffsetColor;

    [SerializeField]
    [BoxGroup("Draw settings")]
    private Color _bulletVelocitiesColor;

    private Vector3 _originPosition => transform.position +
            new Vector3(_pattern.spawnOffset.x, _pattern.spawnOffset.y, 0);
    private BulletSpawnerBehaviour _spawner;
    private PatternData _pattern;

    private void Start()
    {
        InvokeRepeating("ClearVelocitiesList", _refreshRateInSec, _refreshRateInSec);
    }

    private void OnDrawGizmos()
    {
        if (_hideAllGizmos) return;

        GetSpawnerInfo();

        DrawAngleGizmos();
        DrawPositionOffsetGizmos();
        if (Application.isPlaying)
            DrawBulletVelocitiesGizmos();
    }

    private void OnValidate()
    {
        GetSpawnerInfo();
        CancelInvoke();
        InvokeRepeating("ClearVelocitiesList", _refreshRateInSec, _refreshRateInSec);
    }

    private void DrawAngleGizmos()
    {
        if (_hideAngleGizmos) return;

        Gizmos.color = _angleColor;

        if (_pattern.angle == 360)
            Gizmos.DrawWireSphere(_originPosition, _pattern.bulletSpeed);
        else
        {
            Vector3 zeroAngleVector = _spawner.zeroAngleVector * _pattern.bulletSpeed;
            Vector3 angleVector = _spawner.maxAngleVector * _pattern.bulletSpeed;

            Gizmos.DrawLine(_originPosition, _originPosition + zeroAngleVector);
            Gizmos.DrawLine(_originPosition, _originPosition + angleVector);
        }
    }

    private void DrawPositionOffsetGizmos()
    {
        if (_hidePositionOffsetGizmos) return;

        Gizmos.color = _positionOffsetColor;

        Gizmos.DrawLine(transform.position, _originPosition);
    }

    private void DrawBulletVelocitiesGizmos()
    {
        if (_hideBulletVelocitiesGizmos) return;

        Gizmos.color = _bulletVelocitiesColor;

        foreach (Vector3 bulletVelocity in _spawner.bulletVelocities)
            Gizmos.DrawLine(_originPosition,
                            _originPosition + bulletVelocity * _pattern.bulletSpeed);
    }

    private void GetSpawnerInfo()
    {
        if (_spawner == null)
            _spawner = GetComponent<BulletSpawnerBehaviour>();
        if (_pattern == null || _pattern != _spawner.pattern)
            _pattern = _spawner.pattern;
    }

    private void ClearVelocitiesList()
    {
        _spawner.bulletVelocities = new List<Vector3>();
    }
}