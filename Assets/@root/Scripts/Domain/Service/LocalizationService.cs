using System;
using System.Collections.Generic;
using Deniverse.UnityLocalizationSample.Domain.Extension;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Deniverse.UnityLocalizationSample.Domain.Service
{
    /// <summary>
    /// ローカライズに関するドメインサービス
    /// </summary>
    public sealed class LocalizationService : MonoBehaviour
    {
        [Header("スプライト設定")]
        [SerializeField] LocalizedAssetTable _localizedSpriteTable;
        [SerializeField] LocalizedSprite _localizedFlagReference;

        [Header("文字列設定")]
        [SerializeField] LocalizedStringTable _localizedStringTable;
        [SerializeField] LocalizedString _localizedMessageReference;

        [Header("オーディオ設定")]
        [SerializeField] LocalizedAssetTable _localizedAudioTable;
        [SerializeField] LocalizedAudioClip _localizedAudioClipReference;

        [Header("プレハブ設定")]
        [SerializeField] LocalizedAssetTable _localizedPrefabTable;
        [SerializeField] LocalizedGameObject _localizedPrefabReference;

        [Header("Scriptable Object 設定")]
        [SerializeField] LocalizedAssetTable _localizedScriptableObjectTable;
        [SerializeField] LocalizedScriptableObject _localizedScriptableObjectReference;

        /// <summary>
        /// ロケール初期設定用の AsyncOperationHandle
        /// </summary>
        AsyncOperationHandle _initializeOperation;

        public delegate void InitializationCompleted(IReadOnlyList<Locale> locales, int defaultIndex);
        // ロケール初期設定が完了した時のイベントを発火
        public InitializationCompleted InitializationCompletedEvent;

        public delegate void LocaleIndexChanged(int localeIndex);
        // ロケールインデックスに変更が走った時のイベントを発火
        public LocaleIndexChanged LocaleIndexChangedEvent;

        public delegate void StringTableChanged(string message);
        // StringTable に変更が走った時のイベントを発火
        public StringTableChanged StringTableChangedEvent;

        public delegate void AssetTableChanged(Sprite flagSprite);
        // SpriteTable に変更が走った時のイベントを発火
        public AssetTableChanged SpriteTableChangedEvent;

        public delegate void AssetAudioChanged(AudioClip audioClip);
        // AudioTable に変更が走った時のイベントを発火
        public AssetAudioChanged AudioTableChangedEvent;

        public delegate void AssetPrefabChanged(GameObject prefab);
        // PrefabTable に変更が走った時のイベントを発火
        public AssetPrefabChanged PrefabTableChangedEvent;

        public delegate void AssetScriptableObjectChanged(ScriptableObject scriptableObject);
        // ScriptableObjectTable に変更が走った時のイベントを発火
        public AssetScriptableObjectChanged ScriptableObjectTableChangedEvent;

        void Start()
        {
            // Addressables を通して LocalizationSettings のロケール情報を非同期的に取得する
            _initializeOperation = LocalizationSettings.SelectedLocaleAsync;
            // AsyncOperation 完了時の処理
            if (_initializeOperation.IsDone)
            {
                OnInitializeCompleted(_initializeOperation);
            }
            // AsyncOperation 未完了時の処理 (コールバック登録）
            else
            {
                _initializeOperation.Completed += OnInitializeCompleted;
                _localizedStringTable.TableChanged += OnStringTableChanged;
                _localizedSpriteTable.TableChanged += OnSpriteTableChanged;
                _localizedAudioTable.TableChanged += OnAudioTableChanged;
                _localizedPrefabTable.TableChanged += OnPrefabTableChanged;
                _localizedScriptableObjectTable.TableChanged += OnScriptableObjectTableChanged;
            }
        }

        void OnDestroy()
        {
            LocalizationSettings.SelectedLocaleChanged -= OnLocaleChanged;
            _localizedStringTable.TableChanged -= OnStringTableChanged;
            _localizedSpriteTable.TableChanged -= OnSpriteTableChanged;
            _localizedAudioTable.TableChanged -= OnAudioTableChanged;
            _localizedPrefabTable.TableChanged -= OnPrefabTableChanged;
            _localizedScriptableObjectTable.TableChanged -= OnScriptableObjectTableChanged;
        }

        /// <summary>
        /// ロケール初期設定完了時のフック関数
        /// </summary>
        /// <param name="handle"></param>
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

        /// <summary>
        /// ロケール変更時のフック関数
        /// </summary>
        /// <param name="newLocale">新しいロケール</param>
        void OnLocaleChanged(Locale newLocale)
        {
            var index = LocalizationSettings.AvailableLocales.Locales.IndexOf(newLocale);
            LocaleIndexChangedEvent?.Invoke(index);
        }

        /// <summary>
        /// StringTable 変更時のフック関数
        /// </summary>
        void OnStringTableChanged(StringTable newStringTable)
        {
            var localizedString = _localizedMessageReference.GetLocalizedString();
            StringTableChangedEvent?.Invoke(localizedString);
        }

        /// <summary>
        /// SpriteTable 変更時のフック関数
        /// </summary>
        void OnSpriteTableChanged(AssetTable newAssetTable)
        {
            var operation = newAssetTable.GetAssetAsync<Sprite>(_localizedFlagReference.TableEntryReference);
            operation.Completed += handle => SpriteTableChangedEvent?.Invoke(handle.Result);
        }

        /// <summary>
        /// AudioTable 変更時のフック関数
        /// </summary>
        void OnAudioTableChanged(AssetTable newAudioTable)
        {
            var operation = newAudioTable.GetAssetAsync<AudioClip>(_localizedAudioClipReference.TableEntryReference);
            operation.Completed += handle => AudioTableChangedEvent?.Invoke(handle.Result);
        }

        /// <summary>
        /// PrefabTable 変更時のフック関数
        /// </summary>
        void OnPrefabTableChanged(AssetTable newPrefabTable)
        {
            var operation = newPrefabTable.GetAssetAsync<GameObject>(_localizedPrefabReference.TableEntryReference);
            operation.Completed += handle => PrefabTableChangedEvent?.Invoke(handle.Result);
        }

        /// <summary>
        /// ScriptableObjectTable 変更時のフック関数
        /// </summary>
        void OnScriptableObjectTableChanged(AssetTable newScriptableObjectTable)
        {
            var operation = newScriptableObjectTable.GetAssetAsync<ScriptableObject>(_localizedScriptableObjectReference.TableEntryReference);
            operation.Completed += handle => ScriptableObjectTableChangedEvent?.Invoke(handle.Result);
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