using SGLib.Utility.Patterns.EventChannels.Core;
using UnityEngine;

namespace SGLib.Utility.Patterns.EventChannels.Concrete
{
    [CreateAssetMenu(
        fileName = "BoolChannel",
        menuName = "Event Channels/Primitive/Bool Channel"
    )]
    public class BoolChannel_So : SG_ChannelT1_So<bool> { }
}

