using System;
using System.Linq.Expressions;
using System.Windows;

namespace HangmanGame.UI.Helper
{
    /// <summary>
    ///     Helper class to Register DependencyProperties
    /// </summary>
    public static class For<TOwner> where TOwner : DependencyObject
    {
        /// <summary>
        ///     Registers a DependencyProperty for the given OwnerType in WPF. Used Like the build in RegisterCommand
        ///     from PresentationCore to initiate static Properties.
        ///     Usage: For&lt;OwnerType&gt;.Register(o => o.PropertyName);
        ///     Example:
        /// </summary>
        /// <example>
        ///     <code>
        ///     public static readonly DependencyProperty TextProperty = For&lt;LetterBox&gt;.Register(o =&gt; o.Text);
        /// </code>
        /// </example>
        /// <typeparam name="TProperty">
        ///     Type of the Property to Register.
        ///     Usually can be determined automatically by the compiler.
        /// </typeparam>
        /// <param name="property">expression to pass the property that will be registered</param>
        /// <param name="defaultValue">DefaultValue of the Property</param>
        /// <param name="callback">Will be executed if the registered Property is set</param>
        /// <returns>Registered <see cref="DependencyProperty" /></returns>
        public static DependencyProperty Register<TProperty>(Expression<Func<TOwner, TProperty>> property,
                                                             TProperty defaultValue,
                                                             Action<TOwner, DependencyPropertyChangedEventArgs<TProperty>> callback)
        {
            return DependencyProperty.Register(
                GetPropertyName(property),
                typeof(TProperty),
                typeof(TOwner),
                new FrameworkPropertyMetadata(
                    defaultValue,
                    (o, args) => callback((TOwner) o, new DependencyPropertyChangedEventArgs<TProperty>(args))));
        }

        /// <summary>
        ///     Registers a DependencyProperty for the given OwnerType in WPF. Used Like the build in RegisterCommand
        ///     from PresentationCore to initiate static Properties.
        ///     Usage: For&lt;OwnerType&gt;.Register(o => o.PropertyName);
        /// </summary>
        /// <example>
        ///     <code>
        ///     public static readonly DependencyProperty TextProperty = For&lt;LetterBox&gt;.RegisterAttached(o =&gt; o.Text);
        /// </code>
        /// </example>
        /// <typeparam name="TProperty">
        ///     Type of the Property to Register.
        ///     Usually can be determined automatically by the compiler.
        /// </typeparam>
        /// <param name="property">expression to pass the property that will be registered</param>
        /// <param name="defaultValue">DefaultValue of the Property</param>
        /// <param name="callback">Will be executed if the registered Property is set</param>
        /// <returns>Registered DependencyProperty</returns>
        public static DependencyProperty RegisterAttached<TProperty>(
            Expression<Func<TOwner, TProperty>> property,
            TProperty defaultValue,
            Action<DependencyObject, DependencyPropertyChangedEventArgs<TProperty>> callback)
        {
            return DependencyProperty.RegisterAttached(
                GetPropertyName(property),
                typeof(TProperty),
                typeof(TOwner),
                new FrameworkPropertyMetadata(
                    defaultValue,
                    (o, args) => callback(o, new DependencyPropertyChangedEventArgs<TProperty>(args))));
        }

        /// <summary>
        ///     Registers a DependencyProperty for the given OwnerType in WPF. Used Like the build in RegisterCommand
        ///     from PresentationCore to initiate static Properties.
        ///     Usage: For&lt;OwnerType&gt;.Register(o => o.PropertyName);
        /// </summary>
        /// <example>
        ///     <code>
        ///     public static readonly DependencyProperty TextProperty = For&lt;LetterBox&gt;.RegisterAttached(o =&gt; o.Text);
        /// </code>
        /// </example>
        /// <typeparam name="TProperty">
        ///     Type of the Property to Register.
        ///     Usually can be determined automatically by the compiler.
        /// </typeparam>
        /// <param name="property">expression to pass the property that will be registered</param>
        /// <returns></returns>
        public static DependencyProperty Register<TProperty>(Expression<Func<TOwner, TProperty>> property)
        {
            return Register(property, default, (o, args) => { });
        }

        /// <summary>
        ///     Registers a DependencyProperty for the given OwnerType  in WPF. Used Like the build in RegisterCommand
        ///     from PresentationCore to initiate static Properties.
        ///     Usage: For&lt;OwnerType&gt;.Register(o => o.PropertyName);
        /// </summary>
        /// <example>
        ///     <code>
        ///     public static readonly DependencyProperty TextProperty = For&lt;LetterBox&gt;.RegisterAttached(o =&gt; o.Text);
        /// </code>
        /// </example>
        /// <typeparam name="TProperty">
        ///     Type of the Property to Register.
        ///     Usually can be determined automatically by the compiler.
        /// </typeparam>
        /// <param name="property">expression to pass the property that will be registered</param>
        /// <returns></returns>
        public static DependencyProperty RegisterAttached<TProperty>(Expression<Func<TOwner, TProperty>> property)
        {
            return RegisterAttached(property, default, (o, args) => { });
        }

        private static string GetPropertyName<T>(Expression<Func<TOwner, T>> property)
        {
            return ((MemberExpression)property.Body).Member.Name;
        }
    }
}