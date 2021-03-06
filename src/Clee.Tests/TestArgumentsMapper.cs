﻿using System;
using System.Globalization;
using System.Security.Claims;
using Xunit;

namespace Clee.Tests
{
    public class TestArgumentsMapper
    {
        [Fact]
        public void returns_instance()
        {
            var sut = new DefaultArgumentMapper();
            var result = sut.Map(typeof (RelaxedArgument), new Argument[0]);
            
            Assert.NotNull(result);
        }

        [Fact]
        public void returns_expected_type()
        {
            var sut = new DefaultArgumentMapper();
            var result = sut.Map(typeof(RelaxedArgument), new Argument[0]);

            Assert.IsAssignableFrom<RelaxedArgument>(result);
        }

        [Fact]
        public void returns_default_value_for_properties_marked_with_optional_attribute()
        {
            var sut = new DefaultArgumentMapper();
            var result = (RelaxedArgument)sut.Map(typeof(RelaxedArgument), new Argument[0]);

            Assert.Null(result.String);
        }

        [Fact]
        public void throws_exception_if_required_property_is_missing_a_value()
        {
            var sut = new DefaultArgumentMapper();
            Assert.Throws<Exception>(() => sut.Map(typeof (StrictArgument), new Argument[0]));
        }

        [Fact]
        public void can_map_simple_short_datetime()
        {
            var sut = new DefaultArgumentMapper();
            var result = (RelaxedArgument)sut.Map(typeof(RelaxedArgument), new[] { new Argument("datetime", "2000-01-01"), });

            Assert.Equal(new DateTime(2000, 1, 1), result.DateTime);
        }

        [Fact]
        public void can_map_simple_long_datetime()
        {
            var sut = new DefaultArgumentMapper();
            var result = (RelaxedArgument)sut.Map(typeof(RelaxedArgument), new[] { new Argument("datetime", "2000-01-01 01:02:03"), });

            Assert.Equal(new DateTime(2000, 1, 1, 1, 2, 3), result.DateTime);
        }

        [Fact]
        public void can_map_simple_guid()
        {
            var sut = new DefaultArgumentMapper();
            var result = (RelaxedArgument)sut.Map(typeof(RelaxedArgument), new[] { new Argument("guid", "D29F1B98-00F4-49D9-AFF8-87755859B702"), });
            
            Assert.Equal(new Guid("D29F1B98-00F4-49D9-AFF8-87755859B702"), result.Guid);
        }



        [Fact]
        public void can_map_simple_byte()
        {
            var sut = new DefaultArgumentMapper();
            var result = (RelaxedArgument)sut.Map(typeof(RelaxedArgument), new[] { new Argument("byte", "1") });

            Assert.Equal(1, result.Byte);
        }

        [Fact]
        public void can_map_simple_sbyte()
        {
            var sut = new DefaultArgumentMapper();
            var result = (RelaxedArgument)sut.Map(typeof(RelaxedArgument), new[] { new Argument("sbyte", "1") });

            Assert.Equal(1, result.SByte);
        }

        [Fact]
        public void can_map_simple_short()
        {
            var sut = new DefaultArgumentMapper();
            var result = (RelaxedArgument)sut.Map(typeof(RelaxedArgument), new[] { new Argument("short", "1") });

            Assert.Equal(1, result.Short);
        }

        [Fact]
        public void can_map_simple_ushort()
        {
            var sut = new DefaultArgumentMapper();
            var result = (RelaxedArgument)sut.Map(typeof(RelaxedArgument), new[] { new Argument("ushort", "1") });

            Assert.Equal(1, result.UShort);
        }

        [Fact]
        public void can_map_simple_int()
        {
            var sut = new DefaultArgumentMapper();
            var result = (RelaxedArgument)sut.Map(typeof(RelaxedArgument), new[] { new Argument("int", "1") });

            Assert.Equal(1, result.Int);
        }

        [Fact]
        public void can_map_simple_uint()
        {
            var sut = new DefaultArgumentMapper();
            var result = (RelaxedArgument)sut.Map(typeof(RelaxedArgument), new[] { new Argument("uint", "1") });

            Assert.Equal(1, result.UInt);
        }

        [Fact]
        public void can_map_simple_long()
        {
            var sut = new DefaultArgumentMapper();
            var result = (RelaxedArgument)sut.Map(typeof(RelaxedArgument), new[] { new Argument("long", "1") });

            Assert.Equal(1, result.Long);
        }

        [Fact]
        public void can_map_simple_ulong()
        {
            var sut = new DefaultArgumentMapper();
            var result = (RelaxedArgument)sut.Map(typeof(RelaxedArgument), new[] { new Argument("ulong", "1") });

            Assert.Equal((ulong) 1, result.ULong);
        }

        [Fact]
        public void can_map_simple_float()
        {
            var sut = new DefaultArgumentMapper();
            var result = (RelaxedArgument)sut.Map(typeof(RelaxedArgument), new[] { new Argument("float", "1.23") });

            Assert.Equal(1.23f, result.Float);
        }

        [Fact]
        public void can_map_simple_double()
        {
            var sut = new DefaultArgumentMapper();
            var result = (RelaxedArgument)sut.Map(typeof(RelaxedArgument), new[] { new Argument("double", "1.23") });

            Assert.Equal(1.23d, result.Double);
        }

        [Fact]
        public void can_map_simple_decimal()
        {
            var sut = new DefaultArgumentMapper();
            var result = (RelaxedArgument)sut.Map(typeof(RelaxedArgument), new[] { new Argument("decimal", "1.23") });

            Assert.Equal(1.23M, result.Decimal);
        }

        [Fact]
        public void can_map_simple_char()
        {
            var sut = new DefaultArgumentMapper();
            var result = (RelaxedArgument)sut.Map(typeof(RelaxedArgument), new[] { new Argument("char", "A") });

            Assert.Equal('A', result.Char);
        }

        [Fact]
        public void can_map_simple_string()
        {
            var sut = new DefaultArgumentMapper();
            var result = (RelaxedArgument)sut.Map(typeof(RelaxedArgument), new[] { new Argument("string", "foo") });

            Assert.Equal("foo", result.String);
        }

        [Fact]
        public void can_map_simple_bool_to_true()
        {
            var sut = new DefaultArgumentMapper();
            var result = (RelaxedArgument)sut.Map(typeof(RelaxedArgument), new[] { new Argument("bool", "true") });

            Assert.Equal(true, result.Bool);
        }

        [Fact]
        public void can_map_simple_bool_to_false()
        {
            var sut = new DefaultArgumentMapper();
            var result = (RelaxedArgument)sut.Map(typeof(RelaxedArgument), new[] { new Argument("bool", "false") });

            Assert.Equal(false, result.Bool);
        }

        [Fact]
        public void bool_always_defaults_to_true_if_present_and_has_empty_value()
        {
            var sut = new DefaultArgumentMapper();
            var result = (RelaxedArgument)sut.Map(typeof(RelaxedArgument), new[] { new Argument("bool", "") });

            Assert.Equal(true, result.Bool);
        }

        [Fact]
        public void bool_always_defaults_to_false_on_relaxed_types_if_not_present()
        {
            var sut = new DefaultArgumentMapper();
            var result = (RelaxedArgument)sut.Map(typeof(RelaxedArgument), new Argument[0]);

            Assert.Equal(false, result.Bool);
        }

        [Fact]
        public void bool_is_optional_on_strict_types_and_defaults_to_false_if_not_present()
        {
            var sut = new DefaultArgumentMapper();
            var result = (StrictArgument) sut.Map(typeof (StrictArgument), new[] {new Argument("text", "foo")});

            Assert.Equal(false, result.Bool);
        }

        private class RelaxedArgument : ICommandArguments
        {
            [Value(IsOptional = true)]
            public string String { get; set; }

            [Value(IsOptional = true)]
            public double Double { get; set; }

            [Value(IsOptional = true)]
            public decimal Decimal { get; set; }

            [Value(IsOptional = true)]
            public DateTime DateTime { get; set; }

            [Value(IsOptional = true)]
            public Guid Guid { get; set; }

            [Value(IsOptional = true)]
            public int Byte { get; set; }

            [Value(IsOptional = true)]
            public int SByte { get; set; }

            [Value(IsOptional = true)]
            public short Short { get; set; }

            [Value(IsOptional = true)]
            public ushort UShort { get; set; }

            [Value(IsOptional = true)]
            public int Int { get; set; }

            [Value(IsOptional = true)]
            public int UInt { get; set; }

            [Value(IsOptional = true)]
            public long Long { get; set; }

            [Value(IsOptional = true)]
            public ulong ULong { get; set; }

            [Value(IsOptional = true)]
            public float Float { get; set; }

            [Value(IsOptional = true)]
            public char Char { get; set; }

            [Value(IsOptional = true)]
            public bool Bool { get; set; }
        }

        private class StrictArgument : ICommandArguments
        {
            public string Text { get; set; }
            public bool Bool { get; set; }
        }

        private class CustomValueType
        {
            private readonly int _value;

            public CustomValueType(int value)
            {
                _value = value;
            }

            public int Value
            {
                get { return _value; }
            }

            public static bool TryParse(string input, out CustomValueType instance)
            {
                int value;

                if (int.TryParse(input, out value))
                {
                    instance = new CustomValueType(value);
                    return true;
                }

                instance = null;
                return false;
            }
        }

        private class CustomValueTypeArgument : ICommandArguments
        {
            public CustomValueType Id { get; set; } 
        }

        [Fact]
        public void can_use_a_try_parse_method_by_convention()
        {
            var sut = new DefaultArgumentMapper();
            var result = (CustomValueTypeArgument)sut.Map(typeof(CustomValueTypeArgument), new[] { new Argument("id", "1"), });

            Assert.Equal(1, result.Id.Value);
        }

        private class SimpleConstructor
        {
            private readonly int _value;

            public SimpleConstructor(int value)
            {
                _value = value;
            }

            public int Value
            {
                get { return _value; }
            }
        }

        private class SimpleConstructorArgument : ICommandArguments
        {
            public SimpleConstructor Id { get; set; } 
        }

        [Fact]
        public void can_use_simple_constructor_with_a_simple_primitive()
        {
            var sut = new DefaultArgumentMapper();
            var result = (SimpleConstructorArgument)sut.Map(typeof(SimpleConstructorArgument), new[] { new Argument("id", "1"), });

            Assert.Equal(1, result.Id.Value);
        }

        [Fact]
        public void can_map_nullable_guid()
        {
            var expected = Guid.NewGuid();

            var sut = new DefaultArgumentMapper();
            var result = (RelaxedNullableArgument)sut.Map(typeof(RelaxedNullableArgument), new[] { new Argument("nullableguid", expected.ToString("N")) });

            Assert.Equal(expected, result.NullableGuid);
            Assert.Equal(expected, result.NullableGuid.Value);
        }

        [Fact]
        public void can_map_nullable_int()
        {
            var expected = 1;

            var sut = new DefaultArgumentMapper();
            var result = (RelaxedNullableArgument)sut.Map(typeof(RelaxedNullableArgument), new[] { new Argument("nullableint", expected.ToString()) });

            Assert.Equal(expected, result.NullableInt);
            Assert.Equal(expected, result.NullableInt.Value);
        }

        private class RelaxedNullableArgument : ICommandArguments
        {
            [Value(IsOptional = true)]
            public Guid? NullableGuid { get; set; }

            [Value(IsOptional = true)]
            public int? NullableInt { get; set; }
        }

    }
}