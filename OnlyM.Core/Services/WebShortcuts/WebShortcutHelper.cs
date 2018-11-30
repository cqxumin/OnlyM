﻿namespace OnlyM.Core.Services.WebShortcuts
{
    using System;
    using System.IO;
    using System.Linq;

    public class WebShortcutHelper
    {
        private const string UrlToken = "URL";

        private readonly string _path;
        private bool _initialised;
        private string _webAddress;

        public WebShortcutHelper(string path)
        {
            _path = path;
        }
        
        public string Uri
        {
            get
            {
                Init();
                return _webAddress;
            }
        }

        private void Init()
        {
            if (!_initialised)
            {
                try
                {
                    var lines = File.ReadLines(_path);
                    var line = lines.SingleOrDefault(x => x.ToUpper().Trim().StartsWith(UrlToken));
                    if (line != null)
                    {
                        var pos = line.IndexOf("=", StringComparison.OrdinalIgnoreCase);
                        if (pos > 0)
                        {
                            _webAddress = line.Substring(pos + 1).Trim();
                        }
                    }
                }
                catch (Exception)
                {
                    // nothing
                }
                finally
                {
                    _initialised = true;
                }
            }
        }
    }
}
