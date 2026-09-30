using SGLib.Utility.Patterns.EventChannels.Core;
using UnityEngine;

namespace SGLib.Utility.Patterns.EventChannels.Concrete
{
    [CreateAssetMenu(
        fileName = "MonoBehaviourChannel",
        menuName = "Event Channels/NonPrimitive/MonoBehaviour Channel"
    )]
    public class MonoBehaviourChannel_So : SG_ChannelT1_So<MonoBehaviour> { }

}

