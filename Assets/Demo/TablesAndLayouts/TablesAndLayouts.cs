using System;
using UnityEngine;
using ZergRush.ReactiveCore;
using ZergRush.ReactiveUI;

namespace Demo.TablesAndLayouts
{
    class TablesAndLayouts : ConnectableObject
    {
        public SimpleView prefab;
    
        public ReactiveScrollRect vertical;
        public ReactiveScrollRect horizontal;
        public ReactiveScrollRect gridVertical;
        public ReactiveScrollRect gridHorizontal;
        public ReactiveScrollRect variableVertical;
        public ReactiveScrollRect variableHorizontal;
    
        public TableLayoutSettings settingsVertical;
        public TableLayoutSettings settingsHorizontal;
        public TableLayoutSettings settingsGridVertical;
        public TableLayoutSettings settingsGridHorizontal;

        void Start()
        {
            var data = TestData.instance.data;
        
            Action<Cell<int>, SimpleView> factory = (i, view) => {
                view.addConnection = i.Bind(val => view.name.text = val.ToString());
            };

            // Linear layout
            addConnection = Rui.PresentInScrollWithReusableViews(vertical, data, prefab, settingsVertical, factory, delegates: Animations.Default<SimpleView>());
            addConnection = Rui.PresentInScrollWithReusableViews(horizontal, data, prefab, settingsHorizontal, factory, delegates: Animations.Default<SimpleView>());

            // Grid layout
            addConnection = Rui.PresentInScrollWithReusableViews(gridHorizontal, data, prefab, settingsGridHorizontal, factory,
                layout: Rui.GridTableLayout(data.CountCell(), settingsGridHorizontal, 3), 
                delegates: Animations.Default<SimpleView>()
            );

            addConnection = Rui.PresentInScrollWithReusableViews(gridVertical, data, prefab, settingsGridVertical, factory,
                layout: Rui.GridTableLayout(data.CountCell(), settingsGridVertical, 6), 
                delegates: Animations.Default<SimpleView>()
            );

            // Variable view size layout
            Func<Cell<int>, float> sizeFactory = item => 100 + item.value * 10;
            Action<Cell<int>, SimpleView> variableSizeViewFactory = (i, view) =>
            {
                var size = sizeFactory(i);
                view.rectTransform.sizeDelta = new Vector2(size, size);
                factory(i, view);
            };

            addConnection = Rui.PresentInScrollWithReusableViews(variableHorizontal, data, prefab, settingsHorizontal, variableSizeViewFactory,
                layout: Rui.VariableViewSizeLayout(data, sizeFactory, settingsHorizontal, connectionSink),
                delegates: Animations.Default<SimpleView>()
            );

            addConnection = Rui.PresentInScrollWithReusableViews(variableVertical, data, prefab, settingsGridVertical, variableSizeViewFactory,
                layout: Rui.VariableViewSizeLayout(data, sizeFactory, settingsVertical, connectionSink),
                delegates: Animations.Default<SimpleView>()
            );

        }
    }
}
