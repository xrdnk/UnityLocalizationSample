using UnityEngine;

namespace Deniverse.UnityLocalizationSample.Domain.DataStore
{
    [CreateAssetMenu(fileName = nameof(SampleStore), menuName = nameof(SampleStore), order = 0)]
    public class SampleStore : ScriptableObject, ISampleStore
    {
        [SerializeField, TextArea]
        string _sampleText;

        public string SampleText => _sampleText;
    }
}