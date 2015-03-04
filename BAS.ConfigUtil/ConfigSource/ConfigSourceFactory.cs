using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace BAS.ConfigUtil.ConfigSource
{
    public static class ConfigSourceFactory
    {
        #region Variables

        private static IConfigSource _defaultSource;
        #endregion

        #region Properties
        #endregion

        #region Ctors
        static ConfigSourceFactory()
        {
            _defaultSource = new AppSettingsConfigSource();
        }
        #endregion

        #region Methods

        public static void SetDefaultSource(IConfigSource source)
        {
            _defaultSource = source;
        }
        public static IConfigSource GetDefaultSource()
        {
            return _defaultSource;
        }
        #endregion
    }
}
