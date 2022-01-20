using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

namespace Deniverse.UnityLocalizationSample.Presentation.View
{
    /// <summary>
    /// ドロップダウン形式でローカライズ情報を表示するビュー
    /// </summary>
    public sealed class LanguageSelectDropdownUIView : MonoBehaviour
    {
        [SerializeField] Dropdown _dropdown;

        public delegate void SelectionChanged(int index);
        /// <summary>
        /// 選択中のドロップダウン項目が変更された時のイベントを発火
        /// </summary>
        public SelectionChanged SelectionChangedEvent;

        void Start()
        {
            _dropdown.onValueChanged.AddListener(OnSelectionChanged);

            // 最初はドロップダウンを非活性状態にする
            _dropdown.ClearOptions();
            _dropdown.options.Add(new Dropdown.OptionData("Loading..."));
            _dropdown.interactable = false;
        }

        void OnDestroy()
        {
            _dropdown.onValueChanged.RemoveListener(OnSelectionChanged);
        }

        /// <summary>
        /// 選択中のドロップダウン項目が変更された時のイベントを発火
        /// </summary>
        /// <param name="index">選択された項目のインデックス</param>
        void OnSelectionChanged(int index)
        {
            SelectionChangedEvent?.Invoke(index);
        }

        /// <summary>
        /// ドロップダウンの初期化処理(ドロップダウンの項目値の設定)
        /// </summary>
        /// <param name="locales">ロケールのリスト</param>
        /// <param name="defaultIndex">デフォルトのロケールインデックス（最初に設定されるロケール）</param>
        public void InitializeDropdownValueWithoutNotify(IReadOnlyList<Locale> locales, int defaultIndex)
        {
            // ドロップダウンの項目値を設定するためのオプションを設定する
            var options =
                locales
                    .Select(
                        locale => locale.Identifier.CultureInfo != null
                            // CultureInfo が存在する場合は NativeName を表示する (例) ja-JP の場合は「日本語」，en-us の場合は「English」
                            ? locale.Identifier.CultureInfo.NativeName
                            // 存在しない場合はロケール情報をそのまま表示する
                            : locale.ToString())
                    .ToList();

            // オプションの List 要素数が 0 の時
            if (options.Count == 0)
            {
                // "利用可能なロケールがありません"のオプションを追加
                options.Add("No Locales Available");
                // 非活性にする
                _dropdown.interactable = false;
            }
            // オプションの List 要素数が 1 の時
            else
            {
                // 活性にする
                _dropdown.interactable = true;
            }

            // ドロップアウンのオプションをクリア
            _dropdown.ClearOptions();
            // ドロップダウンのオプションを設定
            _dropdown.AddOptions(options);
            // 最初に表示する項目を設定
            _dropdown.SetValueWithoutNotify(defaultIndex);
        }

        /// <summary>
        /// ドロップダウン値の更新
        /// </summary>
        /// <param name="index">ロケールインデックス</param>
        public void UpdateDropdownValueWithoutNotify(int index) => _dropdown.SetValueWithoutNotify(index);
    }
}