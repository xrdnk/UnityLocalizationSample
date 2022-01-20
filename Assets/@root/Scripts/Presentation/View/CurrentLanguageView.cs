using Deniverse.UnityLocalizationSample.Domain.DataStore;
using UnityEngine;
using UnityEngine.UI;

namespace Deniverse.UnityLocalizationSample.Presentation.View
{
    public sealed class CurrentLanguageView : MonoBehaviour
    {
        [SerializeField] Text _text_CurrentLanguage;

        ISampleStore _sampleStore;

        public void SetSampleStore(ScriptableObject so)
        {
            if (so is ISampleStore sampleStore)
            {
                _sampleStore = sampleStore;
            }

            if (_sampleStore != null)
            {
                _text_CurrentLanguage.text = _sampleStore.SampleText;
            }
        }
    }
}