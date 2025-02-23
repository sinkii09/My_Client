using System.Collections.Generic;

namespace Nara.System.Network
{
    public class PacketProcessHandler
    {
        public List<IPacketProcessor> Processors { get; } = new List<IPacketProcessor>();
        public void Process(PacketMessage message)
        {
            foreach (var processor in Processors)
            {
                processor.Process(message);
            }
        }
        public void RegisterProcessor(IPacketProcessor processor)
        {
            Processors.Add(processor);
        }
        public void UnregisterProcessor(IPacketProcessor processor)
        {
            Processors.Remove(processor);
        }
    }
}