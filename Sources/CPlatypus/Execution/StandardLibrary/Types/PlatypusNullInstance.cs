using CPlatypus.Execution.Object;

namespace CPlatypus.Execution.StandardLibrary.Types
{
    public class PlatypusNullInstance : PlatypusInstance
    {
        public static PlatypusNullInstance Instance { get; } = new PlatypusNullInstance();
        
        private PlatypusNullInstance() : base(null, null)
        {
        }

        public override string ToString()
        {
            return "PLATYPUS_NULL";
        }
    }
}