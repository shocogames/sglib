using SGLib.Utility.Patterns.EventChannels.Core;
using UnityEngine;

namespace SGLib.Utility.Patterns.EventChannels.Concrete
{
    [CreateAssetMenu(
        fileName = "CharChannel",
        menuName = "Event Channels/Primitive/Char Channel"
    )]
    public class CharChannel_So : SG_ChannelT1_So<char> { }
}

