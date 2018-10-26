using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluralsightWinFormsDemoApp.Events
{
    public interface IEventAggregator
    {
        void Publish<T>(T message) where T : IApplicationEvent;
        void Subscribe<T>(Action<T> action) where T : IApplicationEvent;
        void Unsubscribe<T>(Action<T> action) where T : IApplicationEvent;
    }
}
