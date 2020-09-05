using System.IO;
using System.Text.Json;

namespace TrackerApplication.Domain
{
    public static class DataLoader
    {
        public static T Load<T>(string filename)
        {
            var json = File.ReadAllText(filename);
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}
