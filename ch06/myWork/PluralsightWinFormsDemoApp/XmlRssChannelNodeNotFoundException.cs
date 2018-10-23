using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class XmlRssChannelNodeNotFoundException : Exception
{
    public XmlRssChannelNodeNotFoundException()
    {
    }

    public XmlRssChannelNodeNotFoundException(string message)
        : base(message)
    {
    }

    public XmlRssChannelNodeNotFoundException(string message, Exception inner)
        : base(message, inner)
    {
    }
}