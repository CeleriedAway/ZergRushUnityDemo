using System;
using System.Linq;
using Demo.TablesAndLayouts;
using UnityEngine.UI;
using ZergRush.ReactiveCore;
using ZergRush.ReactiveUI;
using Random = UnityEngine.Random;

namespace Demo.ReactiveCollectionTransformations
{
	public class ReactiveCollectionTransformations : ConnectableObject
	{
		ReactiveCollection<int> data = new ReactiveCollection<int>();

		public SimpleView prefab;

		public ReactiveScrollRect initial;
		public ReactiveScrollRect mapped;
		public ReactiveScrollRect filtered;

		public Button insertButton;
		public Button removeButton;
		
		void Start ()
		{
			data.Reset(Enumerable.Range(0, 5).Select(_ => Random.Range(0, 20)));
			insertButton.ClickStream().Listen(() => data.Insert(Random.Range(0, data.current.Count), Random.Range(0, 20)));
			removeButton.ClickStream().Listen(() =>
			{
				if (data.current.Count > 0)	
                    data.RemoveAt(Random.Range(0, data.current.Count));
			});
			

			TableLayoutSettings settings = new TableLayoutSettings
			{
				direction = LayoutDirection.Horizontal,
				margin = 30,
				topShift = 30,
				bottomShift = 30,
			};
			
            Action<int, SimpleView> factory = (i, view) => {
                view.name.text = i.ToString();
            };
			
            addConnection = Rui.PresentInScrollWithReusableViews(initial, data, prefab, settings, factory, delegates: Animations.Default<SimpleView>());

			var mappedData = data.Map(val => (val * 3 + 5) % 10);
			
            addConnection = Rui.PresentInScrollWithReusableViews(mapped, mappedData, prefab, settings, factory, delegates: Animations.Default<SimpleView>());
			
			var filteredData = mappedData.Filter(val => val % 2 == 0);
			
            addConnection = Rui.PresentInScrollWithReusableViews(filtered, filteredData, prefab, settings, factory, delegates: Animations.Default<SimpleView>());
		}
	}
}
