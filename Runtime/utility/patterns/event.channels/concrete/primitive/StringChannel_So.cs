using SGLib.Utility.Patterns.EventChannels.Core;
using UnityEngine;

namespace SGLib.Utility.Patterns.EventChannels.Concrete
{
    [CreateAssetMenu(
        fileName = "StringChannel",
        menuName = "Event Channels/Primitive/String Channel"
    )]
    public class StringChannel_So : SG_ChannelT1_So<string> { }
}

