using System.Runtime.CompilerServices;

namespace AssetRipper.TextureDecoder.Rgb
{
	internal static class ConversionUtilities
	{
		internal static TTo ConvertValue<TFrom, TTo>(TFrom value)
			where TFrom : unmanaged
			where TTo : unmanaged
		{
			if (typeof(TFrom) == typeof(TTo))
			{
				return Unsafe.As<TFrom, TTo>(ref value);
			}
			else if(typeof(TFrom) == typeof(byte))
			{
				return ConvertUInt8<TTo>(Unsafe.As<TFrom, byte>(ref value));
			}
			else if (typeof(TFrom) == typeof(ushort))
			{
				return ConvertUInt16<TTo>(Unsafe.As<TFrom, ushort>(ref value));
			}
			else if (typeof(TFrom) == typeof(Half))
			{
				return ConvertHalf<TTo>(Unsafe.As<TFrom, Half>(ref value));
			}
			else if (typeof(TFrom) == typeof(float))
			{
				return ConvertSingle<TTo>(Unsafe.As<TFrom, float>(ref value));
			}
			else if (typeof(TFrom) == typeof(double))
			{
				return ConvertDouble<TTo>(Unsafe.As<TFrom, double>(ref value));
			}
			else
			{
				return default; //exceptions prevent inlining
			}
		}

		private static TTo ConvertUInt8<TTo>(byte value) where TTo : unmanaged
		{
			if (typeof(TTo) == typeof(byte))
			{
				return Unsafe.As<byte, TTo>(ref value);
			}
			else if (typeof(TTo) == typeof(ushort))
			{
				ushort converted = unchecked((ushort)((uint)value << 8));
				return Unsafe.As<ushort, TTo>(ref converted);
			}
			else if (typeof(TTo) == typeof(Half))
			{
				Half converted = (Half)(value / (float)byte.MaxValue);
				return Unsafe.As<Half, TTo>(ref converted);
			}
			else if (typeof(TTo) == typeof(float))
			{
				float converted = (float)(value / (float)byte.MaxValue);
				return Unsafe.As<float, TTo>(ref converted);
			}
			else if (typeof(TTo) == typeof(double))
			{
				double converted = (double)(value / (double)byte.MaxValue);
				return Unsafe.As<double, TTo>(ref converted);
			}
			else
			{
				return default; //exceptions prevent inlining
			}
		}

		private static TTo ConvertUInt16<TTo>(ushort value) where TTo : unmanaged
		{
			if (typeof(TTo) == typeof(byte))
			{
				byte converted = unchecked((byte)((uint)value >> 8));
				return Unsafe.As<byte, TTo>(ref converted);
			}
			else if (typeof(TTo) == typeof(ushort))
			{
				return Unsafe.As<ushort, TTo>(ref value);
			}
			else if (typeof(TTo) == typeof(Half))
			{
				Half converted = (Half)(value / (float)ushort.MaxValue);
				return Unsafe.As<Half, TTo>(ref converted);
			}
			else if (typeof(TTo) == typeof(float))
			{
				float converted = (float)(value / (float)ushort.MaxValue);
				return Unsafe.As<float, TTo>(ref converted);
			}
			else if (typeof(TTo) == typeof(double))
			{
				double converted = (double)(value / (double)ushort.MaxValue);
				return Unsafe.As<double, TTo>(ref converted);
			}
			else
			{
				return default; //exceptions prevent inlining
			}
		}

		private static TTo ConvertHalf<TTo>(Half value) where TTo : unmanaged
		{
			if (typeof(TTo) == typeof(byte))
			{
				byte converted = ClampUInt8((float)value * byte.MaxValue);
				return Unsafe.As<byte, TTo>(ref converted);
			}
			else if (typeof(TTo) == typeof(ushort))
			{
				ushort converted = ClampUInt16((float)value * ushort.MaxValue);
				return Unsafe.As<ushort, TTo>(ref converted);
			}
			else if (typeof(TTo) == typeof(Half))
			{
				return Unsafe.As<Half, TTo>(ref value);
			}
			else if (typeof(TTo) == typeof(float))
			{
				float converted = (float)value;
				return Unsafe.As<float, TTo>(ref converted);
			}
			else if (typeof(TTo) == typeof(double))
			{
				double converted = (double)value;
				return Unsafe.As<double, TTo>(ref converted);
			}
			else
			{
				return default; //exceptions prevent inlining
			}
		}

		private static TTo ConvertSingle<TTo>(float value) where TTo : unmanaged
		{
			if (typeof(TTo) == typeof(byte))
			{
				byte converted = ClampUInt8(value * byte.MaxValue);
				return Unsafe.As<byte, TTo>(ref converted);
			}
			else if (typeof(TTo) == typeof(ushort))
			{
				ushort converted = ClampUInt16(value * ushort.MaxValue);
				return Unsafe.As<ushort, TTo>(ref converted);
			}
			else if (typeof(TTo) == typeof(Half))
			{
				Half converted = (Half)value;
				return Unsafe.As<Half, TTo>(ref converted);
			}
			else if (typeof(TTo) == typeof(float))
			{
				return Unsafe.As<float, TTo>(ref value);
			}
			else if (typeof(TTo) == typeof(double))
			{
				double converted = (double)value;
				return Unsafe.As<double, TTo>(ref converted);
			}
			else
			{
				return default; //exceptions prevent inlining
			}
		}

		private static TTo ConvertDouble<TTo>(double value) where TTo : unmanaged
		{
			if (typeof(TTo) == typeof(byte))
			{
				byte converted = ClampUInt8(value * byte.MaxValue);
				return Unsafe.As<byte, TTo>(ref converted);
			}
			else if (typeof(TTo) == typeof(ushort))
			{
				ushort converted = ClampUInt16(value * ushort.MaxValue);
				return Unsafe.As<ushort, TTo>(ref converted);
			}
			else if (typeof(TTo) == typeof(Half))
			{
				Half converted = (Half)value;
				return Unsafe.As<Half, TTo>(ref converted);
			}
			else if (typeof(TTo) == typeof(float))
			{
				float converted = (float)value;
				return Unsafe.As<float, TTo>(ref converted);
			}
			else if (typeof(TTo) == typeof(double))
			{
				return Unsafe.As<double, TTo>(ref value);
			}
			else
			{
				return default; //exceptions prevent inlining
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static byte ClampUInt8(float x)
		{
			return byte.MaxValue < x ? byte.MaxValue : (x > byte.MinValue ? (byte)x : byte.MinValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static byte ClampUInt8(double x)
		{
			return byte.MaxValue < x ? byte.MaxValue : (x > byte.MinValue ? (byte)x : byte.MinValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static ushort ClampUInt16(float x)
		{
			return ushort.MaxValue < x ? ushort.MaxValue : (x > ushort.MinValue ? (ushort)x : ushort.MinValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static ushort ClampUInt16(double x)
		{
			return ushort.MaxValue < x ? ushort.MaxValue : (x > ushort.MinValue ? (ushort)x : ushort.MinValue);
		}
	}
}
