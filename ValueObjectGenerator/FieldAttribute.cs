﻿using System;

namespace RhoMicro.ValueObjectGenerator
{
    /// <summary>
    /// Informs the value object generator to generate a readonly field into the generated
    /// value object.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true, Inherited = false)]
    public sealed class FieldAttribute : Attribute
    {
        /// <summary>
        /// Defines field visibility modifiers.
        /// </summary>
        public enum VisibilityModifier
        {
            /// <summary>
            /// The field will be <see langword="public"/>.
            /// </summary>
            Public,
            /// <summary>
            /// The field will be <see langword="private"/>.
            /// </summary>
            Private,
            /// <summary>
            /// The field will be <see langword="protected"/>.
            /// </summary>
            Protected,
            /// <summary>
            /// The field will be <see langword="internal"/>.
            /// </summary>
            Internal,
            /// <summary>
            /// The field will be <see langword="protected"/> <see langword="internal"/>.
            /// </summary>
            ProtectedInternal,
            /// <summary>
            /// The field will be <see langword="private"/> <see langword="protected"/>.
            /// </summary>
            PrivateProtected
        }
        /// <summary>
        /// Defines generation options for a field.
        /// </summary>
        [Flags]
        public enum Options
        {
            /// <summary>
            /// No special generation options should be applied for this field.
            /// </summary>
            None = 0x0,
            /// <summary>
            /// The field will be included in the generated deconstruction mechanism.
            /// <para>
            /// If only a single field contained in the generated type is marked as 
            /// deconstructable, the mechanism chosen will be the implicit type
            /// conversion to that fields type using the field value.
            /// </para>
            /// <para>
            /// If multiple fields contained in the generated type are marked as 
            /// deconstructable, the mechanism chosen will be the <c>Deconstruct</c>
            /// method, making available all deconstructible field values as
            /// <see langword="out"/> parameters.
            /// </para>
            /// <para>
            /// If no fields are marked as deconstructable, no deconstruction mechanism will be generated.
            /// </para>
            /// </summary>
            Deconstructable = 0x1,
            /// <summary>
            /// The field will support the <c>WithX(T x)</c> syntax for 
            /// transforming instances of the generated value object.
            /// </summary>
            SupportsWith = 0x2,
            /// <summary>
            /// The field will be included in the generated validation mechanisms.
            /// </summary>
            Validated = 0x4,
            /// <summary>
            /// The field name and value will be excluded from the <see cref="System.Diagnostics.DebuggerDisplayAttribute"/>.
            /// </summary>
            ExcludedFromDebuggerDisplay = 0x8
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="type">The type of the generated field.</param>
        /// <param name="name">The name of the generated field.</param>
        public FieldAttribute(Type type, String name)
        {
            Type = type;
            Name = name;
        }
        /// <summary>
        /// Gets the type of the generated field.
        /// </summary>
        public Type Type { get; private set; }
        /// <summary>
        /// Gets the name of the generated field.
        /// </summary>
        public String Name { get; private set; }
        /// <summary>
        /// Gets or sets the visibility of the generated field.
        /// </summary>
        public VisibilityModifier Visibility { get; set; }
        /// <summary>
        /// Gets or sets the documentation summary of the generated field.
        /// </summary>
        public String Summary { get; set; }
        /// <summary>
        /// Gets or sets additional options for the generated field.
        /// </summary>
        public Options GenerateOptions { get; set; }

        public Boolean IsValidated => GenerateOptions.HasFlag(Options.Validated);
        public Boolean IsDeconstructable => GenerateOptions.HasFlag(Options.Deconstructable);
        public Boolean SupportsWith => GenerateOptions.HasFlag(Options.SupportsWith);
        public Boolean ExcludedFromDebugDisplay => GenerateOptions.HasFlag(Options.ExcludedFromDebuggerDisplay);

        /// <summary>
        /// This method is not intended for use outside of the generator.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="type"></param>
        public void SetTypeProperty(String propertyName, Object type)
        {
            if(propertyName == nameof(Type))
            {
                Type = Type.GetType(type.ToString());
            }
        }
        /// <summary>
        /// This method is not intended for use outside of the generator.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public Object GetTypeProperty(String propertyName)
        {
            if(propertyName == nameof(Type))
            {
                return Type;
            }

            throw new InvalidOperationException();
        }
        /// <summary>
        /// This method is not intended for use outside of the generator.
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="type"></param>
        public void SetTypeParameter(String parameterName, Object type)
        {
            if(parameterName == "type")
            {
                Type = Type.GetType(type.ToString());
            }
        }
        /// <summary>
        /// This method is not intended for use outside of the generator.
        /// </summary>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public Object GetTypeParameter(String parameterName)
        {
            if(parameterName == "type")
            {
                return Type;
            }

            throw new InvalidOperationException();
        }
    }
}