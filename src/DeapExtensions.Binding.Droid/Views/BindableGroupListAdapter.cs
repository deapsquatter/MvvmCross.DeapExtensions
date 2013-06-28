using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Android.Content;
using Cirrious.MvvmCross.Binding.Droid.Views;

namespace DeapExtensions.Binding.Droid.Views
{
	public class BindableGroupListAdapter : MvxAdapter
	{
		int groupTemplateId;
		private IEnumerable _itemsSource;

		public BindableGroupListAdapter(Context context): base(context)
		{
		}

		public int GroupTemplateId
		{
			get { return groupTemplateId; }
			set
			{
				if (groupTemplateId == value)
					return;
				groupTemplateId = value;

				// since the template has changed then let's force the list to redisplay by firing NotifyDataSetChanged()
				if (ItemsSource != null)
					NotifyDataSetChanged();
			}
		}

		void OnItemsSourceCollectionChanged (object sender, NotifyCollectionChangedEventArgs e)
		{
			FlattenAndSetSource(_itemsSource);
		}

		void FlattenAndSetSource(IEnumerable value)
		{
			var list = value.Cast<object>(); 
			var flattened = list.SelectMany ((g) => FlattenHierachy (g));
			base.SetItemsSource (flattened.ToList ());
		}

		protected override void SetItemsSource (System.Collections.IEnumerable value)
		{
			if (_itemsSource == value)
				return;
			var existingObservable = _itemsSource as INotifyCollectionChanged;
			if (existingObservable != null)
				existingObservable.CollectionChanged -= OnItemsSourceCollectionChanged;

			_itemsSource = value;

			var newObservable = _itemsSource as INotifyCollectionChanged;
			if (newObservable != null)
				newObservable.CollectionChanged += OnItemsSourceCollectionChanged;

			if (value != null) {
				FlattenAndSetSource(value);
			}else
				base.SetItemsSource(null);
		}

		public class FlatItem 
		{
			public bool IsGroup;
			public object Item;
		}

		IEnumerable<Object> FlattenHierachy(object group)
		{
			yield return new FlatItem {IsGroup = true, Item = group};
			IEnumerable items = group as IEnumerable;
			if (items != null)
				foreach (object d in items)
					yield return new FlatItem {IsGroup = false, Item = d};
		}

		protected override global::Android.Views.View GetBindableView(global::Android.Views.View convertView, object source, int templateId)
		{
			FlatItem item = (FlatItem)source;
			if (item.IsGroup)
				return base.GetBindableView(convertView, item.Item, GroupTemplateId);
			else
				return base.GetBindableView(convertView, item.Item, ItemTemplateId);
		}
	}
}

