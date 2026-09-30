using SGLib.Utility.Patterns.EventChannels.Core;
using UnityEngine;

namespace SGLib.Utility.Patterns.EventChannels.Concrete
{
    [CreateAssetMenu(
        fileName = "LongChannel",
        menuName = "Event Channels/Primitive/Long Channel"
    )]
    public class LongChannel_So : SG_ChannelT1_So<long> { }
}

