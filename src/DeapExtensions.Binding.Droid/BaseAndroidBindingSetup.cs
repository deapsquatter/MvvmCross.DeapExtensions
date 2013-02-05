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

namespace DeapExtensions.Binding.Droid
{
    public abstract class BaseAndroidBindingSetup
        : MvxBaseAndroidBindingSetup
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
    }
}