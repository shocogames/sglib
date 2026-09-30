using SGLib.Utility.Patterns.EventChannels.Core;
using UnityEngine;

namespace SGLib.Utility.Patterns.EventChannels.Concrete
{
    [CreateAssetMenu(
        fileName = "GameObjectChannel",
        menuName = "Event Channels/NonPrimitive/GameObject Channel"
    )]
    public class GameObjectChannel_So : SG_ChannelT1_So<GameObject> { }
}

