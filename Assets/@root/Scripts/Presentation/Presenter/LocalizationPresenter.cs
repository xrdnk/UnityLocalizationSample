using Deniverse.UnityLocalizationSample.Domain.Service;
using Deniverse.UnityLocalizationSample.Presentation.View;
using UnityEngine;

namespace Deniverse.UnityLocalizationSample.Presentation.Presenter
{
    /// <summary>
    /// ローカライズに関わる部分のプレゼンタークラス
    /// </summary>
    public sealed class LocalizationPresenter : MonoBehaviour
    {
        [SerializeField] LocalizationService _localizationService;
        [SerializeField] LanguageSelectDropdownUIView _dropdownUIView;
        [SerializeField] LanguageSelectToggleUIView _toggleUIView;
        [SerializeField] SampleLocalizationUIView _sampleLocalizationUIView;
        [SerializeField] PrefabSpawner _prefabSpawner;
        [SerializeField] CurrentLanguageView _currentLanguageView;

        void Start()
        {
            // Service State Changed Event -> View Output Event
            _localizationService.InitializationCompletedEvent += _dropdownUIView.InitializeDropdownValueWithoutNotify;
            _localizationService.InitializationCompletedEvent += _toggleUIView.InitializeToggleValueWithoutNotify;
            _localizationService.LocaleIndexChangedEvent += _dropdownUIView.UpdateDropdownValueWithoutNotify;
            _localizationService.LocaleIndexChangedEvent += _toggleUIView.UpdateToggleValueWithoutNotify;
            _localizationService.SpriteTableChangedEvent += _sampleLocalizationUIView.SetFlagImage;
            _localizationService.StringTableChangedEvent += _sampleLocalizationUIView.SetTextMessage;
            _localizationService.AudioTableChangedEvent += _sampleLocalizationUIView.PlayHelloWorld;
            _localizationService.PrefabTableChangedEvent += _prefabSpawner.SpawnPrefab;
            _localizationService.ScriptableObjectTableChangedEvent += _currentLanguageView.SetSampleStore;

            // View Input Event -> Service State Changing
            _dropdownUIView.SelectionChangedEvent += _localizationService.ChangeLocale;
            _toggleUIView.SelectionChangedEvent += _localizationService.ChangeLocale;
        }

        void OnDestroy()
        {
            // Service State Changed Event -> View Output Event
            _localizationService.InitializationCompletedEvent -= _dropdownUIView.InitializeDropdownValueWithoutNotify;
            _localizationService.InitializationCompletedEvent -= _toggleUIView.InitializeToggleValueWithoutNotify;
            _localizationService.LocaleIndexChangedEvent -= _dropdownUIView.UpdateDropdownValueWithoutNotify;
            _localizationService.LocaleIndexChangedEvent -= _dropdownUIView.UpdateDropdownValueWithoutNotify;
            _localizationService.SpriteTableChangedEvent -= _sampleLocalizationUIView.SetFlagImage;
            _localizationService.StringTableChangedEvent -= _sampleLocalizationUIView.SetTextMessage;
            _localizationService.AudioTableChangedEvent -= _sampleLocalizationUIView.PlayHelloWorld;
            _localizationService.PrefabTableChangedEvent -= _prefabSpawner.SpawnPrefab;
            _localizationService.ScriptableObjectTableChangedEvent -= _currentLanguageView.SetSampleStore;

            // View Input Event -> Service State Changing
            _dropdownUIView.SelectionChangedEvent -= _localizationService.ChangeLocale;
            _toggleUIView.SelectionChangedEvent -= _localizationService.ChangeLocale;
        }
    }
}