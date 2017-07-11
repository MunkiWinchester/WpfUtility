using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Sample
{
    public class JsonHelper
    {
        public static List<SampleEntry> ReadValuesFromSample()
        {
            return JsonConvert.DeserializeObject<List<SampleEntry>>(
                File.ReadAllText("SampleFile.json"));
        }
    }
}
