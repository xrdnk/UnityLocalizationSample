using Deniverse.UnityLocalizationSample.Domain.DataStore;
using UnityEngine;
using UnityEngine.UI;

namespace Deniverse.UnityLocalizationSample.Domain.Service
{
    public sealed class ScriptableObjectSetter : MonoBehaviour
    {
        [SerializeField] Text _text;

        ISampleStore _sampleStore;

        public void SetSampleStore(ScriptableObject so)
        {
            if (so is ISampleStore sampleStore)
            {
                _sampleStore = sampleStore;
            }

            if (_sampleStore != null)
            {
                _text.text = _sampleStore.SampleText;
            }
        }
    }
}