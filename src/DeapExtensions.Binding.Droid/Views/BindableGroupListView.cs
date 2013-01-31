using System;
using Android.Content;
using Android.Util;
using System.Collections.Generic;
using System.Windows.Input;
using Cirrious.MvvmCross.Binding.Droid.Views;
using Cirrious.MvvmCross.Binding.Droid;

namespace DeapExtensions.Binding.Droid.Views
{
	public class BindableGroupListView : MvxBindableListView
	{
		public BindableGroupListView(Context context, IAttributeSet attrs)
			: this(context, attrs, new BindableGroupListAdaptor(context))
		{
		}
		
		public BindableGroupListView(Context context, IAttributeSet attrs, BindableGroupListAdaptor adapter)
			: base(context, attrs, adapter)
		{
			var groupTemplateId = MvxBindableListViewHelpers.ReadAttributeValue(context, attrs,
			                                                                   MvxAndroidBindingResource.Instance
			                                                                   .BindableListViewStylableGroupId,
			                                                                   AndroidBindingResource.Instance
			                                                                   .BindableListGroupItemTemplateId);
			adapter.GroupTemplateId = groupTemplateId;
		}

		protected override void ExecuteCommandOnItem(ICommand command, int position)
		{
			if (command == null)
				return;
			
			var item = Adapter.GetRawItem(position);
			if (item == null)
				return;

			var flatItem = (BindableGroupListAdaptor.FlatItem)item;
			if (flatItem.IsGroup || !command.CanExecute(flatItem.Item))
				return;
			
			command.Execute(flatItem.Item);
		}
	}
}

