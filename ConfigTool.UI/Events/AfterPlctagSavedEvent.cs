using Prism.Events;

namespace ConfigTool.UI.Events
{
    class AfterPlctagSavedEvent:PubSubEvent<AfterPlctagSavedEventArgs>
    {
    }

    public class AfterPlctagSavedEventArgs
    {
        public int Id { get; set; }
        public string DisplayMember { get; set; }

    }
}
