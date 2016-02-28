using System;

namespace EventRouting
{
	/// <summary>
	/// Summary description for EventArgs.
	/// </summary>
    public class EventArgs : System.EventArgs 
    {
        bool handled;
        public bool Handled 
        {
            get { return this.handled; }
            set { this.handled = value; }
        }
    }
}
