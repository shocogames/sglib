using SGLib.Utility.Patterns.EventChannels.Core;
using UnityEngine;
namespace SGLib.Utility.Patterns.EventChannels.Concrete
{
    [CreateAssetMenu(
        fileName = "Vector2Channel",
        menuName = "Event Channels/NonPrimitive/Vector2 Channel"
    )]
    public class Vector2Channel_So : SG_ChannelT1_So<Vector2> { }
}
