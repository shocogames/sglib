namespace SGLib.AI.Behaviour.StateMachines.Classic
{
    public abstract class HierarchicalStateBase : StateBase
    {
        public HierarchicalStateBase Ancestor { get; private set; }

        public void SetAncestor(HierarchicalStateBase ancestor)
        {
            Ancestor = ancestor; // can be null
        }
    }
}
