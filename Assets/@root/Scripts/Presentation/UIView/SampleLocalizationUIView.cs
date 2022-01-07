using UnityEngine;
using UnityEngine.UI;

namespace Deniverse.UnityLocalizationSample.Presentation.UIView
{
    /// <summary>
    /// サンプル用のローカライズビュー
    /// </summary>
    public sealed class SampleLocalizationUIView : MonoBehaviour
    {
        [SerializeField] Image _image_Flag;
        [SerializeField] Text _text_HelloWorld;

        /// <summary>
        /// 国旗イメージの設定
        /// </summary>
        /// <param name="sprite"></param>
        public void SetFlagImage(Sprite sprite) => _image_Flag.sprite = sprite;

        /// <summary>
        /// テキストの設定
        /// </summary>
        /// <param name="text"></param>
        public void SetTextMessage(string text) => _text_HelloWorld.text = text;
    }
}