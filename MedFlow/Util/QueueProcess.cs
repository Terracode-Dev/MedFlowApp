using System.Collections.Generic;
using System.Threading;
namespace MedFlow.Util
{
    public class QueueProcess
    {
        private Queue<object> Que = new Queue<object>();
        private Thread pushThread;
        private Thread pullThread;

        public QueueProcess() {
            pushThread = new Thread(pushTHandle);
            pullThread = new Thread(pullTHandle);
        }

        private void pushTHandle ()
        {

        }

        private void pullTHandle ()
        {

        }
        public void PushData(object data)
        {
            
        }

        public object PullData ()
        {
            return Que.Dequeue();
        }


    }
}
