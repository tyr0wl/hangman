using System;
using System.Collections.Generic;
using HangmanGame.EventArgsObjects;

namespace HangmanGame
{
    /// <summary>
    ///     Normal List object that raises an event, when an item was changed.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EventedList<T> : List<T>
    {
        public EventHandler<EventedListItemChangedEventArgs<T>> ItemChanged;

        public new T this[int index]
        {
            get => base[index];
            set
            {
                var oldValue = base[index];
                base[index] = value;

                RaiseItemChanged(index, value, oldValue);
            }
        }

        /// <summary>
        ///     Raises the ItemChanged event
        /// </summary>
        /// <param name="index">index of the item that has changed</param>
        /// <param name="newValue">new Value</param>
        /// <param name="oldValue">old Value</param>
        private void RaiseItemChanged(int index, T newValue, T oldValue)
        {
            ItemChanged?.Invoke(this, new EventedListItemChangedEventArgs<T>(oldValue, newValue, index));
        }
    }
}