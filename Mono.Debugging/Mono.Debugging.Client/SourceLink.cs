using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Mono.Debugging.Client
{
    [Serializable]
    public class SourceLink
    {
        public string Uri { get; }
        public string RelativeFilePath { get; }

        public SourceLink (string uri, string relativeFilePath)
        {
            RelativeFilePath = relativeFilePath;
            Uri = uri;
        }

        public string GetDownloadLocation (string cachePath)
        {
            return Path.Combine (cachePath, MakeValidFileName (Uri));
        }

        static string MakeValidFileName (string text)
        {
            var sb = new StringBuilder (text.Length);
            var invalids = Path.GetInvalidFileNameChars ();
            for (int i = 0; i < text.Length; i++) {
                char c = text[i];
                if (invalids.Contains (c)) {
                    sb.Append ('_');
                } else
                    sb.Append (c);
            }
            if (sb.Length == 0)
                return "_";
            return sb.ToString ();
        }
    }
}
