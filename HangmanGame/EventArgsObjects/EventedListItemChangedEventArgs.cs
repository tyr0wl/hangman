using System;

namespace HangmanGame.EventArgsObjects
{
    /// <summary>
    ///     Object for the ItemChanged Event of the EventedList object.
    /// </summary>
    /// <typeparam name="T">Type of the objects stored in the EventedList.</typeparam>
    public class EventedListItemChangedEventArgs<T> : EventArgs
    {
        public EventedListItemChangedEventArgs(T oldValue, T newValue, int index)
        {
            OldValue = oldValue;
            NewValue = newValue;
            Index = index;
        }

        public T OldValue { get; }
        public T NewValue { get; }
        public int Index { get; }
    }
}