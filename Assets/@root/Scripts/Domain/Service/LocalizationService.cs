using System.Collections.Generic;
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
        [SerializeField] LocalizedAssetTable _localizedSpriteTable;
        [SerializeField] LocalizedSprite _localizedFlagReference;
        [SerializeField] LocalizedStringTable _localizedStringTable;
        [SerializeField] LocalizedString _localizedMessageReference;

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
        // AssetTable (ここでは Sprite のみ）に変更が走った時のイベントを発火
        public AssetTableChanged AssetTableChangedEvent;

        void Start()
        {
            // TODO: Addressables を通して LocalizationSettings のロケール情報を非同期的に取得する

            // AsyncOperation 完了時の処理
            if (_initializeOperation.IsDone)
            {
                OnInitializeCompleted(_initializeOperation);
            }
            // AsyncOperation 未完了時の処理
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

        /// <summary>
        /// ロケール初期設定完了時のフック関数
        /// </summary>
        /// <param name="handle"></param>
        void OnInitializeCompleted(AsyncOperationHandle handle)
        {
            var defaultIndex = 0;
            // TODO: 利用可能なロケールを LocalizationSettings から取得する
            // TODO: 最初に選択されたデフォルトのロケールをインデックスとして設定する
            // TODO: 完了したことを通知する

            // TODO: イベント発火

            // TODO: ここでロケールが変更された時のコールバック登録をする

        }

        /// <summary>
        /// ロケール変更時のフック関数
        /// </summary>
        /// <param name="newLocale">新しいロケール</param>
        void OnLocaleChanged(Locale newLocale)
        {
            // TODO: ロケール → インデックス変換

            // TODO: イベント発火

        }

        /// <summary>
        /// AssetTable 変更時のフック関数
        /// </summary>
        /// <param name="newAssetTable">新しい AssetTable</param>
        void OnAssetTableChanged(AssetTable newAssetTable)
        {
            // TODO: GetAssetAsync<TObject>(TableEntryReference) で国旗アセットを取得

            // TODO: 取得完了時にイベント発火

        }

        /// <summary>
        /// StringTable 変更時のフック関数
        /// </summary>
        /// <param name="newStringTable">新しい StringTable</param>
        void OnStringTableChanged(StringTable newStringTable)
        {
            // TODO: LocalizedStringReference.GetLocalizedString でローカライズ文字列を取得

            // TODO: イベント発火
        }

        /// <summary>
        /// ロケールの変更処理
        /// </summary>
        /// <param name="index">該当ロケールのインデックス</param>
        public void ChangeLocale(int index)
        {
            // TODO: 現在のロケールを設定

        }
    }
}