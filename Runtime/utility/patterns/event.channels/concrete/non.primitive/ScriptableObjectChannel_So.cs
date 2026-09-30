using SGLib.Utility.Patterns.EventChannels.Core;
using UnityEngine;

namespace SGLib.Utility.Patterns.EventChannels.Concrete
{
    [CreateAssetMenu(
        fileName = "ScriptableObjectChannel",
        menuName = "Event Channels/NonPrimitive/ScriptableObject Channel"
    )]
    public class ScriptableObjectChannel_So : SG_ChannelT1_So<ScriptableObject> { }
}

