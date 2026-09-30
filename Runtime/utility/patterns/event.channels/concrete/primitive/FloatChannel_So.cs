using SGLib.Utility.Patterns.EventChannels.Core;
using UnityEngine;

namespace SGLib.Utility.Patterns.EventChannels.Concrete
{
    [CreateAssetMenu(
        fileName = "FloatChannel",
        menuName = "Event Channels/Primitive/Float Channel"
    )]
    public class FloatChannel_So : SG_ChannelT1_So<float> { }
}

