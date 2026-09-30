using SGLib.Utility.Patterns.EventChannels.Core;
using UnityEngine;
namespace SGLib.Utility.Patterns.EventChannels.Concrete
{
    [CreateAssetMenu(
        fileName = "Vector3Channel",
        menuName = "Event Channels/NonPrimitive/Vector3 Channel"
    )]
    public class Vector3Channel_So : SG_ChannelT1_So<Vector3> { }
}