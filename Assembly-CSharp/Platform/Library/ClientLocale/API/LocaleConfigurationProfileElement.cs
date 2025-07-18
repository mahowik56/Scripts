using System;
using Platform.Kernel.OSGi.ClientCore.API;
using Platform.Library.ClientLocale.Impl;
using Platform.Library.ClientLogger.API;
using Platform.System.Data.Statics.ClientConfigurator.API;
using UnityEngine;

namespace Platform.Library.ClientLocale.API
{
	public class LocaleConfigurationProfileElement : MonoBehaviour, ConfigurationProfileElement
	{
		public string language;


		public string ProfileElement
		{
			get
			{
				language = LocaleUtils.GetSavedLocaleCode();
                                if (string.IsNullOrEmpty(language))
                                {
                                        switch (Application.systemLanguage)
                                        {
                                        case SystemLanguage.Russian:
                                                language = "ru";
                                                break;
                                        case SystemLanguage.English:
                                                language = "en";
                                                break;
                                        case SystemLanguage.Turkish:
                                                language = "tr";
                                                break;
                                        default:
                                                language = "en";
                                                break;
                                        }
                                }
				LocaleUtils.SaveLocaleCode(language);
				return language;
			}
		}
	}
}
