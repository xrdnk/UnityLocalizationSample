using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

namespace Deniverse.UnityLocalizationSample.Presentation.View
{
    /// <summary>
    /// トグル形式でローカライズ情報を表示するビュー
    /// </summary>
    public sealed class LanguageSelectToggleUIView : MonoBehaviour
    {
        [SerializeField] Transform _container;
        [SerializeField] GameObject _togglePrefab;

        readonly Dictionary<int, Toggle> _toggles = new();
        ToggleGroup _toggleGroup;

        public delegate void SelectionChanged(int index);
        /// <summary>
        /// 選択中のトグル項目が変更された時のイベントを発火
        /// </summary>
        public SelectionChanged SelectionChangedEvent;

        void Start()
        {
            _toggleGroup = _container.gameObject.AddComponent<ToggleGroup>();
        }

        /// <summary>
        /// トグルボタンの初期化処理（トグルの生成，値の設定）
        /// </summary>
        /// <param name="locales">ロケールのリスト</param>
        /// <param name="defaultIndex">デフォルトのロケールインデックス（最初に設定されるロケール）</param>
        public void InitializeToggleValueWithoutNotify(IReadOnlyList<Locale> locales, int defaultIndex)
        {
            for (var i = 0; i < locales.Count; ++i)
            {
                var locale = locales[i];

                // トグルの生成
                var languageToggle = Instantiate(_togglePrefab, _container);
                // トグルオブジェクトの名前とラベルにはネイティブネームを設定する
                languageToggle.name = locale.Identifier.CultureInfo != null
                    // CultureInfo が存在する場合は NativeName を表示する (例) ja-JP の場合は「日本語」，en-us の場合は「English」
                    ? locale.Identifier.CultureInfo.NativeName
                    // 存在しない場合はロケール情報をそのまま表示する
                    : locale.ToString();
                var label = languageToggle.GetComponentInChildren<Text>();
                label.text = languageToggle.name;

                // デフォルトのロケールインデックスの場合はトグルをONにする
                var toggle = languageToggle.GetComponent<Toggle>();
                toggle.SetIsOnWithoutNotify(i == defaultIndex);

                // 辞書データキャッシュ
                _toggles[i] = toggle;

                var localeIndex = i;
                toggle.onValueChanged.AddListener(val =>
                {
                    if (val)
                    {
                        SelectionChangedEvent?.Invoke(localeIndex);
                    }
                });

                toggle.group = _toggleGroup;
            }
        }

        /// <summary>
        /// トグル値の更新
        /// </summary>
        /// <param name="index"></param>
        public void UpdateToggleValueWithoutNotify(int index)
        {
            foreach (var toggle in _toggles.Values)
            {
                toggle.SetIsOnWithoutNotify(false);
            }
            _toggles[index].SetIsOnWithoutNotify(true);
        }
    }
}