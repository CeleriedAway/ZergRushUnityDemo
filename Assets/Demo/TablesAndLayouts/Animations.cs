using DG.Tweening;
using UnityEngine;
using ZergRush;
using ZergRush.ReactiveUI;

namespace Demo.TablesAndLayouts
{
    public static class Animations
    {
        public static TableDelegates<TView> Default<TView>()
            where TView : ReusableView
        {
            return new TableDelegates<TView>
            {
                onInsert = v =>
                {
                    v.rectTransform.localScale = Vector3.zero;
                    v.rectTransform.DOScale(1, 0.6f).SetEase(Ease.OutCubic);
                },
                onRemove = v =>
                {
                    v.rectTransform.localScale = Vector3.one;
                    v.rectTransform.DOScale(0, 0.6f).SetEase(Ease.OutCubic);
                    return 0.6f;
                },
                moveAnimation = (v, pos) =>
                {
                    var anim = v.rectTransform.DOAnchorPos(pos, 0.6f).SetEase(Ease.OutCubic);
                    return new AnonymousDisposable(() => anim.Kill());
                },
                onRecycle = v =>
                {
                    v.rectTransform.DOKill();
                    v.rectTransform.localScale = Vector3.one;
                }
            };
        }
    }
}