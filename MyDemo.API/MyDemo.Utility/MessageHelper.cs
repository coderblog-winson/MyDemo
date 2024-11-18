namespace MyDemo.Utility;

using System.Xml.Linq;

public class MessageHelper
{
    public MessageHelper() { }

    public Dictionary<string, string> Msg { get; private set; }

    public void Init(string files)
    {
        Msg = new Dictionary<string, string>();

        List<string> xmlFiles = files.Split(',').ToList();
        foreach (var file in xmlFiles)
        {
            LoadMessages(file);
        }
    }

    private void LoadMessages(string xmlFile)
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), $"Messages\\{xmlFile}.xml");
        var xdoc = XDocument.Load(filePath);

        var items = xdoc.Descendants("Item")
                  .Select(item => new
                  {
                      Id = item.Attribute("id").Value,
                      Value = item.Value
                  });

        foreach (var msg in items)
        {
            var id = msg.Id;
            var message = msg.Value;
            if (id != null && !Msg.ContainsKey(id))
            {
                Msg[id] = message;
            }
        }
    }
}

