using Deniverse.UnityLocalizationSample.Domain.Service;
using Deniverse.UnityLocalizationSample.Presentation.Presentation.UIView;
using Deniverse.UnityLocalizationSample.Presentation.UIView;
using UnityEngine;

namespace Deniverse.UnityLocalizationSample.Presentation.Presenter
{
    /// <summary>
    /// ローカライズに関わる部分のプレゼンター
    /// </summary>
    public sealed class LocalizationPresenter : MonoBehaviour
    {
        [SerializeField] LocalizationService _localizationService;
        [SerializeField] LanguageSelectDropdownUIView _dropdownUIView;
        [SerializeField] LanguageSelectToggleUIView _toggleUIView;
        [SerializeField] SampleLocalizationUIView _sampleLocalizationUIView;

        void Start()
        {
            _localizationService.InitializationCompletedEvent += _dropdownUIView.InitializeDropdownValueWithoutNotify;
            _localizationService.InitializationCompletedEvent += _toggleUIView.InitializeToggleValueWithoutNotify;
            _localizationService.LocaleIndexChangedEvent += _dropdownUIView.UpdateDropdownValueWithoutNotify;
            _localizationService.LocaleIndexChangedEvent += _toggleUIView.UpdateToggleValueWithoutNotify;
            _localizationService.AssetTableChangedEvent += _sampleLocalizationUIView.SetFlagImage;
            _localizationService.StringTableChangedEvent += _sampleLocalizationUIView.SetTextMessage;
            _dropdownUIView.SelectionChangedEvent += _localizationService.ChangeLocale;
            _toggleUIView.SelectionChangedEvent += _localizationService.ChangeLocale;
        }

        void OnDestroy()
        {
            _localizationService.InitializationCompletedEvent -= _dropdownUIView.InitializeDropdownValueWithoutNotify;
            _localizationService.InitializationCompletedEvent -= _toggleUIView.InitializeToggleValueWithoutNotify;
            _localizationService.LocaleIndexChangedEvent -= _dropdownUIView.UpdateDropdownValueWithoutNotify;
            _localizationService.LocaleIndexChangedEvent -= _dropdownUIView.UpdateDropdownValueWithoutNotify;
            _localizationService.AssetTableChangedEvent -= _sampleLocalizationUIView.SetFlagImage;
            _localizationService.StringTableChangedEvent -= _sampleLocalizationUIView.SetTextMessage;
            _dropdownUIView.SelectionChangedEvent -= _localizationService.ChangeLocale;
            _toggleUIView.SelectionChangedEvent -= _localizationService.ChangeLocale;
        }
    }
}