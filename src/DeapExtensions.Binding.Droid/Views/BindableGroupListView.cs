using System;
using Android.Content;
using Android.Util;
using System.Windows.Input;
using Cirrious.MvvmCross.Binding.Droid.Views;
using Cirrious.MvvmCross.Binding.Droid.ResourceHelpers;

namespace DeapExtensions.Binding.Droid.Views
{
	public class BindableGroupListView : MvxListView
	{
		public BindableGroupListView(Context context, IAttributeSet attrs)
			: this(context, attrs, new BindableGroupListAdapter(context))
		{
		}
		
		public BindableGroupListView(Context context, IAttributeSet attrs, BindableGroupListAdapter adapter)
			: base(context, attrs, adapter)
		{
			var groupTemplateId = MvxAttributeHelpers.ReadAttributeValue(context, attrs,
			                                                                   MvxAndroidBindingResource.Instance
			                                                             		.ListViewStylableGroupId,
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
			var flatItem = (BindableGroupListAdapter.FlatItem)item;

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

