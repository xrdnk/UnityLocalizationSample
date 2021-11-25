using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Deniverse.UnityLocalizationSample.Domain.Service
{
    public sealed class LocalizationService : MonoBehaviour
    {
        [SerializeField] LocalizedAssetTable _localizedSpriteTable;
        [SerializeField] LocalizedSprite _localizedFlagReference;
        [SerializeField] LocalizedStringTable _localizedStringTable;
        [SerializeField] LocalizedString _localizedMessageReference;

        AsyncOperationHandle _initializeOperation;

        public delegate void InitializationCompleted(List<Locale> locales, int defaultIndex);
        public InitializationCompleted InitializationCompletedEvent;

        public delegate void LocaleIndexChanged(int localeIndex);
        public LocaleIndexChanged LocaleIndexChangedEvent;

        public delegate void StringTableChanged(string message);
        public StringTableChanged StringTableChangedEvent;

        public delegate void AssetTableChanged(Sprite flagSprite);
        public AssetTableChanged AssetTableChangedEvent;

        void Start()
        {
            _initializeOperation = LocalizationSettings.SelectedLocaleAsync;
            if (_initializeOperation.IsDone)
            {
                OnInitializeCompleted(_initializeOperation);
            }
            else
            {
                _initializeOperation.Completed += OnInitializeCompleted;
                _localizedSpriteTable.TableChanged += OnAssetTableChanged;
                _localizedStringTable.TableChanged += OnStringTableChanged;
            }
        }

        void OnDestroy()
        {
            LocalizationSettings.SelectedLocaleChanged -= OnLocaleChanged;
            _localizedSpriteTable.TableChanged -= OnAssetTableChanged;
            _localizedStringTable.TableChanged -= OnStringTableChanged;
        }

        void OnInitializeCompleted(AsyncOperationHandle handle)
        {
            var defaultIndex = 0;
            var locales = LocalizationSettings.AvailableLocales.Locales;
            for (var i = 0; i < locales.Count; ++i)
            {
                var locale = locales[i];
                if (LocalizationSettings.SelectedLocale == locale)
                {
                    defaultIndex = i;
                }
            }
            InitializationCompletedEvent?.Invoke(locales, defaultIndex);
            LocalizationSettings.SelectedLocaleChanged += OnLocaleChanged;
        }

        void OnLocaleChanged(Locale locale)
        {
            var index = LocalizationSettings.AvailableLocales.Locales.IndexOf(locale);
            LocaleIndexChangedEvent?.Invoke(index);
        }

        void OnAssetTableChanged(AssetTable assetTable)
        {
            var operation = assetTable.GetAssetAsync<Sprite>(_localizedFlagReference.TableEntryReference);
            operation.Completed += handle => AssetTableChangedEvent?.Invoke(handle.Result);
        }

        void OnStringTableChanged(StringTable stringTable)
        {
            var localizedString = _localizedMessageReference.GetLocalizedString();
            StringTableChangedEvent?.Invoke(localizedString);
        }

        /// <summary>
        /// ロケールの変更処理
        /// </summary>
        /// <param name="index">該当ロケールのインデックス</param>
        public void ChangeLocale(int index)
        {
            var locale = LocalizationSettings.AvailableLocales.Locales[index];
            LocalizationSettings.SelectedLocale = locale;
        }
    }
}