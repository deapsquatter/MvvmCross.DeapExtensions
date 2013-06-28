// MvxBaseAndroidBindingSetup.cs
// (c) Copyright Cirrious Ltd. http://www.cirrious.com
// MvvmCross is licensed using Microsoft Public License (Ms-PL)
// Contributions and inspirations noted in readme.md and license.txt
// 
// Project Lead - Stuart Lodge, @slodge, me@slodge.com

using System;
using System.Collections.Generic;
using Cirrious.MvvmCross.Binding.Droid;
using Android.Content;
using Cirrious.MvvmCross.Droid.Platform;
using System.Reflection;
using Cirrious.CrossCore.IoC;
using Android.Views;

namespace DeapExtensions.Binding.Droid
{
    public abstract class BaseAndroidBindingSetup
        : MvxAndroidSetup
    {

		protected BaseAndroidBindingSetup(Context applicationContext)
			: base(applicationContext)
		{
		}

		protected override IDictionary<string, string> ViewNamespaceAbbreviations
		{
			get
			{
				var abbreviations = base.ViewNamespaceAbbreviations;
				abbreviations["DeapExt"] = "DeapExtensions.Binding.Droid.Views";
				return abbreviations;
			}
		}

		protected override void FillViewTypes(IMvxTypeCache<View> cache)
		{
			base.FillViewTypes (cache);
			cache.AddAssembly (typeof(DeapExtensions.Binding.Droid.Views.BindableGroupListView).Assembly);
		}
    }
}