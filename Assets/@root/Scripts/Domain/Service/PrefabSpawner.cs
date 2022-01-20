using UnityEngine;

namespace Deniverse.UnityLocalizationSample.Domain.Service
{
    /// <summary>
    /// プレハブのスポーナー
    /// </summary>
    public sealed class PrefabSpawner : MonoBehaviour
    {
        GameObject _prefab;

        /// <summary>
        /// プレハブのスポーン処理
        /// </summary>
        public void SpawnPrefab(GameObject newPrefab)
        {
            if (_prefab != null)
            {
                Destroy(_prefab);
            }

            _prefab = Instantiate(newPrefab, new Vector3(0, 5f, 0), Quaternion.identity);
        }
    }
}