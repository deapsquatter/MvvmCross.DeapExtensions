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

		public ICommand GroupClick { get; set; }

		protected override void ExecuteCommandOnItem(ICommand command, int position)
		{
			var item = Adapter.GetRawItem(position);
			if (item == null)
				return;
			var flatItem = (BindableGroupListAdaptor.FlatItem)item;

			if (flatItem.IsGroup)
				command = GroupClick;

			if (command == null)
				return;

			if (!command.CanExecute(flatItem.Item))
				return;
			
			command.Execute(flatItem.Item);
		}
	}
}

