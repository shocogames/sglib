using SGLib.Utility.Patterns.EventChannels.Core;
using UnityEngine;

namespace SGLib.Utility.Patterns.EventChannels.Concrete
{
    [CreateAssetMenu(
        fileName = "DoubleChannel",
        menuName = "Event Channels/Primitive/Double Channel"
    )]
    public class DoubleChannel_So : SG_ChannelT1_So<double> { }
}

