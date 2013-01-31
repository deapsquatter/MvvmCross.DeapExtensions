using System;
using Cirrious.MvvmCross.Interfaces.ServiceProvider;
using Cirrious.MvvmCross.ExtensionMethods;
using Cirrious.MvvmCross.Droid.Interfaces;

namespace DeapExtensions.Binding.Droid
{
	public class AndroidBindingResource
		: IMvxServiceConsumer
	{
		public static readonly AndroidBindingResource Instance = new AndroidBindingResource();
		
		private AndroidBindingResource()
		{
			var setup = this.GetService<IMvxAndroidGlobals>();
			var resourceTypeName = setup.ExecutableNamespace + ".Resource";
			Type resourceType = setup.ExecutableAssembly.GetType(resourceTypeName);
			if (resourceType == null)
				throw new Exception("Unable to find resource type - " + resourceTypeName);
			try
			{
				BindableListGroupItemTemplateId =
					(int)
						resourceType.GetNestedType("Styleable")
						.GetField("MvxBindableListView_GroupItemTemplate")
						.GetValue(null);
			}
			catch (Exception)
			{
				throw new Exception(
					"Error finding resource ids for MvvmCross.DeapBinding - please make sure ResourcesToCopy are linked into the executable");
			}
		}

		public int BindableListGroupItemTemplateId { get; private set; }
	}
}

