using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace NNChallenge.Services;

public interface IMessageBus<T>
{
    Subject<T> Bus { get; }
    IObservable<T> Listen();
    void Publish(T message);
}

public class MessageBus<T> : IMessageBus<T>
{
    public Subject<T> Bus { get; }

    public MessageBus()
    {
        Bus = new Subject<T>();
    }

    public IObservable<T> Listen() => Bus.AsObservable();

    public void Publish(T message) => Bus.OnNext(message);
}
