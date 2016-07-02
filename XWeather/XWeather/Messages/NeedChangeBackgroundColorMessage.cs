using MvvmCross.Plugins.Messenger;

namespace XWeather.Messages
{
    public class NeedChangeBackgroundColorMessage : MvxMessage
    {
        public int Clouds { get; private set; }

        public NeedChangeBackgroundColorMessage(object sender, int clouds) : base(sender)
        {
            Clouds = clouds;
        }
    }
}