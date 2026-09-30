using SGLib.Utility.Patterns.EventChannels.Core;
using UnityEngine;

namespace SGLib.Utility.Patterns.EventChannels.Concrete
{
    [CreateAssetMenu(
        fileName = "SceneChannel",
        menuName = "Event Channels/NonPrimitive/Scene Channel"
    )]
    public class SceneChannel_So : SG_ChannelT1_So<UnityEngine.SceneManagement.Scene> { }
}

